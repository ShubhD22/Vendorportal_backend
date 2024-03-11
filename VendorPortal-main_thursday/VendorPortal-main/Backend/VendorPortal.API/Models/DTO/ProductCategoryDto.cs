namespace VendorPortal.API.Models.DTO
{
    public class ProductCategoryDto
    {
        public string Name { get; set; }
        public bool IsRoot { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
