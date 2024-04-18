namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Snackbars;

public class SnackbarViewModel
{
    public int Id { get; init; }

    public string Username { get; init; } = string.Empty;

    public string Tel { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string? Avatar { get; init; }

    public DateTime Dob { get; init; }

    public string? CoverImage { get; init; }

    public string DisplayName { get; init; } = string.Empty;
}