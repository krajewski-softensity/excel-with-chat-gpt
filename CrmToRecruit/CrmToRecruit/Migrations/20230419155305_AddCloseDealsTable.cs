using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmToRecruit.Migrations
{
    /// <inheritdoc />
    public partial class AddCloseDealsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClosedDeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DealOwner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DealName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JobOpeningCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumberOfRoles = table.Column<int>(type: "int", nullable: true),
                    NumberOfResources = table.Column<int>(type: "int", nullable: true),
                    LossReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LossDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedDeals", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosedDeals");
        }
    }
}
