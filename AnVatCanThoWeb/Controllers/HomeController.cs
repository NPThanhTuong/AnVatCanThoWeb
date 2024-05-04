using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.Common.Authentication;
using AnVatCanThoWeb.Models;
using AnVatCanThoWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace AnVatCanThoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.UserScheme)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _db = db;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
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

        [AllowAnonymous]
        public IActionResult Index()
        {
            // lấy ra 3 sản phẩm có ratings cao nhất

            List<Product> products = _db.Products.Include(p => p.SnackBar)
                .Include(p => p.Ratings)
                .Include(p => p.ProductImages)
                .OrderByDescending(s => s.Ratings.Average(r => r.Star))
                .Take(4)
                .ToList();
            List<ProductVM> listProductVM = new List<ProductVM>();

            foreach (Product product in products)
            {
                listProductVM.Add(new ProductVM()
                {
                    Product = product,
                    AvgRating = AvgRatingStar(product)
                });
            }

            List<SnackBar> snackBars = _db.SnackBars.Take(3).ToList();

            HomeVM homeVM = new()
            {
                topProducts = listProductVM,
                topSnackBars = snackBars
            };

            return View(homeVM);
        }

        public IActionResult Profile()
        {
            string userId = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            Customer customer = _db.Customers.Include(c => c.Addresses).FirstOrDefault(c => c.Id == int.Parse(userId));
            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult EditProfile()
        {
            string userId = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            Customer customer = _db.Customers.Include(c => c.Addresses).FirstOrDefault(c => c.Id == int.Parse(userId));
            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(Customer customer, IFormFile? fileUpload)
        {
            string wwwRootPath = _env.WebRootPath;

            Customer foundCustomer = _db.Customers.FirstOrDefault(c => c.Id == customer.Id);
            if (foundCustomer is null)
            {
                return NotFound();
            }

            if (fileUpload is not null)
            {
                if (fileUpload.ContentType == "image/jpeg" || fileUpload.ContentType == "image/png")
                {
                    string fileName = Path.GetFileNameWithoutExtension(fileUpload.FileName) +
                        "_" + Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName);

                    string savePath = Path.Combine(wwwRootPath, "images", "User");

                    if (customer.Avatar != "no-avatar.jpg")
                    {
                        var oldImagePath = Path.Combine(savePath, customer.Avatar);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
                    {
                        fileUpload.CopyTo(fileStream);
                    };

                    customer.Avatar = fileName;
                }
            }

            foundCustomer.DisplayName = customer.DisplayName;
            foundCustomer.Dob = customer.Dob;
            foundCustomer.Email = customer.Email;
            foundCustomer.Tel = customer.Tel;
            foundCustomer.Avatar = customer.Avatar;

            _db.Update(foundCustomer);
            _db.SaveChanges();

            // Refresh Claims
            await HttpContext.SignOutAsync(ApplicationAuthenticationScheme.UserScheme);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, foundCustomer.Email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Sid, foundCustomer.Id.ToString()),
                new Claim(ClaimTypes.Name, foundCustomer.DisplayName),
                new Claim(ClaimTypes.UserData, foundCustomer.Avatar),
            };

            var claimsIdentity = new ClaimsIdentity(claims, ApplicationAuthenticationScheme.UserScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                AllowRefresh = true,
            };
            await HttpContext.SignInAsync(
                ApplicationAuthenticationScheme.UserScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
            TempData["Success"] = "Cập nhật hồ sơ thành công";

            return RedirectToAction("EditProfile");
        }

        public IActionResult Order()
        {
            string userId = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            List<Order> orderList = _db.Orders.Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                        .ThenInclude(p => p.ProductImages)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(d => d.SnackBar)
                    .Where(o => o.CustomerId == int.Parse(userId)).ToList();

            return View(orderList);
        }

        [HttpPost]
        public IActionResult Rating(Rating rating)
        {
            Rating foundRating = _db.Ratings.FirstOrDefault(r => r.ProductId == rating.ProductId &&
                r.SnackBarId == rating.ProductId &&
                r.CustomerId == rating.CustomerId
            );

            if (foundRating is not null) {
                _db.Add(rating);
                _db.SaveChanges();
                TempData["Success"] = "Đánh giá sản phẩm thành công";
                return RedirectToAction("Order");
            }
            else
            {
                TempData["Failed"] = "Bạn đã đánh giá sản phẩm này.";
                return RedirectToAction("Order");
            }

            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
