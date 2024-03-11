using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace VendorPortal.API.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string UnitType { get; set; }
        public string Size { get; set; }
        public string Specification { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }

        public ProductCategory Category { get; set; }
        public ProductCategory SubCategory { get; set; }
    }
}
