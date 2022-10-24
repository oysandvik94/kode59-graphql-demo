using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kvittr.Model.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kvitts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Worms = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kvitts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kvitts_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(1981, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elon", "Musk", "musketeer" },
                    { 2, new DateTime(1946, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Donald", "Trump", "trumpinator" },
                    { 3, new DateTime(1964, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jeff", "Bezos", "bezoswisser" }
                });

            migrationBuilder.InsertData(
                table: "Kvitts",
                columns: new[] { "Id", "AuthorId", "Body", "Created", "Worms" },
                values: new object[,]
                {
                    { 1, 1, "Nuke Mars!", new DateTime(2022, 10, 24, 18, 26, 35, 552, DateTimeKind.Local).AddTicks(9670), 12323 },
                    { 2, 1, "Also, I'm buying Manchested United ur welcome", new DateTime(2022, 10, 24, 18, 26, 35, 552, DateTimeKind.Local).AddTicks(9703), 6436456 },
                    { 3, 1, "the color organge is named after the fruit", new DateTime(2022, 10, 24, 18, 26, 35, 552, DateTimeKind.Local).AddTicks(9706), 23942934 },
                    { 4, 2, "Sorry losers and haters, but my I.Q. is one of the highest -and you all know it! Please don’t feel so stupid or insecure,it’s not your fault", new DateTime(2022, 10, 24, 18, 26, 35, 552, DateTimeKind.Local).AddTicks(9709), 4575675 },
                    { 5, 2, "Windmills are the greatest threat in the US to both bald and golden eagles. Media claims fictional ‘global warming’ is worse.", new DateTime(2022, 10, 24, 18, 26, 35, 552, DateTimeKind.Local).AddTicks(9712), 95769579 },
                    { 6, 3, "AWS > Azure", new DateTime(2022, 10, 24, 18, 26, 35, 552, DateTimeKind.Local).AddTicks(9717), 846846 },
                    { 7, 3, "My dream is to be shot up in a rocket", new DateTime(2022, 10, 24, 18, 26, 35, 552, DateTimeKind.Local).AddTicks(9719), 1 },
                    { 8, 3, "I got shot up in a rocket", new DateTime(2022, 10, 24, 18, 26, 35, 552, DateTimeKind.Local).AddTicks(9722), 89769769 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kvitts_AuthorId",
                table: "Kvitts",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kvitts");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
