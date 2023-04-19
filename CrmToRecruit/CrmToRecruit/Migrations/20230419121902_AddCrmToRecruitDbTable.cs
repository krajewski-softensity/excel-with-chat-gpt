using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmToRecruit.Migrations
{
    /// <inheritdoc />
    public partial class AddCrmToRecruitDbTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrmToRecruit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DealName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DealOwner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumberOfResources = table.Column<int>(type: "int", nullable: true),
                    StageOfOpen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StageOfClosed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobOpeningCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastActivityTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MustHaveSkills = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NiceToHaveSkills = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RmOwnership = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NotesPriority = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubmittedST = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubmittedVendor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InterviewedST = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InterviewedVendor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmedST = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmedVendor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RejectedST = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RejectedVendor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmToRecruit", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrmToRecruit");
        }
    }
}
