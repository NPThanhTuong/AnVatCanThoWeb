using Microsoft.AspNetCore.Mvc;
using AnVatCanThoWeb.Areas.SnackBar.Common;
using AnVatCanTho.DataAccess.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AnVatCanThoWeb.Common.Authentication;
using System.Security.Claims;
using AnVatCanTho.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AnVatCanThoWeb.Areas.SnackBar.ViewModels.Product;

namespace AnVatCanThoWeb.Areas.SnackBar.Controllers;

[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.SnackBarScheme)]
[Area(SnackbarAreaName.VALUE)]
public class ProductsController : Controller
{

    private readonly ApplicationDbContext _dbContext;
    private readonly IWebHostEnvironment _env;
    public ProductsController(ApplicationDbContext dbContext, IWebHostEnvironment env)
    {
        _dbContext = dbContext;
        _env = env;
    }
    //Random chuoi ki tu
    private static Random random = new Random();
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string GenerateRandomString(int length)
    {
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public async Task<IActionResult> Index()
    {
        int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        var products = await _dbContext.Products
            .Where(s => s.SnackBar.Id == userId)
            .Include(p => p.ProductCategory)
            .ToListAsync();
        return View(products);
    }
    public async Task<IActionResult> CreateProduct()
    {
        var categories = await _dbContext.ProductCategories.ToListAsync();
        var selectList = categories
            .Select(x => new SelectListItem(text: x.Name, value: x.Id.ToString()));
        ViewBag.Categories = selectList;
        return View();
    }

    [HttpPost,ActionName("CreateProduct")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(CreateProductViewModel vm, IFormFileCollection fileUploads, ProductImage productImage)
    {
        int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        var categories = await _dbContext.ProductCategories.ToListAsync();
        var selectList = categories
            .Select(x => new SelectListItem(text: x.Name, value: x.Id.ToString()));
        ViewBag.Categories = selectList;
        if (ModelState.IsValid)
        {
            var product = new Product 
            {
                SnackBarId = userId,
                Stock = (int)vm.Stock,
                Name = vm.Name,
                Description = vm.Description,
                UnitPrice = (int)vm.UnitPrice,
                ProductCategoryId = vm.ProductCategoryId,
                Ingredient = vm.Ingredient
            };
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            if (fileUploads != null)
            {
                foreach (var fileUpload in fileUploads)
                {
                    var fileName = GenerateRandomString(10) + "-" + Path.GetFileName(fileUpload.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images/Products", fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            fileUpload.CopyTo(stream);
                        }
                    }
                    var producimage = new ProductImage
                    {
                        SnackBarId = userId,
                        ProductId = product.Id,
                        PathName = fileName
                    };
                    _dbContext.ProductImages.Add(producimage);
                }
            }
            await _dbContext.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index), product);
        }
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int? id)
    {
        int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        ViewData["snackbarid"] = userId;
        if (id == null)
        {
            return NotFound();
        }
        var categories = await _dbContext.ProductCategories.ToListAsync();
        var selectList = categories
            .Select(x => new SelectListItem(text: x.Name, value: x.Id.ToString()));
        ViewBag.Categories = selectList;
        var product = await _dbContext.Products
            .Include(p => p.SnackBar)
            .FirstOrDefaultAsync(p => p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        var vm = new CreateProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Ingredient = product.Ingredient,
            ProductCategoryId = product.ProductCategoryId,
            Stock = product.Stock,
            UnitPrice = product.UnitPrice,
            SnackBarId = product.SnackBarId,
        };
        return View(vm);
    }

    [HttpPost, ActionName("EditProduct")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(int id, CreateProductViewModel vm)
    {
        var categories = await _dbContext.ProductCategories.ToListAsync();
        var selectList = categories
            .Select(x => new SelectListItem(text: x.Name, value: x.Id.ToString()));
        ViewBag.Categories = selectList;
        int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        ViewData["snackbarid"] = userId;
        if (id != vm.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null) 
                {
                    return NotFound();
                }
                product.SnackBarId = userId;
                product.Description = vm.Description;
                product.Ingredient = vm.Ingredient;
                product.ProductCategoryId = vm.ProductCategoryId;
                product.Stock = (int)vm.Stock;
                product.UnitPrice = (int)vm.UnitPrice;
                product.Name = vm.Name;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Error Update Data");
            }
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpGet]    
    public async Task<ActionResult> DetailsAsync(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var viewModel = await _dbContext.Products
            .Include(s => s.ProductCategory)
            .Include(n => n.SnackBar)
            .Include(c => c.ProductImages)
            .Where(p => p.Id == id)
            .SingleOrDefaultAsync();
        if(viewModel == null)
        {
            return NotFound();
        }
        return View(viewModel);
    }
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _dbContext.Products
            .Include(p => p.ProductCategory)
            .SingleOrDefaultAsync(p => p.Id == id);
        if(product == null)
        {
            Response.StatusCode = 404;
            return null;
        }
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirm(int id)
    {
        var product = await _dbContext.Products
            .SingleOrDefaultAsync(p => p.Id ==id);
        if (product == null)
        {
            Response.StatusCode = 404;
            return null;
        }
        int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        var producimages = await _dbContext.ProductImages
            .Where(p => p.SnackBarId == userId && p.ProductId == id)
            .ToListAsync();
        var productImages = product.ProductImages;
        foreach (var producImage in productImages) {
            string filePath = Path.Combine(_env.WebRootPath, "images/Products", producImage.PathName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        foreach(var producimage in producimages)
        {
            _dbContext.ProductImages.Remove(producimage);
            await _dbContext.SaveChangesAsync();
        }
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<ActionResult> EditProductImages(int id)
    {
        ViewData["id"] = id;
        if(id == null)
        {
            return NotFound();
        }
        int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        var productImages = await _dbContext.ProductImages
            .Where(s => s.ProductId == id && s.SnackBarId == userId)
            .ToListAsync();
        if(productImages == null)
        {
            return NotFound();
        }
        return View(productImages);    
    }
    [HttpGet]
    public async Task<ActionResult> DeleteProductImage(string id)
    {
        var productImage = await _dbContext.ProductImages
            .SingleOrDefaultAsync(p => p.PathName == id);
        string filePath = Path.Combine(_env.WebRootPath, "images/Products", id);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        if (productImage == null)
        {
            return NotFound();
        }

        _dbContext.ProductImages.Remove(productImage);
        _dbContext.SaveChangesAsync();
        return RedirectToAction("EditProductImages", new {id = productImage.ProductId});
    }
    public ActionResult CreateProductImages(int id)
    {
        ViewData["id"] = id;
        return View();
    }
    [HttpPost,ActionName("CreateProductImages")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateProductImages(int id,IFormFileCollection fileUploads, ProductImage productImage)
    {
        ViewData["id"] = id;
        if(ModelState.IsValid)
        {
            int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            if (fileUploads != null)
            {
                foreach (var fileUpload in fileUploads)
                {
                    var fileName = GenerateRandomString(10) + "-" + Path.GetFileName(fileUpload.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images/Products", fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            fileUpload.CopyTo(stream);
                        }
                    }
                    productImage.ProductId = id;
                    productImage.SnackBarId = userId;
                    productImage.PathName = fileName;
                    _dbContext.ProductImages.Add(productImage);
                    await _dbContext.SaveChangesAsync();
                }
                return RedirectToAction("EditProductImages", new { id = id});
            }
        }
        return View();
    }
}
