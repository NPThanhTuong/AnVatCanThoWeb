using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using PusherServer;
using System.Net;

namespace AnVatCanThoWeb.Controllers
{
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
        public IActionResult Index(string[]? categoryFilter, string? sort, string? search, int page)
        {
            List<Product> productList = _db.Products.Include(o => o.Ratings).Include(p => p.SnackBar).ToList();
            List<ProductCategory> categoryList = _db.ProductCategories.ToList();
            List<ProductVM> productVM = new List<ProductVM>();

            const int PER_PAGE = 9;

            foreach (Product product in productList)
            {

                productVM.Add(new ProductVM()
                {
                    Product = product,
                    AvgRating = AvgRatingStar(product)
                });

            }

            // Xử lý lọc sản phẩm
            if (categoryFilter.Length > 0)
            {
                productVM = productVM.Where(p => categoryFilter.Contains(p.Product.ProductCategoryId.ToString())).ToList();
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

            // Tìm tên của sản phẩm
            if (!string.IsNullOrEmpty(search))
            {
                productVM = productVM.Where(delegate (ProductVM pVM)
                {
                    if (ConvertToUnSign(pVM.Product.Name).IndexOf(ConvertToUnSign(search), StringComparison.CurrentCultureIgnoreCase) >= 0)
                        return true;
                    else
                        return false;
                }).ToList();

                //productVM = productVM.Where(p => p.Product.Name.Contains(search.Trim(), System.StringComparison.CurrentCultureIgnoreCase)).ToList();
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

        public IActionResult Details(int? id)
        {
            const int RELATED_PRODUCT_NUMBER = 4;
            List<ProductVM> relatedProductsVM = new List<ProductVM>();

            if (id == null)
            {
                return NotFound();
            }

            Product product = _db.Products
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Customer)
                .Include(p => p.ProductImages)
                .Include(p => p.Ratings)
                .Include(p => p.SnackBar)
                    .ThenInclude(s => s.Addresses)
                .FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            // Chi tiết sản phẩm chính
            ProductVM productVM = new ProductVM()
            {
                Product = product,
                AvgRating = AvgRatingStar(product)
            };

            // Những sản phẩm có liên quan
            List<Product> relatedProducts = _db.Products
                .Include(p => p.Ratings)
                .Where(p => p.ProductCategoryId == product.ProductCategoryId)
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

            // Dữ liệu View Model
            DetailProductVM detailProductVM = new DetailProductVM()
            {
                ProductVM = productVM,
                RelatedProduct = relatedProductsVM
            };

            return View(detailProductVM);
        }

        public IActionResult Comment()
        {
            List<Comment> comments = _db.Comments.ToList();
            return Json(new { data = comments });
        }

        [HttpPost]
        public async Task<ActionResult> Comment(Comment data)
        {
            _db.Comments.Add(data);
            _db.SaveChanges();
            var options = new PusherOptions();
            options.Cluster = "ap1";
            var pusher = new Pusher("1788154", "a6a81aa4ce5b84db0557", "206be8baf48febe8923c", options);
            ITriggerResult result = await pusher.TriggerAsync("comments_channel", "push_comment_event", data);
            return Json(new { success = true });
        }

        public IActionResult GetAllProduct()
        {
            List<Product> productList = _db.Products.ToList();
            List<Rating> ratingsList = _db.Ratings.ToList();
            return Json(new { data = productList, ratings = ratingsList });
        }

        //app_id = "1788154"
        //key = "a6a81aa4ce5b84db0557"
        //secret = "206be8baf48febe8923c"
        //cluster = "ap1"
    }
}
