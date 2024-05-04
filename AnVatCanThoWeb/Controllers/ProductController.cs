using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Text;
using PusherServer;
using Microsoft.AspNetCore.Authorization;
using AnVatCanThoWeb.Common.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AnVatCanThoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.UserScheme)]
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _db;

        private string ConvertToUnSign(string input)
        {
            input = input.Trim();

            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');

            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }

            return str2;
        }

        private double AvgRatingStar(Product product)
        {
            double sum = 0;

            foreach (var starItem in product.Ratings)
            {
                sum += starItem.Star;
            }
            return sum / product.Ratings.Count;
        }

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /product (list all product)
        [AllowAnonymous]
        public IActionResult Index(string[]? categoryFilter, string? sort, string? search, int page)
        {
            IQueryable<Product> productListQuery = _db.Products.Include(o => o.Ratings)
                .Include(p => p.SnackBar)
                .Include(p => p.ProductImages)
                .AsQueryable();
            List<ProductCategory> categoryList = _db.ProductCategories.ToList();
            List<ProductVM> productVM = new List<ProductVM>();

            const int PER_PAGE = 6;

            // Xử lý lọc sản phẩm
            if (categoryFilter.Length > 0)
            {
                productListQuery = productListQuery.Where(p => categoryFilter.Contains(p.ProductCategoryId.ToString()));
            }

            // Tìm tên của sản phẩm
            if (!string.IsNullOrEmpty(search))
            {
                string searchUnsign = ConvertToUnSign(search);

                //productListQuery = productListQuery.Where(p => ConvertToUnSign(p.Name).Contains(searchUnsign, StringComparison.CurrentCultureIgnoreCase));
                productListQuery = productListQuery.Where(delegate (Product p)
                {
                    if (ConvertToUnSign(p.Name).Contains(searchUnsign, StringComparison.CurrentCultureIgnoreCase))
                        return true;
                    else
                        return false;
                }).AsQueryable();

            }

            // Những product thỏa điều kiện
            List<Product> productList = productListQuery.ToList();

            // Thêm product vào View Model
            foreach (Product product in productList)
            {
                productVM.Add(new ProductVM()
                {
                    Product = product,
                    AvgRating = AvgRatingStar(product)
                });

            }

            // Xử lý sắp xếp sản phẩm
            if (sort == "desc")
            {
                productVM = productVM.OrderByDescending(p => p.Product.UnitPrice).ToList();
            }
            else if (sort == "best")
            {
                productVM = productVM.OrderByDescending(p => p.AvgRating).ToList();
            }
            else
            {
                productVM = productVM.OrderBy(p => p.Product.UnitPrice).ToList();
            }

            // Tổng số trang (Phân trang)
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)productVM.Count / PER_PAGE);

            // Phân trang
            if (page <= 0)
            {
                page = 1;
            }

            int skipNum = PER_PAGE * (page - 1);

            productVM = productVM.Skip(skipNum).Take(PER_PAGE).ToList();

            // Hiển thị danh sách các loại sản phẩm
            ViewBag.CategoryList = categoryList;

            return View(productVM);
        }

        [AllowAnonymous]
        public IActionResult Details(int? id, int page, string sort)
        {
            const int RELATED_PRODUCT_NUMBER = 4;
            List<ProductVM> relatedProductsVM = new List<ProductVM>();
            const int PER_PAGE = 10;

            //Kiểm tra tham số của sản phẩm được truyền vào
            if (id == null)
            {
                return NotFound();
            }

            // Kiểm tra tham số phân trang
            if (page < 1)
            {
                page = 1;
            }
            // Tìm sản phẩm
            Product product = _db.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Ratings)
                .Include(p => p.SnackBar)
                    .ThenInclude(s => s.Addresses)
                .FirstOrDefault(p => p.Id == id);

            // Kiểm tra xem có sản phẩm không
            if (product is null)
            {
                return NotFound();
            }

            IQueryable<Comment> commentQuery = _db.Comments.Include(c => c.Customer)
                .Where(c => c.ProductId == product.Id)
                .AsQueryable();

            commentQuery = (string.IsNullOrEmpty(sort) || sort == "desc")
                            ? commentQuery.OrderByDescending(p => p.CreatedAt)
                            : commentQuery.OrderBy(p => p.CreatedAt);

            // Chi tiết sản phẩm chính
            ProductVM productVM = new ProductVM()
            {
                Product = product,
                AvgRating = AvgRatingStar(product)
            };

            // Những sản phẩm có liên quan
            List<Product> relatedProducts = _db.Products
                .Include(p => p.Ratings)
                .Include(p => p.SnackBar)
                .Include(p => p.ProductImages)
                .Where(p => p.ProductCategoryId == product.ProductCategoryId && p.Id != product.Id)
                .Take(RELATED_PRODUCT_NUMBER)
                .ToList();
            foreach (Product relatedProduct in relatedProducts)
            {
                relatedProductsVM.Add(new ProductVM()
                {
                    Product = relatedProduct,
                    AvgRating = AvgRatingStar(relatedProduct)
                });
            }
            // Lấy ra tổng số bình luận của sản phẩm, và tổng số trang bình luận
            ViewBag.TotalComment = commentQuery.Count();
            ViewBag.TotalPage = (int)Math.Ceiling((double)commentQuery.Count() / PER_PAGE);

            // Lấy ra danh sách bình luận sau khi xử lý tất cả điều kiện
            List<Comment> commentList = commentQuery.Skip(PER_PAGE * (page - 1)).Take(PER_PAGE).ToList();

            // Dữ liệu View Model
            DetailProductVM detailProductVM = new DetailProductVM()
            {
                ProductVM = productVM,
                Comments = commentList,
                RelatedProduct = relatedProductsVM
            };

            return View(detailProductVM);
        }

        [HttpPost]
        public async Task<ActionResult> Comment(Comment comment, string CustomerDisplayName, string CustomerAvatar)
        {
            // Lấy giá trị từ appsettings.json
            var AppVar = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings");



            // Thêm bình luận vào DB
            _db.Comments.Add(comment);
            _db.SaveChanges();

            // pusher options
            var options = new PusherOptions();
            options.Cluster = AppVar["Pusher_Cluster"];

            // Trigger pusher
            var pusher = new Pusher(AppVar["Pusher_AppId"], AppVar["Pusher_AppKey"], AppVar["Pusher_AppSecret"], options);
            ITriggerResult result = await pusher.TriggerAsync("comments_channel", "push_comment_event", new { comment, CustomerDisplayName, CustomerAvatar });

            return Json(new { success = true });
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Order()
        {
            List<Product> cartList = new List<Product>();

            string userId = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            Customer customer = _db.Customers.Include(c => c.Addresses).FirstOrDefault(c => c.Id == int.Parse(userId));
            if (HttpContext.Session.GetString("ShoppingCart") != null)
            {
                cartList = JsonConvert.DeserializeObject<List<Product>>(
                        HttpContext.Session.GetString("ShoppingCart")
                    );
            }

            OrderVM orderVM = new OrderVM()
            {
                Products = cartList,
                Customer = customer
            };
            return View(orderVM);
        }

        [HttpPost]
        public ActionResult Order(string customerAddress)
        {
            Dictionary<int, List<Product>> orders = new Dictionary<int, List<Product>>();
            Dictionary<int, int> total = new Dictionary<int, int>();

            // Lỗi khi đặt hàng nhưng không chọn địa chỉ
            if (customerAddress == "0" || string.IsNullOrEmpty(customerAddress))
            {
                TempData["Failed"] = "Vui lòng chọn địa chỉ để tiếp tục đặt hàng.";
                return RedirectToAction("Order");
            }
            // Lấy ra địa chỉ người dùng chọn
            Address foundAddress = _db.Addresses.FirstOrDefault(a => a.Id == int.Parse(customerAddress));
            if (foundAddress is null)
            {
                return NotFound();
            }

            List<Product> cartList = new List<Product>();

            // Lấy User đặt hàng
            string userId = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            Customer customer = _db.Customers.Include(c => c.Addresses).FirstOrDefault(c => c.Id == int.Parse(userId));
            if (HttpContext.Session.GetString("ShoppingCart") != null)
            {
                cartList = JsonConvert.DeserializeObject<List<Product>>(
                        HttpContext.Session.GetString("ShoppingCart")
                    );
            }

            // Lỗi khi không có sản phẩm trong giỏ hàng
            if (cartList.Count == 0)
            {
                TempData["Failed"] = "Vui lòng thêm sản phẩm vào giỏ hàng.";
                return RedirectToAction("Order");
            }

            // Phân loại sản phẩm theo snackbarId
            foreach (Product item in cartList)
            {
                int quantity = (int)HttpContext.Session.GetInt32($"Quantity_{item.Id}");

                if (orders.ContainsKey(item.SnackBarId))
                {
                    orders[item.SnackBarId].Add(item);
                    total[item.SnackBarId] = total[item.SnackBarId] + (quantity * item.UnitPrice);
                }
                else
                {
                    orders.Add(item.SnackBarId, new List<Product> { item });
                    total.Add(item.SnackBarId, item.UnitPrice * quantity);
                }
            }

            foreach (KeyValuePair<int, List<Product>> kvp in orders)
            {

                // Tạo order cho mỗi snackbar
                Order newOrder = new Order()
                {
                    CustomerId = customer.Id,
                    Total = total[kvp.Key],
                    Status = false,
                    Address = $"{foundAddress.NoAndStreet}, {foundAddress.WardName}, {foundAddress.DistrictName}"
                };
                _db.Add(newOrder);
                _db.SaveChanges();

                // Thêm sản phẩm vào order vừa tạo
                foreach (Product product in kvp.Value)
                {
                    _db.OrderDetails.Add(new OrderDetail()
                    {
                        ProductId = product.Id,
                        SnackBarId = product.SnackBarId,
                        OrderId = newOrder.Id,
                        Quantity = (int)HttpContext.Session.GetInt32($"Quantity_{product.Id}"),
                        Price = product.UnitPrice
                    });

                    // Xóa số lượng trong session  
                    HttpContext.Session.Remove($"Quantity_{product.Id}");
                }
                _db.SaveChanges();

            }

            // Xóa tất cả sản phẩm trong giỏ hàng
            HttpContext.Session.Remove("ShoppingCart");

            TempData["Success"] = "Bạn đã đặt hàng thành công. Đang chờ quầy ăn xử lý.";

            return RedirectToAction("Order");
        }

        #region API
        [AllowAnonymous]
        public IActionResult Comment(int productId, int page, string sort)
        {
            const int PER_PAGE = 10;
            IQueryable<Comment> commentQuery = _db.Comments.Include(c => c.Customer).AsQueryable();

            if (page < 1)
            {
                page = 1;
            }

            if (productId > 0)
            {
                commentQuery = commentQuery.Where(c => c.ProductId == productId);
            }

            commentQuery = (string.IsNullOrEmpty(sort) || sort == "desc") ? commentQuery.OrderByDescending(p => p.CreatedAt) : commentQuery.OrderBy(p => p.CreatedAt);

            var comments = commentQuery.Select(p =>
                new { p.Content, p.LikeQuantity, p.CreatedAt, p.Customer.Id, p.Customer.DisplayName, p.Customer.Avatar })
                .Skip(PER_PAGE * (page - 1))
                .Take(PER_PAGE).ToList();

            return Json(new { data = comments });
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            List<Product> cartList;
            if (HttpContext.Session.Get("ShoppingCart") == null)
            {
                cartList = new List<Product>();

                Product product = _db.Products.Include(p => p.SnackBar)
                    .Include(p => p.ProductImages)
                    .FirstOrDefault(p => p.Id == id);

                cartList.Add(product);


                HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(cartList, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

                HttpContext.Session.SetInt32($"Quantity_{product.Id}", quantity);
            }
            else
            {
                bool flag = false;

                cartList = JsonConvert.DeserializeObject<List<Product>>(
                    HttpContext.Session.GetString("ShoppingCart")
                );
                foreach (Product cartItem in cartList)
                {
                    if (cartItem.Id == id)
                    {
                        int oldQuantity = (int)HttpContext.Session.GetInt32($"Quantity_{id}");
                        HttpContext.Session.SetInt32($"Quantity_{id}", oldQuantity + quantity);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    Product product = _db.Products.Include(p => p.SnackBar)
                        .Include(p => p.ProductImages)
                        .FirstOrDefault(p => p.Id == id);

                    cartList.Add(product);

                    HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(cartList, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
                    HttpContext.Session.SetInt32($"Quantity_{product.Id}", quantity);
                }
            }

            List<Product> ls = JsonConvert.DeserializeObject<List<Product>>(
                    HttpContext.Session.GetString("ShoppingCart")
                );

            int cartCount = ls.Count;


            return Json(new { ItemAmount = cartCount });
        }

        [HttpPost]
        public ActionResult DeleteFromCart(int id)
        {
            List<Product> cartList = JsonConvert.DeserializeObject<List<Product>>(
                    HttpContext.Session.GetString("ShoppingCart")
                );
            foreach (Product item in cartList)
            {
                if (item.Id == id)
                {
                    cartList.Remove(item);
                    HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(cartList, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
                    HttpContext.Session.Remove($"Quantity_{id}");

                    break;
                }
            }

            int cartCount = cartList.Count;

            return Json(new { ItemAmount = cartCount });
        }
        #endregion
    }
}
