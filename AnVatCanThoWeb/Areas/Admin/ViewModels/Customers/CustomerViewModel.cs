using AnVatCanThoWeb.Areas.Admin.ViewModels.Common;

namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Customers;

public class CustomerViewModel
{
    public int Id { get; init; }

    public string Username { get; init; } = string.Empty;

    public string Tel { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string? Avatar { get; init; }

    public string Password { get; init; } = string.Empty;
    
    public DateTime Dob { get; init; }
    
    public string DisplayName { get; init; } = string.Empty;

    public IEnumerable<AddressViewModel> Addresses { get; init; } = Enumerable.Empty<AddressViewModel>();
}