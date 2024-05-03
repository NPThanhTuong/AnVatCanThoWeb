using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnVatCanThoWeb.ViewModels.Auth;

public class RegisterViewModel
{
    [DisplayName("Tên đăng nhập")]
    public string? Username { get; set; }
    [DisplayName("Số điện thoại")]
    public string? Tel { get; set; }
    [DisplayName("Email")]
    public string? Email { get; set; }
    [DisplayName("Mật khẩu")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
    public string? ConfirmPassword { get; set; }
    [DisplayName("Ngày sinh")]
    public DateTime? Dob { get; set; }
}
