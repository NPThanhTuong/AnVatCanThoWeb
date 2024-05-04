using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.Common.Authentication;
using AnVatCanThoWeb.Models;
using AnVatCanThoWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AnVatCanThoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.UserScheme)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
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

            foreach(Product product in products) {
                listProductVM.Add(new ProductVM()
                {
                    Product = product,
                    AvgRating = AvgRatingStar(product)
                });
            }

            List<SnackBar> snackBars = _db.SnackBars.Take(3).ToList();

            HomeVM homeVM = new () { 
                topProducts = listProductVM,
                topSnackBars = snackBars
            };

            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
