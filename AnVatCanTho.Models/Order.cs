using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        [DisplayName("Khách hàng")]
        public Customer Customer { get; set; } = null!;

        [DisplayName("Tổng tiền")]
        public int Total { get; set; }

        [DisplayName("Trạng thái đơn hàng")]
        public bool Status { get; set; }

        [DisplayName("Địa chỉ đơn hàng")]
        public string Address { get; set; } = null!;

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
