using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Administrator
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

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

        [DisplayName("Thành phố")]
        public string City { get; set; } = null!;

        [DisplayName("Tên hiển thị")]
        public string DisplayName { get; set; } = null!;
    }
}
