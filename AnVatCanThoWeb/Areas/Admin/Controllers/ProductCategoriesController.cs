using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.Areas.Admin.Common;
using AnVatCanThoWeb.Areas.Admin.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
public class ProductCategoriesController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ProductCategoriesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index([FromQuery] int? defaultTypeId)
    {
        var productTypes = await _dbContext.ProductTypes
            .ToListAsync();
        var selectListItems = productTypes
            .Select(x => new SelectListItem(text: x.Name, 
                value: x.Id.ToString(), 
                selected: x.Id == defaultTypeId ));
        ViewData["ProductTypes"] = selectListItems;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["ProductTypes"] = await GetProductTypeListItems();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryViewModel vm)
    {
        ViewData["ProductTypes"] = await GetProductTypeListItems();

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        if(!await _dbContext.ProductTypes.AnyAsync(x => x.Id == vm.ProductTypeId))
        {
            ModelState.AddModelError(nameof(vm.ProductTypeId), "Mã kiểu sản phẩm không tồn tại");
            return View(vm);
        }

        var newCate = new ProductCategory
        {
            ProductTypeId = vm.ProductTypeId,
            Name = vm.Name!,
            Description = vm.Description!,
        };
        await _dbContext.ProductCategories.AddAsync(newCate);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Index", new {defaultTypeId = vm.ProductTypeId});
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _dbContext.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == id);
        if(category is null)
        {
            return NotFound();
        }
        
        ViewData["ProductTypes"] = await GetProductTypeListItems();

        var vm = new CategoryViewModel(
            category.ProductTypeId, 
            category.Name, 
            category.Description);

        return View(vm);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryViewModel vm)
    {
        ViewData["ProductTypes"] = await GetProductTypeListItems();
        
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var category = await _dbContext.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == id);
        if (category is null || 
            !await _dbContext.ProductTypes.AnyAsync(x => x.Id == vm.ProductTypeId))
        {
            return View(vm);
        }

        category.ProductTypeId = vm.ProductTypeId;
        category.Description = vm.Description!;
        category.Name = vm.Name!;

        await _dbContext.SaveChangesAsync();
        
        return RedirectToAction("Index", new {defaultTypeId = vm.ProductTypeId});
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var category = await _dbContext.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == id);
        if (category is null)
        {
            return NotFound();
        }

        try
        {
            _dbContext.ProductCategories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Vui lòng xoá các sản phẩm thuộc danh mục này";
        }
        
        return RedirectToAction("Index", new {defaultTypeId = category.ProductTypeId} );
    }
    
    [HttpGet("/[area]/api/product-types/{id:int}/product-categories")]
    public async Task<IActionResult> GetAllProductCategories(int id)
    {
        var productCategories = await _dbContext.ProductCategories
            .Where(x => x.ProductType.Id == id)
            .ToListAsync();
        return Ok(productCategories);
    }

    private async Task<IEnumerable<SelectListItem>> GetProductTypeListItems()
    {
        var productTypes = await _dbContext.ProductTypes
            .ToListAsync();
        var selectListItems = productTypes
            .Select(x => new SelectListItem(text: x.Name, value: x.Id.ToString()));
        return selectListItems;
    }
}