using FluentValidation;

namespace AnVatCanThoWeb.Areas.SnackBar.ViewModels.Authentication;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
