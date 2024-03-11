namespace VendorPortal.API.Models.DTO
{
    public class VendorUpdateDto
    {
        public string OrganizationName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

    }
}
