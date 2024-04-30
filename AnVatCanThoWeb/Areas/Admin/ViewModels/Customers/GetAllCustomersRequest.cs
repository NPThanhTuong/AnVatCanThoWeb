namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Customers;

public class GetAllCustomersRequest
{
    public int PageIndex { get; init; } = 1;
    
    public string? Keyword { get; init; }
    
    public string? CurrentKeyword { get; init; }
}