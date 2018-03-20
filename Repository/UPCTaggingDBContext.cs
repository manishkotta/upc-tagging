using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Repositories.Entities;

namespace Repository
{
    public class UPCTaggingDBContext : DbContext
    {
        public UPCTaggingDBContext(DbContextOptions<UPCTaggingDBContext> options)
           : base(options)
        {

        }



        public static readonly LoggerFactory MyLoggerFactory
= new LoggerFactory(new[]
{
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                       , true)
});

        public DbSet<UntaggedUPC> UntaggedUPC { get; set; }
        public DbSet<TaggedUPC> TaggedUPC { get; set; }

        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategory { get; set; }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UPCHistory> UPCHistory {get;set;}

        public DbSet<ExceptionLoggerEntity> ExceptionLogger { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UntaggedUPC>().ToTable("untaggedupc");
            modelBuilder.Entity<UntaggedUPC>(entity =>
            {
                entity.HasKey(e => e.UntaggedUPCID);
                entity.Property(e => e.Description).HasMaxLength(255);
            });

            modelBuilder.Entity<TaggedUPC>().ToTable("taggedupc");
                //.HasOne(u => u.UPCHistroy)
                //.WithOne(t => t.TaggedUPC)
                //.HasForeignKey<UPCHistory>(s => s.TaggedUPCCode);
                                          
            modelBuilder.Entity<TaggedUPC>(entity =>
            {
                entity.HasKey(e => e.UPCCode);
                entity.Property(e => e.Description).HasMaxLength(255);
                //entity.Property(e => e.IsMigrated).HasDefaultValue(false);
            });


            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("producttype");
                entity.HasKey(e => e.TypeID);
                entity.Property(e => e.ProductTypeName).HasMaxLength(255).HasColumnName("producttype");
                entity.Property(e => e.TypeID).HasColumnName("typeid");

            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("category");
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.CategoryName).HasColumnName("category");
                entity.Property(e => e.CategoryID).HasColumnName("categoryid");

            });

            modelBuilder.Entity<ProductSubCategory>(entity =>
            {
                entity.ToTable("subcategory");
                entity.HasKey(e => e.SubCategoryID);
                entity.Property(e => e.SubcategoryName).HasColumnName("subcategory");
                entity.Property(e => e.SubCategoryID).HasColumnName("subcategoryid");

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(e => e.RoleID);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("upcuser");
                entity.HasKey(e => e.UserID).HasName("userid");
            });

            modelBuilder.Entity<UPCHistory>(entity => {

                entity.ToTable("upchistory");

                entity.HasKey(e => e.UPCHistoryID);
                entity.Property(e => e.UPCHistoryID).HasColumnName("upchistoryid");
                entity.Property(e => e.TaggedUPCCode).HasColumnName("taggedupccode");
                entity.Property(e => e.SubmittedBy).HasColumnName("submittedby");
                entity.Property(e => e.ApprovedBy).HasColumnName("approvedby");
                entity.Property(e => e.ItemInsertedAt).HasColumnName("iteminsertedat");
                entity.Property(e => e.ItemModifiedAt).HasColumnName("itemmodifiedat");
                entity.Property(e => e.ModifiedApprovedBy).HasColumnName("modifiedapprovedby");
                entity.Property(e => e.ModifiedSubmittedBy).HasColumnName("modifiedsubmittedby");
            });

            modelBuilder.Entity<ExceptionLoggerEntity>(entity =>
            {
                entity.ToTable("upcexceptions");
                entity.HasKey(e => e.ExceptionID);

            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
        .UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time

        }
    }
}
