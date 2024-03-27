using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Bill
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [DisplayName("Tổng hóa đơn")]
        public int Total { get; set; }
        
        [DisplayName("Địa chỉ hóa đơn")]
        public string Address { get; set; } = null!;
    }
}
