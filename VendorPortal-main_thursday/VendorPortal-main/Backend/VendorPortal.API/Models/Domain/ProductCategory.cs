using System.ComponentModel.DataAnnotations.Schema;

namespace VendorPortal.API.Models.Domain
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRoot { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual ProductCategory? ParentCategory { get; set; }
    }
}
