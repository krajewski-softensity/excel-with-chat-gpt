using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmToRecruit.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordId = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfResources = table.Column<int>(type: "int", nullable: false),
                    StageOfOpen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StageOfClosed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobOpeningCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActivityTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MustHaveSkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NiceToHaveSkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RMOwnership = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotesPriority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedVendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterviewedST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterviewedVendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmedST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmedVendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectedST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectedVendor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelData");
        }
    }
}
