using Microsoft.EntityFrameworkCore.Migrations;

namespace WebP.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Posao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nedelja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posao", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Radnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JMBG = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AgencijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Radnik_Agencija_AgencijaID",
                        column: x => x.AgencijaID,
                        principalTable: "Agencija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spoj",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Honorar = table.Column<int>(type: "int", nullable: false),
                    DanID = table.Column<int>(type: "int", nullable: true),
                    RadnikID = table.Column<int>(type: "int", nullable: true),
                    PosaoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spoj", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Spoj_Dan_DanID",
                        column: x => x.DanID,
                        principalTable: "Dan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spoj_Posao_PosaoID",
                        column: x => x.PosaoID,
                        principalTable: "Posao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spoj_Radnik_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Radnik_AgencijaID",
                table: "Radnik",
                column: "AgencijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_DanID",
                table: "Spoj",
                column: "DanID");

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_PosaoID",
                table: "Spoj",
                column: "PosaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_RadnikID",
                table: "Spoj",
                column: "RadnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spoj");

            migrationBuilder.DropTable(
                name: "Dan");

            migrationBuilder.DropTable(
                name: "Posao");

            migrationBuilder.DropTable(
                name: "Radnik");

            migrationBuilder.DropTable(
                name: "Agencija");
        }
    }
}
