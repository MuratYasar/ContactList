using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "public",
                table: "ReportStatus",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 27, 16, 54, 19, 824, DateTimeKind.Utc).AddTicks(5032),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2022, 2, 26, 0, 49, 33, 13, DateTimeKind.Utc).AddTicks(9514));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRequested",
                schema: "public",
                table: "Report",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "DateCreated",
                value: new DateTime(2022, 2, 27, 16, 54, 19, 824, DateTimeKind.Utc).AddTicks(5846));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "DateCreated",
                value: new DateTime(2022, 2, 27, 16, 54, 19, 824, DateTimeKind.Utc).AddTicks(5854));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRequested",
                schema: "public",
                table: "Report");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "public",
                table: "ReportStatus",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 26, 0, 49, 33, 13, DateTimeKind.Utc).AddTicks(9514),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2022, 2, 27, 16, 54, 19, 824, DateTimeKind.Utc).AddTicks(5032));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "DateCreated",
                value: new DateTime(2022, 2, 26, 0, 49, 33, 14, DateTimeKind.Utc).AddTicks(116));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "DateCreated",
                value: new DateTime(2022, 2, 26, 0, 49, 33, 14, DateTimeKind.Utc).AddTicks(123));
        }
    }
}
