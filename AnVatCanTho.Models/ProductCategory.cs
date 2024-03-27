using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class ProductCategory
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; } = null!;
        
        [DisplayName("Tên danh mục sản phẩm")] 
        public string Name { get; set; } = null!;
        
        [DisplayName("Mô tả danh mục sản phẩm")]
        public string Description { get; set; } = null!;

        public ICollection<Product> Products = new List<Product>();
    }
}
