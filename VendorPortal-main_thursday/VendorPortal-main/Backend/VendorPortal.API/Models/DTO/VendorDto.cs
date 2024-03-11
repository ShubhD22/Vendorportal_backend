using System.ComponentModel.DataAnnotations;

namespace VendorPortal.API.Models.DTO
{
    public class VendorDto
    {
        public string OrganizationName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }

        public Guid VendorCategoryId { get; set; }

    }
}
