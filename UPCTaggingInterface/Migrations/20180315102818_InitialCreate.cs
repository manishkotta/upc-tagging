using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UPCTaggingInterface.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "upcuser",
                columns: table => new
                {
                    userid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    accessfailedcount = table.Column<int>(nullable: false),
                    concurrencystamp = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    emailconfirmed = table.Column<bool>(nullable: false),
                    id = table.Column<string>(nullable: true),
                    isenabled = table.Column<bool>(nullable: false),
                    lockoutenabled = table.Column<bool>(nullable: false),
                    lockoutend = table.Column<DateTimeOffset>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    normalizedemail = table.Column<string>(nullable: true),
                    normalizedusername = table.Column<string>(nullable: true),
                    passwordhash = table.Column<string>(nullable: true),
                    phonenumber = table.Column<string>(nullable: true),
                    phonenumberconfirmed = table.Column<bool>(nullable: false),
                    userroleid = table.Column<int>(nullable: false),
                    securitystamp = table.Column<string>(nullable: true),
                    twofactorenabled = table.Column<bool>(nullable: false),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userid", x => x.userid);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
