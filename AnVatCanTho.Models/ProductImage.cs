using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class ProductImage
    {
        [DisplayName("Đường dẫn")]
        public string PathName { get; set; } = null!;
        
        public int SnackBarId { get; set; }
        public SnackBar SnackBar { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
