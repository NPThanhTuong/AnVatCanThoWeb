using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class OrderDetail
    {
        public int SnackBarId { get; set; }
        public SnackBar SnackBar { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;


        [DisplayName("Số lượng đặt")]
        public int Quantity { get; set; }

        [DisplayName("Giá tiền")]
        public int Price { get; set; }
    }
}
