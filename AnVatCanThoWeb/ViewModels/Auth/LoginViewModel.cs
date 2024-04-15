using System.ComponentModel.DataAnnotations;

namespace AnVatCanThoWeb.ViewModels.Auth
{
    public record LoginViewModel(
    [DataType(DataType.EmailAddress)]
    string? Email,
    [DataType(DataType.Password)]
    string? Password);
}
