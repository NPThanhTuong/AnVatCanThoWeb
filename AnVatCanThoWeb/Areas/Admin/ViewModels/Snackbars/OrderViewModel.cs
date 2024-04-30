namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Snackbars;

public class OrderViewModel
{
    public int Id { get; init; }

    public string CustomerName { get; init; } = string.Empty;
    
    public int CustomerId { get; init; }
    
    public int Total { get; init; }
    
    public bool Status { get; init; }
    
    public string Address { get; init; } = string.Empty;
    
    public IEnumerable<OrderDetailViewModel> OrderDetails = Enumerable.Empty<OrderDetailViewModel>();
}

public record OrderDetailViewModel(
    int ProductId,
    string ProductName,
    int Quantity,
    int Price);