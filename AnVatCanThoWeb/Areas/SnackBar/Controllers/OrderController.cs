using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.Areas.SnackBar.Common;

using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AnVatCanThoWeb.Areas.SnackBar.Controllers;

[Area(SnackbarAreaName.VALUE)]
[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.SnackBarScheme)]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _db;
    public OrderController(ApplicationDbContext db)
    {
        _db = db;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var SnackBarId = User.FindFirst(ClaimTypes.Sid);
        if (SnackBarId == null || !int.TryParse(SnackBarId.Value, out int snackBarId))
        {
            return BadRequest("ID người dùng không hợp lệ.");
        }
        var OrderId = await _db.OrderDetails.Where(od => od.SnackBarId == snackBarId).Select(od => od.OrderId).ToListAsync();

        var Orders = await _db.Orders.Include(o => o.Customer).Where(o => OrderId.Contains(o.Id)).ToListAsync();
        return View(Orders);
    }
    public async Task<IActionResult> OrderDetail(int Id)
    {
        var OrderDetail = await _db.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == Id).ToListAsync();
        Order order = _db.Orders.FirstOrDefault(o => o.Id == Id);

        ViewBag.OrderStatus = order.Status; 
        return View(OrderDetail);
    }

    public async Task<IActionResult> Process(int id)
    {
        if(id == 0)
        {
            return NotFound();
        }

        Order foundOrder = _db.Orders.FirstOrDefault(o => o.Id == id);
        if(foundOrder is null)
        {
            return NotFound();
        }

        foundOrder.Status = true;
        _db.Update(foundOrder);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
}
