namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Snackbars;

public class ProductDetailsViewModel
{
    public int Id { get; init; }

    public string ProductCategoryName { get; init; } = string.Empty;
    
    public string ProductTypeName { get; init; } = string.Empty;
    
    public string Name { get; init; } = string.Empty;
    
    public string Description { get; init; } = string.Empty;
        
    public string Ingredient { get; init; } = string.Empty;
    
    public int Stock { get; init; }
    
    public int UnitPrice { get; init; }

    public IEnumerable<string> ProductImages = Enumerable.Empty<string>();
}
