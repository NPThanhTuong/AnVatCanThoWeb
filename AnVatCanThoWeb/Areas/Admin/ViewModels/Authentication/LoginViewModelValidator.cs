using FluentValidation;

namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Authentication;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(errorMessage: "Vui lòng nhập email")
            .EmailAddress()
            .WithMessage(errorMessage: "Vui lòng nhập email");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(errorMessage: "Vui lòng nhập mật khẩu");
    }
}