using FluentValidation;

namespace AnVatCanThoWeb.ViewModels.Auth
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("Trường địa chỉ email không được trống!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Trường mật khẩu không được trống!");
        }
    }
}
