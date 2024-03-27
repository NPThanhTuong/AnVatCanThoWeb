using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Customer
    {
        public int Id { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; } = null!;

        [DisplayName("Số điện thoại")]
        public string Tel { get; set; } = null!;

        [DisplayName("Email")]
        public string Email { get; set; } = null!;

        [DisplayName("Ảnh đại diện")]
        public string? Avatar { get; set; }

        [DisplayName("Mật khẩu")]
        public string Password { get; set; } = null!;

        [DisplayName("Ngày sinh")]
        public DateTime Dob { get; set; }

        [DisplayName("Tên hiển thị")]
        public string DisplayName { get; set; } = null!;

        public ICollection<Address> Addresses { get; set; } =  new List<Address>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
