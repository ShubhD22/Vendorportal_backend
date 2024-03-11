using System.ComponentModel.DataAnnotations;

namespace VendorPortal.API.Models.DTO
{
    public class VendorCategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string DocumentList { get; set; }
    }
}
