using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class finalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                column: "Password",
                value: "Z/DiJ4wcLRUUsveNBvCiENnIaCGTOjvePedL454CraI=");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "Password",
                value: "aMFz1AB6IlUGoSk1+yVR2/i88odz24jUz3OJetd0Nuw=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "Birthdate",
                value: new DateTime(2023, 5, 24, 20, 40, 16, 793, DateTimeKind.Local).AddTicks(9355));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "Birthdate",
                value: new DateTime(2023, 5, 24, 20, 40, 16, 793, DateTimeKind.Local).AddTicks(9366));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 24, 20, 40, 16, 793, DateTimeKind.Local).AddTicks(9214), new DateTime(2023, 5, 24, 20, 40, 16, 793, DateTimeKind.Local).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 24, 20, 40, 16, 793, DateTimeKind.Local).AddTicks(9280), new DateTime(2023, 5, 24, 20, 40, 16, 793, DateTimeKind.Local).AddTicks(9286) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "password1");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "Password",
                value: "password2");
        }
    }
}
