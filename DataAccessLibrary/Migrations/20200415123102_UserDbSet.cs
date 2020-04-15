using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class UserDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchlistItem_Movies_MovieId",
                table: "WatchlistItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchlistItem",
                table: "WatchlistItem");

            migrationBuilder.RenameTable(
                name: "WatchlistItem",
                newName: "WatchlistItems");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "WatchlistItems",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchlistItems",
                table: "WatchlistItems",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistItems_UserId1",
                table: "WatchlistItems",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchlistItems_Movies_MovieId",
                table: "WatchlistItems",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchlistItems_AspNetUsers_UserId1",
                table: "WatchlistItems",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchlistItems_Movies_MovieId",
                table: "WatchlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchlistItems_AspNetUsers_UserId1",
                table: "WatchlistItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchlistItems",
                table: "WatchlistItems");

            migrationBuilder.DropIndex(
                name: "IX_WatchlistItems_UserId1",
                table: "WatchlistItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "WatchlistItems");

            migrationBuilder.RenameTable(
                name: "WatchlistItems",
                newName: "WatchlistItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchlistItem",
                table: "WatchlistItem",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WatchlistItem_Movies_MovieId",
                table: "WatchlistItem",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
