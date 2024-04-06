using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.Admin.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
[Route("[area]/product-categories")]
public class ProductCategoriesController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ProductCategoriesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var productTypes = await _dbContext.ProductTypes
            .ToListAsync();
        var selectListItems = productTypes
            .Select(x => new SelectListItem(text: x.Name, value: x.Id.ToString()));
        ViewData["ProductTypes"] = selectListItems;
        return View();
    }
    
    [HttpGet("/[area]/api/product-types/{id:int}/product-categories")]
    public async Task<IActionResult> GetAllProductCategories(int id)
    {
        var productCategories = await _dbContext.ProductCategories
            .Where(x => x.ProductType.Id == id)
            .ToListAsync();
        return Ok(productCategories);
    }
}