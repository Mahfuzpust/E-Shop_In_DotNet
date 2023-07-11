using System.ComponentModel.DataAnnotations;

namespace ASP_Ecomerce.Models
{
    public class SpecialTags
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "SpecialTag")]
        public string SpecialTag { get; set; }
    }
}
