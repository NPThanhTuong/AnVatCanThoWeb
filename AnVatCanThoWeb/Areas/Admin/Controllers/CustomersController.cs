using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.Admin.Common;
using AnVatCanThoWeb.Areas.Admin.ViewModels.Common;
using AnVatCanThoWeb.Areas.Admin.ViewModels.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
public class CustomersController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public CustomersController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index([FromQuery] GetAllCustomersRequest request)
    {
        const int pageSize = 10;
        var requestKeyword = request.Keyword;
        var requestPageIndex = request.PageIndex;
        var requestCurrentKeyword = request.CurrentKeyword;

        if (requestKeyword is not null)
        {
            requestPageIndex = 1;
        }
        else
        {
            requestKeyword = requestCurrentKeyword;
        }

        ViewData["CurrentKeyword"] = requestKeyword;

        var queryableCustomers = _dbContext.Customers
            .Include(x => x.Addresses)
            .AsQueryable();
        if (requestKeyword is not null)
        {
            queryableCustomers = queryableCustomers.Where(
                x => x.Username.Contains(requestKeyword)
                     || x.Email.Contains(requestKeyword)
                     || x.Email.Contains(requestKeyword)
                     || x.Tel.Contains(requestKeyword)
                     || x.DisplayName.Contains(requestKeyword));
        }

        var totalRows = await queryableCustomers.CountAsync();
        var totalPages = (int)Math.Ceiling((float)totalRows / pageSize);

        queryableCustomers = queryableCustomers
            .Skip((requestPageIndex - 1) * pageSize)
            .Take(pageSize);

        var customers = await queryableCustomers
            .Select(x => new CustomerViewModel
            {
                Id = x.Id,
                Dob = x.Dob,
                Email = x.Email,
                Tel = x.Tel,
                Username = x.Username,
                DisplayName = x.DisplayName,
                Addresses = x.Addresses.Select(y => new AddressViewModel(
                    y.NoAndStreet, y.DistrictName, y.WardName)),
            })
            .ToListAsync();

        var response = new GetAllCustomersResponse
        {
            Data = customers,
            TotalPages = totalPages,
            PageIndex = requestPageIndex,
        };
        return View(response);
    }

    public async Task<IActionResult> Details(int id)
    {
        var customer = await _dbContext.Customers
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (customer is null)
        {
            return NotFound();
        }

        var customerVm = new CustomerViewModel
        {
            Id = customer.Id,
            Dob = customer.Dob,
            Email = customer.Email,
            Tel = customer.Tel,
            Username = customer.Username,
            DisplayName = customer.DisplayName,
            Avatar = customer.Avatar,
            Addresses = customer.Addresses.Select(y => new AddressViewModel(
                y.NoAndStreet, y.DistrictName, y.WardName)),
        };

        return View(customerVm);
    }

    [Route("/[area]/[controller]/details/{id:int}/orders")]
    public async Task<IActionResult> Orders([FromMultiSource] GetAllOrdersRequest request)
    {
        var customer = await _dbContext.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        if (customer is null)
        {
            return NotFound();
        }

        const int pageSize = 10;

        var orders = await _dbContext.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.SnackBar)
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .Where(x => x.CustomerId == request.Id)
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
                SnackbarId = x.OrderDetails.First().SnackBarId,
                SnackbarName = x.OrderDetails.First().SnackBar.DisplayName,
            })
            .ToListAsync();

        ViewData["Customer"] = customer;
        return View(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] int customerId)
    {
        var customer = await _dbContext.Customers
            .FirstOrDefaultAsync(x => x.Id == customerId);
        if (customer is null)
        {
            return NotFound();
        }

        var addresses = await _dbContext.Addresses.Where(x => x.CustomerId == customerId)
            .ToListAsync();

        try
        {
            _dbContext.Customers.Remove(customer);
            _dbContext.Addresses.RemoveRange(addresses);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Không thể xoá khách hàng vì liên quan đến các đơn hàng";
            return RedirectToAction("Details", new { id = customerId });
        }

        return RedirectToAction("Index");
    }
}