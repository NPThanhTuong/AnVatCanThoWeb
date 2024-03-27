using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        
        [DisplayName("Tên vai trò")]
        public string Name { get; set; } = null!;
        
        [DisplayName("Quyền")]
        public string Permission { get; set; } = null!;

        public ICollection<Administrator> Administrators { get; set; } = new List<Administrator>();
    }
}
