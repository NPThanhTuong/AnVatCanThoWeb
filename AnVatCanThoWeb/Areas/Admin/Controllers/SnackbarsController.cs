using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.Admin.Common;
using AnVatCanThoWeb.Areas.Admin.ViewModels.Snackbars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
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

        if (requestKeyword is not null)
        {
            requestPageIndex = 1;
        }
        else
        {
            requestKeyword = requestCurrentKeyword;
        }

        ViewData["CurrentKeyword"] = requestKeyword;

        var snackbars = _dbContext.SnackBars.AsQueryable();
        if (!string.IsNullOrEmpty(requestKeyword))
        {
            snackbars = snackbars
                .Where(x => x.Email.Contains(requestKeyword)
                    || x.Description.Contains(requestKeyword)
                    || x.DisplayName.Contains(requestKeyword)
                    || x.Tel.Contains(requestKeyword)
                    || x.Username.Contains(requestKeyword));
        }

        var totalPages = await snackbars.CountAsync();

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
            }).ToListAsync();

        var response = new GetAllSnackbarsResponse
        {
            Data = snackbarVms,
            TotalPages = totalPages,
            PageIndex = requestPageIndex,
        };

        return View(response);
    }
}