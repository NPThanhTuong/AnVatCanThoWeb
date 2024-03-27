using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class District
    {
        [DisplayName("Tên quận/huyện")]
        public string Name { get; set; } = null!;

        public ICollection<Ward> Wards { get; set; } = new List<Ward>();
    }
}
