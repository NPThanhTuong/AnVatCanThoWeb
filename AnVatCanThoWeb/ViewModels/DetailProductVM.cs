using AnVatCanTho.Models;

namespace AnVatCanThoWeb.ViewModels
{
    public class DetailProductVM
    {
        public ProductVM ProductVM { get; set; }
        public List<ProductVM> RelatedProduct { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
