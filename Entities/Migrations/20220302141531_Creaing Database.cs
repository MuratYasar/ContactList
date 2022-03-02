using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class CreaingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "public",
                table: "ReportStatus",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 2, 14, 15, 31, 558, DateTimeKind.Utc).AddTicks(2459),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2022, 2, 28, 10, 19, 47, 660, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "DateCreated",
                value: new DateTime(2022, 3, 2, 14, 15, 31, 558, DateTimeKind.Utc).AddTicks(3301));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "DateCreated",
                value: new DateTime(2022, 3, 2, 14, 15, 31, 558, DateTimeKind.Utc).AddTicks(3308));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "public",
                table: "ReportStatus",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 28, 10, 19, 47, 660, DateTimeKind.Utc).AddTicks(1340),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2022, 3, 2, 14, 15, 31, 558, DateTimeKind.Utc).AddTicks(2459));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "DateCreated",
                value: new DateTime(2022, 2, 28, 10, 19, 47, 660, DateTimeKind.Utc).AddTicks(2151));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "DateCreated",
                value: new DateTime(2022, 2, 28, 10, 19, 47, 660, DateTimeKind.Utc).AddTicks(2160));
        }
    }
}
