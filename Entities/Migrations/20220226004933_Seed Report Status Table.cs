using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class SeedReportStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "public",
                table: "ReportStatus",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 26, 0, 49, 33, 13, DateTimeKind.Utc).AddTicks(9514),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.InsertData(
                schema: "public",
                table: "ReportStatus",
                columns: new[] { "Id", "DateCreated", "Name" },
                values: new object[,]
                {
                    { (byte)1, new DateTime(2022, 2, 26, 0, 49, 33, 14, DateTimeKind.Utc).AddTicks(116), "Hazırlanıyor" },
                    { (byte)2, new DateTime(2022, 2, 26, 0, 49, 33, 14, DateTimeKind.Utc).AddTicks(123), "Tamamlandı" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "public",
                table: "ReportStatus",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2022, 2, 26, 0, 49, 33, 13, DateTimeKind.Utc).AddTicks(9514));
        }
    }
}
