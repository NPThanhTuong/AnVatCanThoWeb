using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Ward
    {
        [DisplayName("Tên quận/huyện")]
        public string DistrictName { get; set; } = null!;
        public District District { get; set; } = null!;

        [DisplayName("Tên phường")]
        public string Name { get; set; } = null!;

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
