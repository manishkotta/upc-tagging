﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Repository;
using System;

namespace UPCTaggingInterface.Migrations
{
    [DbContext(typeof(UPCTaggingDBContext))]
    [Migration("20180312141944_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Repositories.Entities.ProductCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("categoryid");

                    b.Property<string>("CategoryName")
                        .HasColumnName("category");

                    b.HasKey("CategoryID");

                    b.ToTable("category");
                });

            modelBuilder.Entity("Repositories.Entities.ProductSubCategory", b =>
                {
                    b.Property<int>("SubCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("subcategoryid");

                    b.Property<string>("SubcategoryName")
                        .HasColumnName("subcategory");

                    b.HasKey("SubCategoryID");

                    b.ToTable("subcategory");
                });

            modelBuilder.Entity("Repositories.Entities.ProductType", b =>
                {
                    b.Property<int>("TypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("typeid");

                    b.Property<string>("ProductTypeName")
                        .HasColumnName("producttype")
                        .HasMaxLength(255);

                    b.HasKey("TypeID");

                    b.ToTable("producttype");
                });

            modelBuilder.Entity("Repositories.Entities.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName");

                    b.HasKey("RoleID");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Repositories.Entities.TaggedUPC", b =>
                {
                    b.Property<string>("UPCCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("upccode");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(255);

                    b.Property<int>("DescriptionID")
                        .HasColumnName("descriptionid");

                    b.Property<bool>("IsMigrated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("ProductCategoryID")
                        .HasColumnName("productcategoryid");

                    b.Property<string>("ProductSizing")
                        .HasColumnName("productsizing");

                    b.Property<int?>("ProductSubcategoryID")
                        .HasColumnName("productsubcategoryid");

                    b.Property<int?>("ProductTypeID")
                        .HasColumnName("producttypeid");

                    b.Property<int?>("productcategoryid");

                    b.Property<int?>("productsubcategoryid");

                    b.Property<int?>("producttypeid");

                    b.HasKey("UPCCode");

                    b.HasIndex("productcategoryid");

                    b.HasIndex("productsubcategoryid");

                    b.HasIndex("producttypeid");

                    b.ToTable("TaggedUPC");
                });

            modelBuilder.Entity("Repositories.Entities.UntaggedUPC", b =>
                {
                    b.Property<int>("UntaggedUPCID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("untaggedupcid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(255);

                    b.Property<int>("DescriptionID")
                        .HasColumnName("descriptionid");

                    b.Property<int?>("ItemAssignedBy")
                        .HasColumnName("itemassignedby");

                    b.Property<int?>("ItemAssignedTo")
                        .HasColumnName("itemassingedto");

                    b.Property<DateTime?>("ItemInsertedAt")
                        .HasColumnName("iteminsertedat")
                        .HasColumnType("timestamp");

                    b.Property<int?>("ItemInsertedBy")
                        .HasColumnName("iteminsertedby");

                    b.Property<DateTime?>("ItemModifiedAt")
                        .HasColumnName("itemmodifiedat")
                        .HasColumnType("timestamp");

                    b.Property<int?>("ItemModifiedBy")
                        .HasColumnName("itemmodifiedby");

                    b.Property<string>("ProductSizing")
                        .HasColumnName("productsizing");

                    b.Property<int?>("StatusID")
                        .HasColumnName("statusid");

                    b.Property<string>("UPCCode")
                        .HasColumnName("upccode");

                    b.Property<int?>("productcategoryid");

                    b.Property<int?>("productsubcategoryid");

                    b.Property<int?>("producttypeid");

                    b.HasKey("UntaggedUPCID");

                    b.HasIndex("productcategoryid");

                    b.HasIndex("productsubcategoryid");

                    b.HasIndex("producttypeid");

                    b.ToTable("untaggedupc");
                });

            modelBuilder.Entity("Repositories.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("userid");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnName("userpassword");

                    b.Property<int>("RoleID")
                        .HasColumnName("userrole");

                    b.Property<string>("UserName")
                        .HasColumnName("username");

                    b.HasKey("UserID")
                        .HasName("userid");

                    b.ToTable("upcuser");
                });

            modelBuilder.Entity("Repositories.Entities.TaggedUPC", b =>
                {
                    b.HasOne("Repositories.Entities.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("productcategoryid");

                    b.HasOne("Repositories.Entities.ProductSubCategory", "ProductSubCategory")
                        .WithMany()
                        .HasForeignKey("productsubcategoryid");

                    b.HasOne("Repositories.Entities.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("producttypeid");
                });

            modelBuilder.Entity("Repositories.Entities.UntaggedUPC", b =>
                {
                    b.HasOne("Repositories.Entities.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("productcategoryid");

                    b.HasOne("Repositories.Entities.ProductSubCategory", "ProductSubCategory")
                        .WithMany()
                        .HasForeignKey("productsubcategoryid");

                    b.HasOne("Repositories.Entities.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("producttypeid");
                });
#pragma warning restore 612, 618
        }
    }
}
