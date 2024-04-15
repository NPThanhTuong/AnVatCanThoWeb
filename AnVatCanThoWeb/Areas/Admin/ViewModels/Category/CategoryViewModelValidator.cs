using FluentValidation;

namespace AnVatCanThoWeb.Areas.Admin.ViewModels.Category;

public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
{
    public CategoryViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Vui lòng nhập tên loại sản phẩm"); ;    

        RuleFor(x => x.Description).NotEmpty()
            .WithMessage("Vui lòng nhập mô tả loại sản phẩm");

        RuleFor(x => x.ProductTypeId)
            .GreaterThan(-1)
            .WithMessage("Vui lòng chọn kiểu sản phẩm");
    }
}

