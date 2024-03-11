using System.ComponentModel.DataAnnotations;

namespace VendorPortal.API.Models.DTO
{
    public class AdminDto
    {
        
        public string Name { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Mobile Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Uppercase,Symbol & Numbers combination required")]
        public string Password { get; set; }

    }
}
