using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UPCTaggingInterface.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    categoryid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.categoryid);
                });

            migrationBuilder.CreateTable(
                name: "producttype",
                columns: table => new
                {
                    typeid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    producttype = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producttype", x => x.typeid);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "subcategory",
                columns: table => new
                {
                    subcategoryid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    subcategory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subcategory", x => x.subcategoryid);
                });

            migrationBuilder.CreateTable(
                name: "upcuser",
                columns: table => new
                {
                    userid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    email = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    userpassword = table.Column<string>(nullable: true),
                    userrole = table.Column<int>(nullable: false),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userid", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "TaggedUPC",
                columns: table => new
                {
                    upccode = table.Column<string>(nullable: false),
                    description = table.Column<string>(maxLength: 255, nullable: true),
                    descriptionid = table.Column<int>(nullable: false),
                    IsMigrated = table.Column<bool>(nullable: false, defaultValue: false),
                    productcategoryid = table.Column<int>(nullable: true),
                    productsizing = table.Column<string>(nullable: true),
                    productsubcategoryid = table.Column<int>(nullable: true),
                    producttypeid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaggedUPC", x => x.upccode);
                    table.ForeignKey(
                        name: "FK_TaggedUPC_category_productcategoryid",
                        column: x => x.productcategoryid,
                        principalTable: "category",
                        principalColumn: "categoryid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaggedUPC_subcategory_productsubcategoryid",
                        column: x => x.productsubcategoryid,
                        principalTable: "subcategory",
                        principalColumn: "subcategoryid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaggedUPC_producttype_producttypeid",
                        column: x => x.producttypeid,
                        principalTable: "producttype",
                        principalColumn: "typeid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "untaggedupc",
                columns: table => new
                {
                    untaggedupcid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(maxLength: 255, nullable: true),
                    descriptionid = table.Column<int>(nullable: false),
                    itemassignedby = table.Column<int>(nullable: true),
                    itemassingedto = table.Column<int>(nullable: true),
                    iteminsertedat = table.Column<DateTime>(type: "timestamp", nullable: true),
                    iteminsertedby = table.Column<int>(nullable: true),
                    itemmodifiedat = table.Column<DateTime>(type: "timestamp", nullable: true),
                    itemmodifiedby = table.Column<int>(nullable: true),
                    productsizing = table.Column<string>(nullable: true),
                    statusid = table.Column<int>(nullable: true),
                    upccode = table.Column<string>(nullable: true),
                    productcategoryid = table.Column<int>(nullable: true),
                    productsubcategoryid = table.Column<int>(nullable: true),
                    producttypeid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_untaggedupc", x => x.untaggedupcid);
                    table.ForeignKey(
                        name: "FK_untaggedupc_category_productcategoryid",
                        column: x => x.productcategoryid,
                        principalTable: "category",
                        principalColumn: "categoryid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_untaggedupc_subcategory_productsubcategoryid",
                        column: x => x.productsubcategoryid,
                        principalTable: "subcategory",
                        principalColumn: "subcategoryid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_untaggedupc_producttype_producttypeid",
                        column: x => x.producttypeid,
                        principalTable: "producttype",
                        principalColumn: "typeid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaggedUPC_productcategoryid",
                table: "TaggedUPC",
                column: "productcategoryid");

            migrationBuilder.CreateIndex(
                name: "IX_TaggedUPC_productsubcategoryid",
                table: "TaggedUPC",
                column: "productsubcategoryid");

            migrationBuilder.CreateIndex(
                name: "IX_TaggedUPC_producttypeid",
                table: "TaggedUPC",
                column: "producttypeid");

            migrationBuilder.CreateIndex(
                name: "IX_untaggedupc_productcategoryid",
                table: "untaggedupc",
                column: "productcategoryid");

            migrationBuilder.CreateIndex(
                name: "IX_untaggedupc_productsubcategoryid",
                table: "untaggedupc",
                column: "productsubcategoryid");

            migrationBuilder.CreateIndex(
                name: "IX_untaggedupc_producttypeid",
                table: "untaggedupc",
                column: "producttypeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "TaggedUPC");

            migrationBuilder.DropTable(
                name: "untaggedupc");

            migrationBuilder.DropTable(
                name: "upcuser");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "subcategory");

            migrationBuilder.DropTable(
                name: "producttype");
        }
    }
}
