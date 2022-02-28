using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class ReCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2022, 2, 27, 16, 58, 21, 117, DateTimeKind.Utc).AddTicks(169));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "public",
                table: "ReportStatus",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 27, 16, 58, 21, 117, DateTimeKind.Utc).AddTicks(169),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2022, 2, 28, 10, 19, 47, 660, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "DateCreated",
                value: new DateTime(2022, 2, 27, 16, 58, 21, 117, DateTimeKind.Utc).AddTicks(1079));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "DateCreated",
                value: new DateTime(2022, 2, 27, 16, 58, 21, 117, DateTimeKind.Utc).AddTicks(1087));
        }
    }
}
