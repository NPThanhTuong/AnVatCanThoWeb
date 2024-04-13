using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

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

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /product (list all product)
        public IActionResult Index(string[]? categoryFilter, string? sort, string? search, int page)
        {
            List<Product> productList = _db.Products.Include(o => o.Ratings).ToList();
            List<ProductCategory> categoryList = _db.ProductCategories.ToList();
            List<ProductVM> productVM = new List<ProductVM>();
            
            const int PER_PAGE = 1;
            
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
            if(!string.IsNullOrEmpty(search))
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
            ViewBag.TotalPage = productVM.Count;

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

        public IActionResult GetAllProduct()
        {
            List<Product> productList = _db.Products.ToList();
            List<Rating> ratingsList = _db.Ratings.ToList();
            return Json(new { data = productList, ratings = ratingsList });
        }
    }
}
