using VendorPortal.API.Models.Domain;

namespace VendorPortal.API.Models.DTO
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string UnitType { get; set; }
        public string Size { get; set; }
        public string Specification { get; set; }
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public string SubCategory { get; set; }
        public Guid SubCategoryId { get; set; }
    }
}
