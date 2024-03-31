using System.ComponentModel.DataAnnotations;

namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Authentication;

public record LoginViewModel(
    [DataType(DataType.EmailAddress)]
    string Email, 
    [DataType(DataType.Password)]
    string Password);