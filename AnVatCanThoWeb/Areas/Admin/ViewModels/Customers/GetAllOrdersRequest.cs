using Microsoft.AspNetCore.Mvc;

namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Customers;

public class GetAllOrdersRequest
{
    [FromRoute(Name = "id")]
    public int Id { get; set; }

    [FromQuery] public int PageIndex { get; set; } = 1;
}