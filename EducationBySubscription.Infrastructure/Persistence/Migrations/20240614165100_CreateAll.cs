using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationBySubscription.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 14, 13, 51, 0, 26, DateTimeKind.Local).AddTicks(3409))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalIdSubscription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(15,4)", precision: 15, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCourse = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Lessons_tbl_Courses_IdCourse",
                        column: x => x.IdCourse,
                        principalTable: "tbl_Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPlan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMember = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Subscriptions_tbl_Members_IdMember",
                        column: x => x.IdMember,
                        principalTable: "tbl_Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Subscriptions_tbl_Plans_IdPlan",
                        column: x => x.IdPlan,
                        principalTable: "tbl_Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Lessons_IdCourse",
                table: "tbl_Lessons",
                column: "IdCourse");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Subscriptions_IdMember",
                table: "tbl_Subscriptions",
                column: "IdMember");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Subscriptions_IdPlan",
                table: "tbl_Subscriptions",
                column: "IdPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Lessons");

            migrationBuilder.DropTable(
                name: "tbl_OutboxMessages");

            migrationBuilder.DropTable(
                name: "tbl_Payments");

            migrationBuilder.DropTable(
                name: "tbl_Subscriptions");

            migrationBuilder.DropTable(
                name: "tbl_Users");

            migrationBuilder.DropTable(
                name: "tbl_Courses");

            migrationBuilder.DropTable(
                name: "tbl_Members");

            migrationBuilder.DropTable(
                name: "tbl_Plans");
        }
    }
}
