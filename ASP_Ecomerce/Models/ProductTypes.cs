using System.ComponentModel.DataAnnotations;

namespace ASP_Ecomerce.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "ProductType")]
        public string ProductType { get; set; }
    }
}
