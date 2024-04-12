using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AnVatCanThoWeb.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /product (list all product)
        public IActionResult Index(string[] categoryFilter, string sort)
        {

            List<Product> productList = _db.Products.Include(o => o.Ratings).ToList();
            List<ProductCategory> categoryList = _db.ProductCategories.ToList();
            string[] defaultCategory = new string[categoryFilter.Length];

            List<ProductVM> productVM = new List<ProductVM>();

            foreach (var product in productList)
            {
                double sum = 0;
                int count = 0;
                foreach (var starItem in product.Ratings)
                {
                    sum += starItem.Star;
                    count++;
                }

                productVM.Add(new ProductVM()
                {
                    Product = product,
                    AvgRating = sum / product.Ratings.Count
                });

            }

            // Xử lý lọc sản phẩm
            if (categoryFilter.Length > 0)
            {
                categoryFilter.CopyTo(defaultCategory, 0);

                productVM = productVM.Where(p => defaultCategory.Contains(p.Product.ProductCategoryId.ToString())).ToList();
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

            // Phân trang

            // Hiển thị danh sách các loại sản phẩm
            ViewBag.CategoryList = categoryList;

            return View(productVM);
        }

        public IActionResult GetAllProduct()
        {
            List<Product> productList = _db.Products.ToList();
            List<Rating> ratingsList = _db.Ratings.ToList();
            return Json(new { data = productList, ratings = ratingsList });
        }
    }
}
