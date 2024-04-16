using FluentValidation;

namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Authentication;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(errorMessage: "Email is required")
            .EmailAddress()
            .WithMessage(errorMessage: "Must be an email");
        
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}