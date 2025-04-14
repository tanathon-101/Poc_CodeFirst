using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApi.Migrations.EmployeeMigrations
{
    /// <inheritdoc />
    public partial class UpdateWithNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAddress_Employees_EmployeeId",
                table: "EmployeeAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_EmployeeId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Project_ProjectId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Department_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_Employees_EmployeeId",
                table: "EmployeeTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTask",
                table: "EmployeeTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProject",
                table: "EmployeeProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeAddress",
                table: "EmployeeAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameTable(
                name: "EmployeeTask",
                newName: "EmployeeTasks");

            migrationBuilder.RenameTable(
                name: "EmployeeProject",
                newName: "EmployeeProjects");

            migrationBuilder.RenameTable(
                name: "EmployeeAddress",
                newName: "EmployeeAddresses");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeTask_EmployeeId",
                table: "EmployeeTasks",
                newName: "IX_EmployeeTasks_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProject_ProjectId",
                table: "EmployeeProjects",
                newName: "IX_EmployeeProjects_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTasks",
                table: "EmployeeTasks",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects",
                columns: new[] { "EmployeeId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeAddresses",
                table: "EmployeeAddresses",
                column: "EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAddresses_Employees_EmployeeId",
                table: "EmployeeAddresses",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_Employees_EmployeeId",
                table: "EmployeeProjects",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_Projects_ProjectId",
                table: "EmployeeProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTasks_Employees_EmployeeId",
                table: "EmployeeTasks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAddresses_Employees_EmployeeId",
                table: "EmployeeAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_Employees_EmployeeId",
                table: "EmployeeProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_Projects_ProjectId",
                table: "EmployeeProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTasks_Employees_EmployeeId",
                table: "EmployeeTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTasks",
                table: "EmployeeTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeAddresses",
                table: "EmployeeAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "EmployeeTasks",
                newName: "EmployeeTask");

            migrationBuilder.RenameTable(
                name: "EmployeeProjects",
                newName: "EmployeeProject");

            migrationBuilder.RenameTable(
                name: "EmployeeAddresses",
                newName: "EmployeeAddress");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeTasks_EmployeeId",
                table: "EmployeeTask",
                newName: "IX_EmployeeTask_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProjects_ProjectId",
                table: "EmployeeProject",
                newName: "IX_EmployeeProject_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTask",
                table: "EmployeeTask",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProject",
                table: "EmployeeProject",
                columns: new[] { "EmployeeId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeAddress",
                table: "EmployeeAddress",
                column: "EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAddress_Employees_EmployeeId",
                table: "EmployeeAddress",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_EmployeeId",
                table: "EmployeeProject",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Project_ProjectId",
                table: "EmployeeProject",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Department_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_Employees_EmployeeId",
                table: "EmployeeTask",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
