using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HighStakes.Storing.Migrations
{
    public partial class newmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Value = table.Column<int>(nullable: false),
                    Suit = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ChipTotal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "Pannu", "Simar" },
                    { 2, "Nguyen", "Han" },
                    { 3, "Goldsmith", "James" }
                });

            migrationBuilder.InsertData(
                table: "Card",
                columns: new[] { "CardId", "Name", "Suit", "Value" },
                values: new object[,]
                {
                    { 28, null, "Clubs", 3 },
                    { 29, null, "Clubs", 4 },
                    { 30, null, "Clubs", 5 },
                    { 31, null, "Clubs", 6 },
                    { 32, null, "Clubs", 7 },
                    { 33, null, "Clubs", 8 },
                    { 34, null, "Clubs", 9 },
                    { 35, null, "Clubs", 10 },
                    { 36, null, "Clubs", 11 },
                    { 37, null, "Clubs", 12 },
                    { 38, null, "Clubs", 13 },
                    { 40, null, "Diamonds", 2 },
                    { 27, null, "Clubs", 2 },
                    { 41, null, "Diamonds", 3 },
                    { 42, null, "Diamonds", 4 },
                    { 43, null, "Diamonds", 5 },
                    { 44, null, "Diamonds", 6 },
                    { 45, null, "Diamonds", 7 },
                    { 46, null, "Diamonds", 8 },
                    { 47, null, "Diamonds", 9 },
                    { 48, null, "Diamonds", 10 },
                    { 49, null, "Diamonds", 11 },
                    { 50, null, "Diamonds", 12 },
                    { 39, null, "Clubs", 14 },
                    { 26, null, "Hearts", 14 },
                    { 25, null, "Hearts", 13 },
                    { 24, null, "Hearts", 12 },
                    { 1, null, "Spades", 2 },
                    { 2, null, "Spades", 3 },
                    { 3, null, "Spades", 4 },
                    { 4, null, "Spades", 5 },
                    { 5, null, "Spades", 6 },
                    { 6, null, "Spades", 7 },
                    { 7, null, "Spades", 8 },
                    { 8, null, "Spades", 9 },
                    { 9, null, "Spades", 10 },
                    { 10, null, "Spades", 11 },
                    { 11, null, "Spades", 12 },
                    { 12, null, "Spades", 13 },
                    { 13, null, "Spades", 14 },
                    { 14, null, "Hearts", 2 },
                    { 15, null, "Hearts", 3 },
                    { 16, null, "Hearts", 4 },
                    { 17, null, "Hearts", 5 },
                    { 18, null, "Hearts", 6 },
                    { 19, null, "Hearts", 7 },
                    { 20, null, "Hearts", 8 },
                    { 21, null, "Hearts", 9 },
                    { 22, null, "Hearts", 10 },
                    { 23, null, "Hearts", 11 },
                    { 51, null, "Diamonds", 13 },
                    { 52, null, "Diamonds", 14 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "AccountId", "ChipTotal", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 1, 5000, "Simar", "Pannu" },
                    { 2, 2, 5000, "Han", "Nguyen" },
                    { 3, 3, 5000, "James", "Goldsmith" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_AccountId",
                table: "User",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
