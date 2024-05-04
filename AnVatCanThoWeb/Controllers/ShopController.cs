using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;

        private double AvgRatingStar(Product product)
        {
            double sum = 0;

            foreach (var starItem in product.Ratings)
            {
                sum += starItem.Star;
            }
            return sum / product.Ratings.Count;
        }

        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Details(int id, int page)
        {
            List<ProductVM> productsVM = new List<ProductVM>();
            const int PER_PAGE = 8;

            if (id <= 0)
            {
                return NotFound();
            }
            
            if(page <= 0)
            {
                page = 1;
            }

            SnackBar snackBar = _db.SnackBars.FirstOrDefault(s => s.Id == id);

            if (snackBar is null)
            {
                return NotFound();
            }

            var productQuery = _db.Products.Include(p => p.Ratings)
                .Include(p => p.ProductImages)
                .Where(p => p.SnackBarId == id).AsQueryable();

            ViewBag.TotalPage = (int)Math.Ceiling((decimal)productQuery.Count() / PER_PAGE);
            
            List<Product> products = productQuery
                .Skip(PER_PAGE * (page - 1))
                .Take(PER_PAGE)
                .ToList();


            foreach (Product product in products)
            {
                productsVM.Add(new ProductVM()
                {
                    Product = product,
                    AvgRating = AvgRatingStar(product)
                });
            }


            DetailSnackBarVM detailProductVM = new DetailSnackBarVM()
            {
                ProductsVM = productsVM,
                SnackBar = snackBar
            };

            return View(detailProductVM);
        }
    }
}
