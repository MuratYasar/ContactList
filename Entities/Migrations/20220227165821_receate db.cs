using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class receatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2022, 2, 27, 16, 56, 31, 315, DateTimeKind.Utc).AddTicks(3306));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "public",
                table: "ReportStatus",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 27, 16, 56, 31, 315, DateTimeKind.Utc).AddTicks(3306),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2022, 2, 27, 16, 58, 21, 117, DateTimeKind.Utc).AddTicks(169));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "DateCreated",
                value: new DateTime(2022, 2, 27, 16, 56, 31, 315, DateTimeKind.Utc).AddTicks(4205));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "DateCreated",
                value: new DateTime(2022, 2, 27, 16, 56, 31, 315, DateTimeKind.Utc).AddTicks(4214));
        }
    }
}
