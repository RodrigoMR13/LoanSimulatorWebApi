using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "SIMULACAO_EMPRESTIMO",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PRODUTO = table.Column<int>(type: "int", nullable: false),
                    NO_PRODUTO = table.Column<string>(type: "varchar(200)", nullable: false),
                    TAXA_JUROS = table.Column<decimal>(type: "numeric(10,9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIMULACAO_EMPRESTIMO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SIMULACOES",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIPO_AMORTIZACAO = table.Column<string>(type: "varchar(80)", nullable: false),
                    ID_SIMULACAO_EMPRESTIMO = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIMULACOES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SIMULACOES_SIMULACAO_EMPRESTIMO_ID_SIMULACAO_EMPRESTIMO",
                        column: x => x.ID_SIMULACAO_EMPRESTIMO,
                        principalSchema: "dbo",
                        principalTable: "SIMULACAO_EMPRESTIMO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PARCELAS",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NUM_PARCELA = table.Column<short>(type: "smallint", nullable: false),
                    VR_AMORTIZACAO = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VR_JUROS = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VR_PARCELA = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ID_SIMULACAO = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARCELAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PARCELAS_SIMULACOES_ID_SIMULACAO",
                        column: x => x.ID_SIMULACAO,
                        principalSchema: "dbo",
                        principalTable: "SIMULACOES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PARCELAS_ID_SIMULACAO",
                schema: "dbo",
                table: "PARCELAS",
                column: "ID_SIMULACAO");

            migrationBuilder.CreateIndex(
                name: "IX_SIMULACOES_ID_SIMULACAO_EMPRESTIMO",
                schema: "dbo",
                table: "SIMULACOES",
                column: "ID_SIMULACAO_EMPRESTIMO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PARCELAS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SIMULACOES",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SIMULACAO_EMPRESTIMO",
                schema: "dbo");
        }
    }
}
