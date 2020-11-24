using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureServiceBus.Employee.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    DepartmentName = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: true),
                    EmpCode = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
