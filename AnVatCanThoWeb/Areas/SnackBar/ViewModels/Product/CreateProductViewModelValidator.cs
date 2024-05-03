using FluentValidation;

namespace AnVatCanThoWeb.Areas.SnackBar.ViewModels.Product;

public class CreateProductViewModelValidator : AbstractValidator<CreateProductViewModel>
{
    public CreateProductViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Vui lòng nhập tên sản phẩm");

        RuleFor(x => x.ProductCategoryId).NotEmpty()
            .WithMessage("Vui lòng chọn danh mục sản phẩm");

        RuleFor(x => x.Description).NotEmpty()
            .WithMessage("Vui lòng nhập mô tả sản phẩm");

        RuleFor(x => x.Ingredient).NotEmpty()
            .WithMessage("Vui lòng nhập thành phần có trong sản phẩm");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(1000)
            .WithMessage("Giá sản phẩm phải lớn hơn bằng 1000 VNĐ");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Số lượng sản phẩm không âm");
    }
}
