namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Customers;

public class OrderViewModel
{
    public int Id { get; init; }
    
    public int Total { get; init; }
    
    public bool Status { get; init; }
    
    public string Address { get; init; } = string.Empty;
    
    public int SnackbarId { get; init; }
    
    public string SnackbarName { get; init; } = string.Empty;
    
    public IEnumerable<OrderDetailViewModel> OrderDetails = Enumerable.Empty<OrderDetailViewModel>();
}

public record OrderDetailViewModel(
    int ProductId,
    string ProductName,
    int Quantity,
    int Price);