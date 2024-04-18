using AnVatCanTho.Models;
using System.ComponentModel;

namespace AnVatCanThoWeb.Areas.SnackBar.ViewModels.Product;

public class CreateProductViewModel
{
    public int Id { get; set; }
    public int SnackBarId { get; set; }
    public int ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; }

    [DisplayName("Tên sản phẩm")]
    public string? Name { get; set; }

    [DisplayName("Mô tả sản phẩm")]
    public string? Description { get; set; }

    [DisplayName("Thành phần")]
    public string? Ingredient { get; set; }

    [DisplayName("Số lượng kho")]
    public int? Stock { get; set; }

    [DisplayName("Đơn giá")]
    public int? UnitPrice { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
