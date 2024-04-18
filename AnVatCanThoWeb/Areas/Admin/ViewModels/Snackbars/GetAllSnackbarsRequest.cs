namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Snackbars;

public class GetAllSnackbarsRequest
{
    public int PageIndex { get; init; } = 1;
    
    public string? CurrentKeyword { get; init; }
    
    public string? Keyword { get; init; }
}