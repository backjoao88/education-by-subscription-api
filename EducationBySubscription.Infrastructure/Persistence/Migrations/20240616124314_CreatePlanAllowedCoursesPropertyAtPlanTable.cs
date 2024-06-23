using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationBySubscription.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlanAllowedCoursesPropertyAtPlanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllowedCourses",
                table: "tbl_Plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "tbl_OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 16, 9, 43, 14, 578, DateTimeKind.Local).AddTicks(3437),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 13, 51, 0, 26, DateTimeKind.Local).AddTicks(3409));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedCourses",
                table: "tbl_Plans");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "tbl_OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 13, 51, 0, 26, DateTimeKind.Local).AddTicks(3409),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 16, 9, 43, 14, 578, DateTimeKind.Local).AddTicks(3437));
        }
    }
}
