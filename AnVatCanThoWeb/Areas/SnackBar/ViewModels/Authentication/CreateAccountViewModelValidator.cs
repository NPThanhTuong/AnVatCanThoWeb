using FluentValidation;

namespace AnVatCanThoWeb.Areas.SnackBar.ViewModels.Authentication;

public class CreateAccountViewModelValidator : AbstractValidator<CreateAccountViewModel>
{
    public CreateAccountViewModelValidator()
    {
        RuleFor(x => x.Dob).NotEmpty()
            .WithMessage("Vui lòng chọn ngày sinh");
        RuleFor(x => x.Email).NotEmpty()
            .WithMessage("Vui lòng nhập email");
        RuleFor(x => x.Username).NotEmpty()
            .WithMessage("Vui lòng nhập Họ và Tên");
        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("Vui lòng nhập mật khẩu");
        RuleFor(x => x.Tel).NotEmpty()
            .WithMessage("Vui lòng nhập số điện thoại");
        RuleFor(x => x.Tel).MaximumLength(10)
            .MinimumLength(10)
            .WithMessage("Số điện thoại có 10 chữ số");
        RuleFor(x => x.ConfirmPassword).NotEmpty()
            .WithMessage("Vui lòng nhập lại mật khẩu");
        RuleFor(x => x.Dob)
            .Must(BeAValidDateOfBirth)
            .WithMessage("Ngày sinh phải nhỏ hơn ngày hiện tại");
    }

    private bool BeAValidDateOfBirth(DateTime? dateOfBirth)
    {
        return dateOfBirth <= DateTime.Now;
    }
}
