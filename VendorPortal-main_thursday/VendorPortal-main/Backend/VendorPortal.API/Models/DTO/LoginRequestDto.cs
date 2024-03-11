using System.ComponentModel.DataAnnotations;

namespace VendorPortal.API.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password, ErrorMessage = "Uppercase,Symbol & Numbers combination required")]
        public string Password { get; set; }
    }
}
