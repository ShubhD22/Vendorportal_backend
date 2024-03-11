namespace VendorPortal.API.Models.DTO
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string UnitType { get; set; }
        public string Size { get; set; }
        public string Specification { get; set; }
    }
}
