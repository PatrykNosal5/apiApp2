using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class finalMigrationREALLY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "Birthdate",
                value: new DateTime(2023, 5, 24, 23, 35, 35, 325, DateTimeKind.Local).AddTicks(7452));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "Birthdate",
                value: new DateTime(2023, 5, 24, 23, 35, 35, 325, DateTimeKind.Local).AddTicks(7462));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 24, 23, 35, 35, 325, DateTimeKind.Local).AddTicks(7293), new DateTime(2023, 5, 24, 23, 35, 35, 325, DateTimeKind.Local).AddTicks(7360) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 24, 23, 35, 35, 325, DateTimeKind.Local).AddTicks(7371), new DateTime(2023, 5, 24, 23, 35, 35, 325, DateTimeKind.Local).AddTicks(7377) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "RefreshToken",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "RefreshToken",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "Birthdate",
                value: new DateTime(2023, 5, 24, 23, 33, 7, 788, DateTimeKind.Local).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "Birthdate",
                value: new DateTime(2023, 5, 24, 23, 33, 7, 788, DateTimeKind.Local).AddTicks(3826));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 24, 23, 33, 7, 788, DateTimeKind.Local).AddTicks(3712), new DateTime(2023, 5, 24, 23, 33, 7, 788, DateTimeKind.Local).AddTicks(3762) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 24, 23, 33, 7, 788, DateTimeKind.Local).AddTicks(3770), new DateTime(2023, 5, 24, 23, 33, 7, 788, DateTimeKind.Local).AddTicks(3774) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "RefreshToken",
                value: "abcdabcdabcdabcdabcdabcdabcdabcdabcdabcd");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "RefreshToken",
                value: "abcabcabcabcabcabcabcabcabcabcabcabcabc");
        }
    }
}
