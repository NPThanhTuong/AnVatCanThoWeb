using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.Admin.Common;
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
        var totalPages = (int)Math.Ceiling((float) totalRows / pageSize);

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