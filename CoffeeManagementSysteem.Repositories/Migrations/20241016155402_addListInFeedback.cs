using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementSystem.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class addListInFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FeedbackId",
                table: "Orders",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_FeedbackId",
                table: "Customers",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Feedbacks_FeedbackId",
                table: "Customers",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Feedbacks_FeedbackId",
                table: "Orders",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "FeedbackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Feedbacks_FeedbackId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Feedbacks_FeedbackId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FeedbackId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Customers_FeedbackId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "Customers");
        }
    }
}
