using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VendorPortal.API.Models.Domain;

namespace VendorPortal.API.Data
{
    public class VendorPortalDbContext : IdentityDbContext<UserProfile>
    {
        public VendorPortalDbContext(DbContextOptions<VendorPortalDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<RFP> RFPs { get; set; }
        public DbSet<VendorCategory> VendorCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var adminRoleId = "7f541d69-7524-4077-bcb8-bdbe3fd836e0";
            var vendorRoleId = "1beefd77-dac2-4b30-b285-4407bfd1507f";
            var projectHeadRoleId = "8bb312cb-0bbc-4788-9b55-8520aaa01e35";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId,
                    Name="Admin",
                    NormalizedName="Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id=vendorRoleId,
                    ConcurrencyStamp=vendorRoleId,
                    Name="Vendor",
                    NormalizedName="Vendor".ToUpper()
                },
                new IdentityRole
                {
                    Id=projectHeadRoleId,
                    ConcurrencyStamp=projectHeadRoleId,
                    Name="ProjectHead",
                    NormalizedName="ProjectHead".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<UserProfile>().Navigation(e => e.VendorCategory).AutoInclude();

            builder.Entity<Project>().HasOne(u=>u.UserProfile).WithMany().HasForeignKey(u => u.ProjectHeadId);

            builder.Entity<Product>().HasOne(u=>u.Category).WithMany().HasForeignKey(u => u.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>().HasOne(u=>u.SubCategory).WithMany().HasForeignKey(u => u.SubCategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductCategory>().HasOne(u => u.ParentCategory).WithMany().HasForeignKey(u => u.ParentCategoryId);

        }

    }
}
