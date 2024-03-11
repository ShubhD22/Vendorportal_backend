namespace VendorPortal.API.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string ProjectHeadId { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Description { get;set; }

        // Navigation properties
        public UserProfile UserProfile { get; set; }
    }
}
