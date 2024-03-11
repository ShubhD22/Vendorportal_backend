namespace VendorPortal.API.Models.Domain
{
    public class RFPResponseDto
    {
        public string Title { get; set; }
        public string Document { get; set; }
        public DateTime EndDate { get;set; }

        // Navigation properties
        public VendorCategory VendorCategory { get; set; }
        public Project Project { get; set; }


    }
}
