using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Capstone.Migrations
{
    /// <inheritdoc />
    public partial class ok : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Azienda",
                columns: table => new
                {
                    IdAzienda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeAzienda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartitaIva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAP = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Azienda", x => x.IdAzienda);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    IdUtente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cellulare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.IdUtente);
                });

            migrationBuilder.CreateTable(
                name: "Ristorante",
                columns: table => new
                {
                    IdRistorante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAzienda = table.Column<int>(type: "int", nullable: false),
                    NomeRistorante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAP = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Latitudine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitudine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgCopertina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ristorante", x => x.IdRistorante);
                    table.ForeignKey(
                        name: "FK_Ristorante_Azienda_IdAzienda",
                        column: x => x.IdAzienda,
                        principalTable: "Azienda",
                        principalColumn: "IdAzienda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientiRistorante",
                columns: table => new
                {
                    IdIngrediente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRistorante = table.Column<int>(type: "int", nullable: false),
                    NomeIngrediente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoIngrediente = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientiRistorante", x => x.IdIngrediente);
                    table.ForeignKey(
                        name: "FK_IngredientiRistorante_Ristorante_IdRistorante",
                        column: x => x.IdRistorante,
                        principalTable: "Ristorante",
                        principalColumn: "IdRistorante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    IdOrdini = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    IdRistorante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.IdOrdini);
                    table.ForeignKey(
                        name: "FK_Ordini_Ristorante_IdRistorante",
                        column: x => x.IdRistorante,
                        principalTable: "Ristorante",
                        principalColumn: "IdRistorante",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordini_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdottoRistorante",
                columns: table => new
                {
                    IdProdottoRistorante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRistorante = table.Column<int>(type: "int", nullable: false),
                    NomeProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoProdotto = table.Column<double>(type: "float", nullable: false),
                    DescrizioneProdotto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgProdotto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottoRistorante", x => x.IdProdottoRistorante);
                    table.ForeignKey(
                        name: "FK_ProdottoRistorante_Ristorante_IdRistorante",
                        column: x => x.IdRistorante,
                        principalTable: "Ristorante",
                        principalColumn: "IdRistorante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdottiAcquistati",
                columns: table => new
                {
                    IdProdottiAcquistati = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrdine = table.Column<int>(type: "int", nullable: false),
                    NomeProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoProdotto = table.Column<double>(type: "float", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    DescrizioneProdotto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgProdotto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottiAcquistati", x => x.IdProdottiAcquistati);
                    table.ForeignKey(
                        name: "FK_ProdottiAcquistati_Ordini_IdOrdine",
                        column: x => x.IdOrdine,
                        principalTable: "Ordini",
                        principalColumn: "IdOrdini",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientiProdottoRistorante",
                columns: table => new
                {
                    IdIngredientiProdottoRistorante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProdottoRistorante = table.Column<int>(type: "int", nullable: false),
                    NomeIngrediente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoIngrediente = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientiProdottoRistorante", x => x.IdIngredientiProdottoRistorante);
                    table.ForeignKey(
                        name: "FK_IngredientiProdottoRistorante_ProdottoRistorante_IdProdottoRistorante",
                        column: x => x.IdProdottoRistorante,
                        principalTable: "ProdottoRistorante",
                        principalColumn: "IdProdottoRistorante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientiProdottoAcquistato",
                columns: table => new
                {
                    IdIngredientiProdottoAcquistato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProdottoAcquistato = table.Column<int>(type: "int", nullable: false),
                    NomeIngrediente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoIngrediente = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientiProdottoAcquistato", x => x.IdIngredientiProdottoAcquistato);
                    table.ForeignKey(
                        name: "FK_IngredientiProdottoAcquistato_ProdottiAcquistati_IdProdottoAcquistato",
                        column: x => x.IdProdottoAcquistato,
                        principalTable: "ProdottiAcquistati",
                        principalColumn: "IdProdottiAcquistati",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiProdottoAcquistato_IdProdottoAcquistato",
                table: "IngredientiProdottoAcquistato",
                column: "IdProdottoAcquistato");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiProdottoRistorante_IdProdottoRistorante",
                table: "IngredientiProdottoRistorante",
                column: "IdProdottoRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiRistorante_IdRistorante",
                table: "IngredientiRistorante",
                column: "IdRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_IdRistorante",
                table: "Ordini",
                column: "IdRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_IdUtente",
                table: "Ordini",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiAcquistati_IdOrdine",
                table: "ProdottiAcquistati",
                column: "IdOrdine");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottoRistorante_IdRistorante",
                table: "ProdottoRistorante",
                column: "IdRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_Ristorante_IdAzienda",
                table: "Ristorante",
                column: "IdAzienda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientiProdottoAcquistato");

            migrationBuilder.DropTable(
                name: "IngredientiProdottoRistorante");

            migrationBuilder.DropTable(
                name: "IngredientiRistorante");

            migrationBuilder.DropTable(
                name: "ProdottiAcquistati");

            migrationBuilder.DropTable(
                name: "ProdottoRistorante");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "Ristorante");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Azienda");
        }
    }
}
