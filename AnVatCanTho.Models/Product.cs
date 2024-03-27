using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Product
    {
        public int Id { get; set; }
     
        public int SnackBarId { get; set; }
        public SnackBar SnackBar { get; set; } = null!;

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;

        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; } = null!;
        
        [DisplayName("Mô tả sản phẩm")]
        public string Description { get; set; } = null!;
        
        [DisplayName("Thành phần")]
        public string Ingredient { get; set; } = null!;
        
        [DisplayName("Số lượng kho")]
        public int Stock { get; set; }
        
        [DisplayName("Đơn giá")]
        public int UnitPrice { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
