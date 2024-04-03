using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Address
    {
        public int Id { get; set; }

        public int? SnackBarId { get; set; }
        public SnackBar? SnackBar { get; set; } = null!;

        [DisplayName("Tên quận/huyện")]
        public string DistrictName { get; set; } = null!;
        public District District { get; set; } = null!;

        [DisplayName("Tên phường")]
        public string WardName { get; set; } = null!;
        public Ward Ward { get; set; } = null!;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; } = null!;

        [DisplayName("Số nhà")]
        public string NoAndStreet { get; set; } = null!;
    }
}
