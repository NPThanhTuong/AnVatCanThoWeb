using Microsoft.AspNetCore.Mvc;
using AnVatCanThoWeb.Areas.SnackBar.Common;
using AnVatCanTho.DataAccess.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AnVatCanThoWeb.Common.Authentication;
using System.Security.Claims;
using AnVatCanTho.Models;

namespace AnVatCanThoWeb.Areas.SnackBar.Controllers;

[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.SnackBarScheme)]
[Area(SnackbarAreaName.Value)]
public class ProductsController : Controller
{

    private readonly ApplicationDbContext _dbContext;
    private readonly IWebHostEnvironment _env;
    public ProductsController(ApplicationDbContext dbContext, IWebHostEnvironment env)
    {
        _dbContext = dbContext;
        _env = env;
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
    public async Task<IActionResult> CreateProductAsync()
    {
        var categories = await _dbContext.ProductCategories.ToListAsync();
        var selectList = categories
            .Select(x => new SelectListItem(text: x.Name, value: x.Id.ToString()));
        ViewBag.Categories = selectList;
        return View();
    }

    [HttpPost,ActionName("CreateProduct")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(Product product, IFormFileCollection fileUploads, ProductImage productImage)
    {
        if(ModelState.IsValid)
        {
            int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            product.SnackBarId = userId;
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            if (fileUploads != null)
            {
                foreach (var fileUpload in fileUploads) {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var filePath = Path.Combine(_env.WebRootPath,"images/Products", fileName);
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
                        PathName = fileUpload.FileName
                    };
                    _dbContext.ProductImages.Add(producimage);
                }
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index), product);
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var product = await _dbContext.Products
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if(product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost, ActionName("EditProduct")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(int id, Product product)
    {
        if(id != product.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Error Update Data");
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
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
        _dbContext.Products.Remove(product);
        _dbContext.SaveChangesAsync();
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
    public ActionResult CreateProductImages()
    {
        return View();
    }
    [HttpPost,ActionName("CreateProductImages")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateProductImages(int id,IFormFileCollection fileUploads, ProductImage productImage)
    {
        if(ModelState.IsValid)
        {
            int userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            if (fileUploads != null)
            {
                foreach (var fileUpload in fileUploads)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
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
                    productImage.PathName = fileUpload.FileName;
                    _dbContext.ProductImages.Add(productImage);
                    await _dbContext.SaveChangesAsync();
                }
                return RedirectToAction("EditProductImages", new { id = id});
            }
        }
        return View(productImage);
    }
}
