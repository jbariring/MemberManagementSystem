using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagement.Infrastructure.Migrations
{
    public partial class AddBranchTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop the old string 'Branch' column
            migrationBuilder.DropColumn(
                name: "Branch",
                table: "Members");

            // 2. Ensure IsActive has default value true
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            // 3. Add the BranchID column to Members (temporarily default 0)
            migrationBuilder.AddColumn<int>(
                name: "BranchID",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // 4. Create the Branches table
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                });

            // 5. Seed a default branch
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Name", "Location", "IsActive" },
                values: new object[] { "Default Branch", "Default Location", true });

            // 6. Update existing members to reference the default branch
            migrationBuilder.Sql(
                @"UPDATE Members
                  SET BranchID = (SELECT TOP 1 BranchID FROM Branches WHERE Name = 'Default Branch')");

            // 7. Create an index on BranchID
            migrationBuilder.CreateIndex(
                name: "IX_Members_BranchID",
                table: "Members",
                column: "BranchID");

            // 8. Add the foreign key from Members → Branches
            migrationBuilder.AddForeignKey(
                name: "FK_Members_Branches_BranchID",
                table: "Members",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove FK and index
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Branches_BranchID",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_BranchID",
                table: "Members");

            // Drop Branches table
            migrationBuilder.DropTable(
                name: "Branches");

            // Drop BranchID column from Members
            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "Members");

            // Restore IsActive column (remove default)
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Members",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            // Add back old string 'Branch' column
            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
