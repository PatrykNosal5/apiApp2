using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

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

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Login", "Password", "RefreshToken", "Salt" },
                values: new object[,]
                {
                    { 1, "login1", "password1", "abcdabcdabcdabcdabcdabcdabcdabcdabcdabcd", "123456789123456789123456789" },
                    { 2, "login2", "password2", "abcabcabcabcabcabcabcabcabcabcabcabcabc", "012345678901234567890123456789" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "Birthdate",
                value: new DateTime(2023, 5, 17, 23, 24, 7, 255, DateTimeKind.Local).AddTicks(6282));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "Birthdate",
                value: new DateTime(2023, 5, 17, 23, 24, 7, 255, DateTimeKind.Local).AddTicks(6288));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 17, 23, 24, 7, 255, DateTimeKind.Local).AddTicks(6183), new DateTime(2023, 5, 17, 23, 24, 7, 255, DateTimeKind.Local).AddTicks(6229) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 17, 23, 24, 7, 255, DateTimeKind.Local).AddTicks(6237), new DateTime(2023, 5, 17, 23, 24, 7, 255, DateTimeKind.Local).AddTicks(6241) });
        }
    }
}
