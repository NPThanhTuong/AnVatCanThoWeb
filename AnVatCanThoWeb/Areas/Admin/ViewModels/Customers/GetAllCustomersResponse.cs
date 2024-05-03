using AnVatCanThoWeb.Areas.Admin.ViewModels.Snackbars;

namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Customers;

public class GetAllCustomersResponse
{
    public IEnumerable<CustomerViewModel> Data { get; init; } = Enumerable.Empty<CustomerViewModel>();
    
    public int PageIndex { get; init; }
    
    public int TotalPages { get; init; }
    
    public bool HasPrevious => (PageIndex - 1) > 0;
    
    public bool HasNext => (PageIndex + 1) <= TotalPages;
}