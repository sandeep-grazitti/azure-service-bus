using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureServiceBus.Employee.Infrastructure.Migrations
{
    public partial class PrimaryKeyChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("1c21e224-673b-45c4-a140-30c99f91ed89"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("4d3a7ce4-bc33-4127-a829-ca9b81be7414"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("5f383aa5-413a-4358-8352-c12e9ea927d3"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Contact", "CreatedBy", "CreatedOn", "DepartmentName", "EmpCode", "FirstName", "IsActive", "JoiningDate", "LastName", "ModifiedBy", "ModifiedOn" },
                values: new object[] { new Guid("68bf1cbd-68b6-43cd-b810-f8814aaf8a3f"), "", "", "", new DateTime(2020, 11, 26, 11, 18, 37, 536, DateTimeKind.Local).AddTicks(6028), "Dot Net", "63550340-c2fb-4ff4-964a-601df8d3a54f", "Sandeep", true, new DateTime(2020, 11, 26, 11, 18, 37, 535, DateTimeKind.Local).AddTicks(5729), "Kumar", "", new DateTime(2020, 11, 26, 11, 18, 37, 536, DateTimeKind.Local).AddTicks(6851) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Contact", "CreatedBy", "CreatedOn", "DepartmentName", "EmpCode", "FirstName", "IsActive", "JoiningDate", "LastName", "ModifiedBy", "ModifiedOn" },
                values: new object[] { new Guid("02b71caf-7d25-45bc-9876-d031065e38c9"), "", "", "", new DateTime(2020, 11, 26, 11, 18, 37, 536, DateTimeKind.Local).AddTicks(7389), "QA", "a1b50ce0-954f-423a-b1a2-48f2872ec8c8", "Praveen", true, new DateTime(2020, 11, 26, 11, 18, 37, 536, DateTimeKind.Local).AddTicks(7345), "Kumar", "", new DateTime(2020, 11, 26, 11, 18, 37, 536, DateTimeKind.Local).AddTicks(7407) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Contact", "CreatedBy", "CreatedOn", "DepartmentName", "EmpCode", "FirstName", "IsActive", "JoiningDate", "LastName", "ModifiedBy", "ModifiedOn" },
                values: new object[] { new Guid("a230239e-4709-49e1-a3e5-54c8d8ea0ce4"), "", "", "", new DateTime(2020, 11, 26, 11, 18, 37, 536, DateTimeKind.Local).AddTicks(7420), "Sales", "139fc502-9189-4ffc-9e54-106e6b23d1a0", "Mukul", true, new DateTime(2020, 11, 26, 11, 18, 37, 536, DateTimeKind.Local).AddTicks(7415), "Bansal", "", new DateTime(2020, 11, 26, 11, 18, 37, 536, DateTimeKind.Local).AddTicks(7421) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("02b71caf-7d25-45bc-9876-d031065e38c9"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("68bf1cbd-68b6-43cd-b810-f8814aaf8a3f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("a230239e-4709-49e1-a3e5-54c8d8ea0ce4"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Contact", "CreatedBy", "CreatedOn", "DepartmentName", "EmpCode", "FirstName", "IsActive", "JoiningDate", "LastName", "ModifiedBy", "ModifiedOn" },
                values: new object[] { new Guid("1c21e224-673b-45c4-a140-30c99f91ed89"), "", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dot Net", "a4805487-5056-49a7-a2e7-da1310dfa622", "Sandeep", true, new DateTime(2020, 11, 24, 10, 8, 56, 804, DateTimeKind.Local).AddTicks(3740), "Kumar", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Contact", "CreatedBy", "CreatedOn", "DepartmentName", "EmpCode", "FirstName", "IsActive", "JoiningDate", "LastName", "ModifiedBy", "ModifiedOn" },
                values: new object[] { new Guid("4d3a7ce4-bc33-4127-a829-ca9b81be7414"), "", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "QA", "0788bb0a-03a1-4de3-96f0-bc63a6f17238", "Praveen", true, new DateTime(2020, 11, 24, 10, 8, 56, 805, DateTimeKind.Local).AddTicks(4034), "Kumar", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Contact", "CreatedBy", "CreatedOn", "DepartmentName", "EmpCode", "FirstName", "IsActive", "JoiningDate", "LastName", "ModifiedBy", "ModifiedOn" },
                values: new object[] { new Guid("5f383aa5-413a-4358-8352-c12e9ea927d3"), "", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sales", "ab7428a3-2024-4c73-a74a-96a3498170e0", "Mukul", true, new DateTime(2020, 11, 24, 10, 8, 56, 805, DateTimeKind.Local).AddTicks(4115), "Bansal", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
