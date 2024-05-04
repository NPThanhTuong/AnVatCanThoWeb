using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.Admin.Common;
using AnVatCanThoWeb.Areas.Admin.ViewModels.Common;
using AnVatCanThoWeb.Areas.Admin.ViewModels.Snackbars;
using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.AdminScheme)]
public class SnackbarsController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public SnackbarsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index([FromQuery] GetAllSnackbarsRequest request)
    {
        const int pageSize = 10;

        var requestPageIndex = request.PageIndex;
        var requestKeyword = request.Keyword;
        var requestCurrentKeyword = request.CurrentKeyword;

        var requestDistrictName = request.DistrictName;
        var requestWardName = request.WardName;
        var requestCurrentDistrictName = request.CurrentDistrictName;
        var requestCurrentWardName = request.CurrentWardName;

        if (requestKeyword is not null
            || requestDistrictName is not null
            || requestWardName is not null)
        {
            requestPageIndex = 1;
        }

        requestKeyword ??= requestCurrentKeyword;
        requestDistrictName ??= requestCurrentDistrictName;
        requestWardName ??= requestCurrentWardName;

        ViewData["CurrentKeyword"] = requestKeyword;
        ViewData["CurrentWardName"] = requestWardName;
        ViewData["CurrentDistrictName"] = requestDistrictName;

        var snackbars = _dbContext.SnackBars
            .Include(x => x.Addresses)
            .AsQueryable();

        if (!string.IsNullOrEmpty(requestDistrictName))
        {
            snackbars = snackbars
                .Where(x => x.Addresses
                    .Any(y => y.DistrictName.Equals(requestDistrictName)));
        }

        if (!string.IsNullOrEmpty(requestWardName))
        {
            snackbars = snackbars
                .Where(x => x.Addresses
                    .Any(y => y.WardName.Equals(requestWardName)));
        }

        if (!string.IsNullOrEmpty(requestKeyword))
        {
            snackbars = snackbars
                .Where(x => x.Email.Contains(requestKeyword)
                            || x.Description.Contains(requestKeyword)
                            || x.DisplayName.Contains(requestKeyword)
                            || x.Tel.Contains(requestKeyword)
                            || x.Username.Contains(requestKeyword)
                            || x.Addresses
                                .Any(y => y.NoAndStreet.Contains(requestKeyword)));
        }

        var totalRows = await snackbars.CountAsync();
        var totalPages = (int)Math.Ceiling((float)totalRows / pageSize);

        var snackbarVms = await snackbars.Skip((requestPageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new SnackbarViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Dob = x.Dob,
                Email = x.Email,
                Tel = x.Tel,
                Username = x.Username,
                DisplayName = x.DisplayName,
                Addresses = x.Addresses.Select(y => new AddressViewModel(
                    y.NoAndStreet, y.DistrictName, y.WardName))
            }).ToListAsync();

        var response = new GetAllSnackbarsResponse
        {
            Data = snackbarVms,
            TotalPages = totalPages,
            PageIndex = requestPageIndex,
        };

        return View(response);
    }

    public async Task<IActionResult> Details([FromRoute] int id)
    {
        var snackbar = await _dbContext.SnackBars
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (snackbar is null)
        {
            return NotFound();
        }

        var snackbarViewModel = new SnackbarViewModel
        {
            Id = snackbar.Id,
            Description = snackbar.Description,
            Avatar = snackbar.Avatar,
            CoverImage = snackbar.CoverImage,
            Dob = snackbar.Dob,
            Email = snackbar.Email,
            Tel = snackbar.Tel,
            Username = snackbar.Username,
            DisplayName = snackbar.DisplayName,
            Addresses = snackbar.Addresses.Select(y => new AddressViewModel(
                y.NoAndStreet, y.DistrictName, y.WardName))
        };

        return View(snackbarViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] int snackbarId)
    {
        var snackbar = await _dbContext.SnackBars
            .FirstOrDefaultAsync(x => x.Id == snackbarId);
        if (snackbar is null)
        {
            return NotFound();
        }

        var addresses = await _dbContext.Addresses
            .Where(x => x.SnackBarId == snackbarId).ToListAsync();
        
        try
        {
            _dbContext.SnackBars.Remove(snackbar);
            _dbContext.Addresses.RemoveRange(addresses);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Không thể xoá quầy vì liên quan đến các đơn hàng";
            return RedirectToAction("Details", new { id = snackbarId });
        }
        
        return RedirectToAction("Index");
    }

    [Route("/[area]/[controller]/details/{id:int}/products")]
    public async Task<IActionResult> Products([FromRoute] int id)
    {
        var snackbar = await _dbContext.SnackBars
            .FirstOrDefaultAsync(x => x.Id == id);
        if (snackbar is null)
        {
            return NotFound();
        }

        ViewData["Snackbar"] = snackbar;
        var products = await _dbContext.Products
            .Include(x => x.ProductCategory)
            .ThenInclude(x => x.ProductType)
            .Where(x => x.SnackBarId == id)
            .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                Ingredient = x.Ingredient,
                Stock = x.Stock,
                ProductCategoryName = x.ProductCategory.Name,
                ProductTypeName = x.ProductCategory.ProductType.Name,
            })
            .ToListAsync();

        return View(products);
    }

    [Route("/[area]/[controller]/details/{id:int}/products/{productId:int}")]
    public async Task<IActionResult> ProductDetails(int id, int productId)
    {
        var snackbar = await _dbContext.SnackBars
            .FirstOrDefaultAsync(x => x.Id == id);
        if (snackbar is null)
        {
            return NotFound();
        }

        var product = await _dbContext.Products
            .Include(x => x.ProductImages)
            .Include(x => x.ProductCategory)
            .ThenInclude(x => x.ProductType)
            .FirstOrDefaultAsync(x => x.Id == productId && x.SnackBarId == id);
        if (product is null)
        {
            return NotFound();
        }

        var productDetails = new ProductDetailsViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            UnitPrice = product.UnitPrice,
            Ingredient = product.Ingredient,
            Stock = product.Stock,
            ProductCategoryName = product.ProductCategory.Name,
            ProductTypeName = product.ProductCategory.ProductType.Name,
            ProductImages = product.ProductImages.Select(x => x.PathName)
        };

        ViewData["Snackbar"] = snackbar;

        return View(productDetails);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct([FromForm] int snackbarId, [FromForm] int productId)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == productId);
        if (product is null)
        {
            return NotFound();
        }

        var comments = await _dbContext.Comments.Where(x => x.SnackBarId == snackbarId)
            .ToListAsync();

        try
        {
            _dbContext.Comments.RemoveRange(comments);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Không thể xoá sản phẩm vì liên quan đến các đơn hàng, bình luận và đánh giá";
            return RedirectToAction("ProductDetails", new { id = snackbarId, productId });
        }

        return RedirectToAction(actionName: "Products", routeValues: new { id = snackbarId });
    }
    
    [Route("/[area]/[controller]/details/{id:int}/orders")]
    public async Task<IActionResult> Orders([FromMultiSource] GetAllOrdersRequest request)
    {
        var snackbar = await _dbContext.SnackBars
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        if (snackbar is null)
        {
            return NotFound();
        }

        const int pageSize = 10;
        
        var orders = await _dbContext.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .Where(x => x.OrderDetails
                .Any(y => y.SnackBarId == request.Id))
            .Skip((request.PageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new OrderViewModel
            {
                Id = x.Id,
                Address = x.Address,
                Status = x.Status,
                Total = x.Total,
                OrderDetails = x.OrderDetails
                    .Select(y => new OrderDetailViewModel(
                        y.ProductId,
                        y.Product.Name,
                        y.Quantity, 
                        y.Price)),
                CustomerId = x.CustomerId,
                CustomerName = x.Customer.DisplayName
            })
            .ToListAsync();
        
        ViewData["Snackbar"] = snackbar;
        return View(orders);
    }

    [HttpGet("/[area]/api/districts")]
    public async Task<IActionResult> GetAllDistricts()
    {
        var districts = await _dbContext.Districts.ToListAsync();
        return Ok(districts);
    }


    [HttpGet("/[area]/api/wards")]
    public async Task<IActionResult> GetAllWards([FromQuery] string? districtName)
    {
        var wardQueryable = _dbContext.Wards.AsQueryable();
        if (!string.IsNullOrEmpty(districtName))
        {
            wardQueryable = wardQueryable.Where(x => x.DistrictName.Equals(districtName));
        }

        var wards = await wardQueryable.ToListAsync();

        return Ok(wards);
    }
}