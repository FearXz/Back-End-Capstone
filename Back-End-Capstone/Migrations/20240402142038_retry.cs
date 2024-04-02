using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Capstone.Migrations
{
    /// <inheritdoc />
    public partial class retry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aziende",
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
                    table.PrimaryKey("PK_Aziende", x => x.IdAzienda);
                });

            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    IdCategorie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.IdCategorie);
                });

            migrationBuilder.CreateTable(
                name: "GiorniChiusura",
                columns: table => new
                {
                    IdGiorniChiusura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroGiorno = table.Column<int>(type: "int", nullable: false),
                    NomeGiorno = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiorniChiusura", x => x.IdGiorniChiusura);
                });

            migrationBuilder.CreateTable(
                name: "TipiProdotti",
                columns: table => new
                {
                    IdTipoProdotto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipiProdotti", x => x.IdTipoProdotto);
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
                name: "Ristoranti",
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
                    OrarioApertura = table.Column<TimeSpan>(type: "time", nullable: false),
                    OrarioChiusura = table.Column<TimeSpan>(type: "time", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAttivo = table.Column<bool>(type: "bit", nullable: false),
                    ImgCopertina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ristoranti", x => x.IdRistorante);
                    table.ForeignKey(
                        name: "FK_Ristoranti_Aziende_IdAzienda",
                        column: x => x.IdAzienda,
                        principalTable: "Aziende",
                        principalColumn: "IdAzienda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorieRistoranti",
                columns: table => new
                {
                    IdCategorieRistorante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRistorante = table.Column<int>(type: "int", nullable: false),
                    IdCategorie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieRistoranti", x => x.IdCategorieRistorante);
                    table.ForeignKey(
                        name: "FK_CategorieRistoranti_Categorie_IdCategorie",
                        column: x => x.IdCategorie,
                        principalTable: "Categorie",
                        principalColumn: "IdCategorie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorieRistoranti_Ristoranti_IdRistorante",
                        column: x => x.IdRistorante,
                        principalTable: "Ristoranti",
                        principalColumn: "IdRistorante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiorniChiusuraRistoranti",
                columns: table => new
                {
                    IdGiorniChiusuraRistorante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRistorante = table.Column<int>(type: "int", nullable: false),
                    IdGiorniChiusura = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiorniChiusuraRistoranti", x => x.IdGiorniChiusuraRistorante);
                    table.ForeignKey(
                        name: "FK_GiorniChiusuraRistoranti_GiorniChiusura_IdGiorniChiusura",
                        column: x => x.IdGiorniChiusura,
                        principalTable: "GiorniChiusura",
                        principalColumn: "IdGiorniChiusura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiorniChiusuraRistoranti_Ristoranti_IdRistorante",
                        column: x => x.IdRistorante,
                        principalTable: "Ristoranti",
                        principalColumn: "IdRistorante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientiRistoranti",
                columns: table => new
                {
                    IdIngrediente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRistorante = table.Column<int>(type: "int", nullable: false),
                    NomeIngrediente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoIngrediente = table.Column<double>(type: "float", nullable: false),
                    IsAttivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientiRistoranti", x => x.IdIngrediente);
                    table.ForeignKey(
                        name: "FK_IngredientiRistoranti_Ristoranti_IdRistorante",
                        column: x => x.IdRistorante,
                        principalTable: "Ristoranti",
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
                    IdRistorante = table.Column<int>(type: "int", nullable: false),
                    IndirizzoConsegna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataOrdine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrarioConsegnaPrevista = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsOrdineEvaso = table.Column<bool>(type: "bit", nullable: false),
                    IsOrdineConsegnato = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.IdOrdini);
                    table.ForeignKey(
                        name: "FK_Ordini_Ristoranti_IdRistorante",
                        column: x => x.IdRistorante,
                        principalTable: "Ristoranti",
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
                name: "ProdottiRistoranti",
                columns: table => new
                {
                    IdProdottoRistorante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRistorante = table.Column<int>(type: "int", nullable: false),
                    NomeProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoProdotto = table.Column<double>(type: "float", nullable: false),
                    IsAttivo = table.Column<bool>(type: "bit", nullable: false),
                    DescrizioneProdotto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgProdotto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottiRistoranti", x => x.IdProdottoRistorante);
                    table.ForeignKey(
                        name: "FK_ProdottiRistoranti_Ristoranti_IdRistorante",
                        column: x => x.IdRistorante,
                        principalTable: "Ristoranti",
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
                name: "IngredientiProdottiRistoranti",
                columns: table => new
                {
                    IdIngredientiProdottoRistorante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProdottoRistorante = table.Column<int>(type: "int", nullable: false),
                    IdIngredientiRistorante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientiProdottiRistoranti", x => x.IdIngredientiProdottoRistorante);
                    table.ForeignKey(
                        name: "FK_IngredientiProdottiRistoranti_IngredientiRistoranti_IdIngredientiRistorante",
                        column: x => x.IdIngredientiRistorante,
                        principalTable: "IngredientiRistoranti",
                        principalColumn: "IdIngrediente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientiProdottiRistoranti_ProdottiRistoranti_IdProdottoRistorante",
                        column: x => x.IdProdottoRistorante,
                        principalTable: "ProdottiRistoranti",
                        principalColumn: "IdProdottoRistorante",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdottiTipiProdotti",
                columns: table => new
                {
                    IdProdottoTipoProdotto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProdottoRistorante = table.Column<int>(type: "int", nullable: false),
                    IdTipoProdotto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottiTipiProdotti", x => x.IdProdottoTipoProdotto);
                    table.ForeignKey(
                        name: "FK_ProdottiTipiProdotti_ProdottiRistoranti_IdProdottoRistorante",
                        column: x => x.IdProdottoRistorante,
                        principalTable: "ProdottiRistoranti",
                        principalColumn: "IdProdottoRistorante",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdottiTipiProdotti_TipiProdotti_IdTipoProdotto",
                        column: x => x.IdTipoProdotto,
                        principalTable: "TipiProdotti",
                        principalColumn: "IdTipoProdotto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientiProdottiAcquistati",
                columns: table => new
                {
                    IdIngredientiProdottoAcquistato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProdottoAcquistato = table.Column<int>(type: "int", nullable: false),
                    IdIngredientiRistorante = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientiProdottiAcquistati", x => x.IdIngredientiProdottoAcquistato);
                    table.ForeignKey(
                        name: "FK_IngredientiProdottiAcquistati_IngredientiRistoranti_IdIngredientiRistorante",
                        column: x => x.IdIngredientiRistorante,
                        principalTable: "IngredientiRistoranti",
                        principalColumn: "IdIngrediente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientiProdottiAcquistati_ProdottiAcquistati_IdProdottoAcquistato",
                        column: x => x.IdProdottoAcquistato,
                        principalTable: "ProdottiAcquistati",
                        principalColumn: "IdProdottiAcquistati",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorieRistoranti_IdCategorie",
                table: "CategorieRistoranti",
                column: "IdCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_CategorieRistoranti_IdRistorante",
                table: "CategorieRistoranti",
                column: "IdRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_GiorniChiusuraRistoranti_IdGiorniChiusura",
                table: "GiorniChiusuraRistoranti",
                column: "IdGiorniChiusura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiorniChiusuraRistoranti_IdRistorante",
                table: "GiorniChiusuraRistoranti",
                column: "IdRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiProdottiAcquistati_IdIngredientiRistorante",
                table: "IngredientiProdottiAcquistati",
                column: "IdIngredientiRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiProdottiAcquistati_IdProdottoAcquistato",
                table: "IngredientiProdottiAcquistati",
                column: "IdProdottoAcquistato");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiProdottiRistoranti_IdIngredientiRistorante",
                table: "IngredientiProdottiRistoranti",
                column: "IdIngredientiRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiProdottiRistoranti_IdProdottoRistorante",
                table: "IngredientiProdottiRistoranti",
                column: "IdProdottoRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiRistoranti_IdRistorante",
                table: "IngredientiRistoranti",
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
                name: "IX_ProdottiRistoranti_IdRistorante",
                table: "ProdottiRistoranti",
                column: "IdRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiTipiProdotti_IdProdottoRistorante",
                table: "ProdottiTipiProdotti",
                column: "IdProdottoRistorante");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiTipiProdotti_IdTipoProdotto",
                table: "ProdottiTipiProdotti",
                column: "IdTipoProdotto");

            migrationBuilder.CreateIndex(
                name: "IX_Ristoranti_IdAzienda",
                table: "Ristoranti",
                column: "IdAzienda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorieRistoranti");

            migrationBuilder.DropTable(
                name: "GiorniChiusuraRistoranti");

            migrationBuilder.DropTable(
                name: "IngredientiProdottiAcquistati");

            migrationBuilder.DropTable(
                name: "IngredientiProdottiRistoranti");

            migrationBuilder.DropTable(
                name: "ProdottiTipiProdotti");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "GiorniChiusura");

            migrationBuilder.DropTable(
                name: "ProdottiAcquistati");

            migrationBuilder.DropTable(
                name: "IngredientiRistoranti");

            migrationBuilder.DropTable(
                name: "ProdottiRistoranti");

            migrationBuilder.DropTable(
                name: "TipiProdotti");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "Ristoranti");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Aziende");
        }
    }
}
