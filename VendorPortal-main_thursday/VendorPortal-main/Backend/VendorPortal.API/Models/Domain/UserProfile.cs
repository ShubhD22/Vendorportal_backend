using Microsoft.AspNetCore.Identity;

namespace VendorPortal.API.Models.Domain
{
    public class UserProfile : IdentityUser
    {
        public string Name { get; set; }
        public string? OrganizationName { get; set; }
        public Guid? VendorCategoryId { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public int? Pincode { get; set; }
        public string? DocumentPaths { get; set; }
        public string? DocumentVerified { get; set; }
        public string? DocumentComment { get; set; }

        // Navigation properties
        public VendorCategory VendorCategory { get; set; }

    }
}
