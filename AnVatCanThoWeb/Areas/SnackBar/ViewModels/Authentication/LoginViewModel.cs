using System.ComponentModel.DataAnnotations;

namespace AnVatCanThoWeb.Areas.SnackBar.ViewModels.Authentication;

public record LoginViewModel(
    [DataType(DataType.EmailAddress)]
    string Email,
    [DataType(DataType.Password)]
    string Password);
