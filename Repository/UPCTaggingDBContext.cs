using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repository
{
    public class UPCTaggingDBContext : DbContext
    {
        public UPCTaggingDBContext(DbContextOptions<UPCTaggingDBContext> options)
           : base(options)
        { }

        public DbSet<UntaggedUPC> UntaggedUPC { get; set; }
        public DbSet<TaggedUPC> TaggedUPC { get; set; }

        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UntaggedUPC>().ToTable("untaggedupc");
            modelBuilder.Entity<UntaggedUPC>(entity =>
            {
                entity.HasKey(e => e.UntaggedUPCID);
                entity.Property(e => e.Description).HasMaxLength(255);  
            });

            modelBuilder.Entity<TaggedUPC>().ToTable("TaggedUPC");
            modelBuilder.Entity<TaggedUPC>(entity =>
            {
                entity.HasKey(e => e.UPCCode);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.IsMigrated).HasDefaultValue(false);
            });

            
            modelBuilder.Entity<ProductType>(entity => {
                entity.ToTable("type");
                entity.HasKey(e => e.TypeID);
                entity.Property(e => e.ProductTypeName).HasMaxLength(255).HasColumnName("producttype");
                entity.Property(e => e.TypeID).HasColumnName("typeid");

            });

            modelBuilder.Entity<ProductCategory>(entity => {
                entity.ToTable("category");
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.CategoryName).HasColumnName("category");
                entity.Property(e => e.CategoryID).HasColumnName("categoryid");

            });

            modelBuilder.Entity<ProductSubCategory>(entity => {
                entity.ToTable("subcategory");
                entity.HasKey(e => e.SubCategoryID);
                entity.Property(e => e.SubcategoryName).HasColumnName("subcategory");
                entity.Property(e => e.SubCategoryID).HasColumnName("subcategoryid");
                
            });


        } 
    }
}
