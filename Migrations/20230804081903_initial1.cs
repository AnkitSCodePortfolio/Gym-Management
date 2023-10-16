using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Management.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_members_MembershipId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_members_MembershipId",
                table: "payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_members",
                table: "members");

            migrationBuilder.RenameTable(
                name: "members",
                newName: "Membership");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Membership",
                table: "Membership",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Membership_MembershipId",
                table: "Customer",
                column: "MembershipId",
                principalTable: "Membership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payment_Membership_MembershipId",
                table: "payment",
                column: "MembershipId",
                principalTable: "Membership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Membership_MembershipId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_Membership_MembershipId",
                table: "payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Membership",
                table: "Membership");

            migrationBuilder.RenameTable(
                name: "Membership",
                newName: "members");

            migrationBuilder.AddPrimaryKey(
                name: "PK_members",
                table: "members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_members_MembershipId",
                table: "Customer",
                column: "MembershipId",
                principalTable: "members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payment_members_MembershipId",
                table: "payment",
                column: "MembershipId",
                principalTable: "members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
