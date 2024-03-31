using FluentValidation;

namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Authentication;

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