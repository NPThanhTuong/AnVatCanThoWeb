namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Snackbars;

public class GetAllSnackbarsResponse
{
    public IEnumerable<SnackbarViewModel> Data { get; init; } = Enumerable.Empty<SnackbarViewModel>();
    
    public int PageIndex { get; init; }
    
    public int TotalPages { get; init; }

    public bool HasPrevious => (PageIndex - 1) > 0;
    
    public bool HasNext => (PageIndex + 1) <= TotalPages;
}