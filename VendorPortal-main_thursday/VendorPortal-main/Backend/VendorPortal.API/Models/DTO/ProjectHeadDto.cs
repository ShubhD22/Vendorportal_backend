using System.ComponentModel.DataAnnotations;

namespace VendorPortal.API.Models.DTO
{
    public class ProjectHeadDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Mobile Number")]
        public string PhoneNumber { get; set; }
    }
}
