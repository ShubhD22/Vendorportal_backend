using System.ComponentModel.DataAnnotations;

namespace VendorPortal.API.Models.DTO
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string ProjectHeadId { get; set; }

        [Required]
        public string ProjectStatus { get; set; }

        public string? Description { get; set; }
    }
}
