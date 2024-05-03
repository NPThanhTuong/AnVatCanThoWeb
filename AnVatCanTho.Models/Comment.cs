using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnVatCanTho.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        
        public int SnackBarId { get; set; }
        public SnackBar SnackBar { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [DisplayName("Nội dung bình luận")]
        public string Content { get; set; } = null!;
        
        [DisplayName("Số người thích")]
        public int? LikeQuantity { get; set; }

        public DateTime CreatedAt { get; set; }

        //public DateTime UpdatedAt { get; set; }
    }
}
