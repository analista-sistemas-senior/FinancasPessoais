using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinancasPessoais.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NMUsuario = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NMLogin = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CDSenha = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IDUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Carteira",
                columns: table => new
                {
                    IDCarteira = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMCarteira = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carteira", x => x.IDCarteira);
                    table.ForeignKey(
                        name: "FK_Carteira_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesaFonte",
                columns: table => new
                {
                    IDDespesaFonte = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMDespesaFonte = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaFonte", x => x.IDDespesaFonte);
                    table.ForeignKey(
                        name: "FK_DespesaFonte_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesaTipo",
                columns: table => new
                {
                    IDDespesaTipo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMDespesaTipo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaTipo", x => x.IDDespesaTipo);
                    table.ForeignKey(
                        name: "FK_DespesaTipo_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emissor",
                columns: table => new
                {
                    IDEmissor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMEmissor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emissor", x => x.IDEmissor);
                    table.ForeignKey(
                        name: "FK_Emissor_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndiceFinanceiro",
                columns: table => new
                {
                    IDIndiceFinanceiro = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMIndiceFinanceiro = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    VLIndiceFinanceiro = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    INTaxaPeriodicidade = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceFinanceiro", x => x.IDIndiceFinanceiro);
                    table.ForeignKey(
                        name: "FK_IndiceFinanceiro_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaFonte",
                columns: table => new
                {
                    IDReceitaFonte = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMReceitaFonte = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaFonte", x => x.IDReceitaFonte);
                    table.ForeignKey(
                        name: "FK_ReceitaFonte_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaTipo",
                columns: table => new
                {
                    IDReceitaTipo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMReceitaTipo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaTipo", x => x.IDReceitaTipo);
                    table.ForeignKey(
                        name: "FK_ReceitaTipo_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    IDDespesa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDCarteira = table.Column<int>(type: "integer", nullable: false),
                    IDDespesaTipo = table.Column<int>(type: "integer", nullable: false),
                    IDDespesaFonte = table.Column<int>(type: "integer", nullable: false),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMDespesa = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DSDespesa = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    DTDespesa = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VLDespesa = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.IDDespesa);
                    table.ForeignKey(
                        name: "FK_Despesa_Carteira_IDCarteira",
                        column: x => x.IDCarteira,
                        principalTable: "Carteira",
                        principalColumn: "IDCarteira",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_DespesaFonte_IDDespesaFonte",
                        column: x => x.IDDespesaFonte,
                        principalTable: "DespesaFonte",
                        principalColumn: "IDDespesaFonte",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_DespesaTipo_IDDespesaTipo",
                        column: x => x.IDDespesaTipo,
                        principalTable: "DespesaTipo",
                        principalColumn: "IDDespesaTipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Indexador",
                columns: table => new
                {
                    IDIndexador = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDIndiceFinanceiro = table.Column<int>(type: "integer", nullable: false),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMIndexador = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SGIndexador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PCIndiceFinanceiro = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indexador", x => x.IDIndexador);
                    table.ForeignKey(
                        name: "FK_Indexador_IndiceFinanceiro_IDIndiceFinanceiro",
                        column: x => x.IDIndiceFinanceiro,
                        principalTable: "IndiceFinanceiro",
                        principalColumn: "IDIndiceFinanceiro",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Indexador_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    IDReceita = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDCarteira = table.Column<int>(type: "integer", nullable: false),
                    IDReceitaTipo = table.Column<int>(type: "integer", nullable: false),
                    IDReceitaFonte = table.Column<int>(type: "integer", nullable: false),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMReceita = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DSReceita = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    DTReceita = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VLReceita = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.IDReceita);
                    table.ForeignKey(
                        name: "FK_Receita_Carteira_IDCarteira",
                        column: x => x.IDCarteira,
                        principalTable: "Carteira",
                        principalColumn: "IDCarteira",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receita_ReceitaFonte_IDReceitaFonte",
                        column: x => x.IDReceitaFonte,
                        principalTable: "ReceitaFonte",
                        principalColumn: "IDReceitaFonte",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receita_ReceitaTipo_IDReceitaTipo",
                        column: x => x.IDReceitaTipo,
                        principalTable: "ReceitaTipo",
                        principalColumn: "IDReceitaTipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receita_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestimentoTipo",
                columns: table => new
                {
                    IDInvestimentoTipo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDIndexador = table.Column<int>(type: "integer", nullable: true),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMInvestimentoTipo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SGInvestimentoTipo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    INTipoRentabilidade = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestimentoTipo", x => x.IDInvestimentoTipo);
                    table.ForeignKey(
                        name: "FK_InvestimentoTipo_Indexador_IDIndexador",
                        column: x => x.IDIndexador,
                        principalTable: "Indexador",
                        principalColumn: "IDIndexador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvestimentoTipo_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investimento",
                columns: table => new
                {
                    IDInvestimento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDInvestimentoTipo = table.Column<int>(type: "integer", nullable: false),
                    IDEmissor = table.Column<int>(type: "integer", nullable: false),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMInvestimento = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    VLInvestimento = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DTInvestimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VLSaldo = table.Column<decimal>(type: "numeric", nullable: false),
                    DTVencimento = table.Column<DateTime>(type: "date", nullable: true),
                    PCTaxaRentabilidade = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    INTaxaPeriodicidade = table.Column<byte>(type: "smallint", nullable: true),
                    TXAnotacao = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    FLLiquidado = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investimento", x => x.IDInvestimento);
                    table.ForeignKey(
                        name: "FK_Investimento_Emissor_IDEmissor",
                        column: x => x.IDEmissor,
                        principalTable: "Emissor",
                        principalColumn: "IDEmissor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Investimento_InvestimentoTipo_IDInvestimentoTipo",
                        column: x => x.IDInvestimentoTipo,
                        principalTable: "InvestimentoTipo",
                        principalColumn: "IDInvestimentoTipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Investimento_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestimentoHistorico",
                columns: table => new
                {
                    IDInvestimentoHistorico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDInvestimento = table.Column<int>(type: "integer", nullable: false),
                    DTInvestimentoHistorico = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VLInvestimentoHistorico = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    INInvestimentoHistorico = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestimentoHistorico", x => x.IDInvestimentoHistorico);
                    table.ForeignKey(
                        name: "FK_InvestimentoHistorico_Investimento_IDInvestimento",
                        column: x => x.IDInvestimento,
                        principalTable: "Investimento",
                        principalColumn: "IDInvestimento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carteira_IDUsuario",
                table: "Carteira",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_IDCarteira",
                table: "Despesa",
                column: "IDCarteira");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_IDDespesaFonte",
                table: "Despesa",
                column: "IDDespesaFonte");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_IDDespesaTipo",
                table: "Despesa",
                column: "IDDespesaTipo");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_IDUsuario",
                table: "Despesa",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaFonte_IDUsuario",
                table: "DespesaFonte",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaTipo_IDUsuario",
                table: "DespesaTipo",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Emissor_IDUsuario",
                table: "Emissor",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Indexador_IDIndiceFinanceiro",
                table: "Indexador",
                column: "IDIndiceFinanceiro");

            migrationBuilder.CreateIndex(
                name: "IX_Indexador_IDUsuario",
                table: "Indexador",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceFinanceiro_IDUsuario",
                table: "IndiceFinanceiro",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Investimento_IDEmissor",
                table: "Investimento",
                column: "IDEmissor");

            migrationBuilder.CreateIndex(
                name: "IX_Investimento_IDInvestimentoTipo",
                table: "Investimento",
                column: "IDInvestimentoTipo");

            migrationBuilder.CreateIndex(
                name: "IX_Investimento_IDUsuario",
                table: "Investimento",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_InvestimentoHistorico_IDInvestimento",
                table: "InvestimentoHistorico",
                column: "IDInvestimento");

            migrationBuilder.CreateIndex(
                name: "IX_InvestimentoTipo_IDIndexador",
                table: "InvestimentoTipo",
                column: "IDIndexador");

            migrationBuilder.CreateIndex(
                name: "IX_InvestimentoTipo_IDUsuario",
                table: "InvestimentoTipo",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_IDCarteira",
                table: "Receita",
                column: "IDCarteira");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_IDReceitaFonte",
                table: "Receita",
                column: "IDReceitaFonte");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_IDReceitaTipo",
                table: "Receita",
                column: "IDReceitaTipo");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_IDUsuario",
                table: "Receita",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaFonte_IDUsuario",
                table: "ReceitaFonte",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaTipo_IDUsuario",
                table: "ReceitaTipo",
                column: "IDUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "InvestimentoHistorico");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "DespesaFonte");

            migrationBuilder.DropTable(
                name: "DespesaTipo");

            migrationBuilder.DropTable(
                name: "Investimento");

            migrationBuilder.DropTable(
                name: "Carteira");

            migrationBuilder.DropTable(
                name: "ReceitaFonte");

            migrationBuilder.DropTable(
                name: "ReceitaTipo");

            migrationBuilder.DropTable(
                name: "Emissor");

            migrationBuilder.DropTable(
                name: "InvestimentoTipo");

            migrationBuilder.DropTable(
                name: "Indexador");

            migrationBuilder.DropTable(
                name: "IndiceFinanceiro");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
