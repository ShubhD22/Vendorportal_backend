using System.ComponentModel.DataAnnotations;

namespace VendorPortal.API.Models.DTO
{
    public class ProjectHeadUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Mobile Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Uppercase,Symbol & Numbers combination required")]
        public string CurrentPassword { get; set; } = string.Empty;

        [DataType(DataType.Password, ErrorMessage = "Uppercase,Symbol & Numbers combination required")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
