using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoReservas.Migrations
{
    /// <inheritdoc />
    public partial class Reservas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    idcliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellidoC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.idcliente);
                });

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    idempleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cargo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleados", x => x.idempleado);
                });

            migrationBuilder.CreateTable(
                name: "mesas",
                columns: table => new
                {
                    idmesa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_mesa = table.Column<int>(type: "int", nullable: false),
                    capacidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mesas", x => x.idmesa);
                });

            migrationBuilder.CreateTable(
                name: "reservas",
                columns: table => new
                {
                    idreserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaHoraReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaHoraLlegada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    clienteid = table.Column<int>(type: "int", nullable: false),
                    mesaid = table.Column<int>(type: "int", nullable: false),
                    empleadoid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservas", x => x.idreserva);
                    table.ForeignKey(
                        name: "FK_reservas_clientes_clienteid",
                        column: x => x.clienteid,
                        principalTable: "clientes",
                        principalColumn: "idcliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservas_empleados_empleadoid",
                        column: x => x.empleadoid,
                        principalTable: "empleados",
                        principalColumn: "idempleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservas_mesas_mesaid",
                        column: x => x.mesaid,
                        principalTable: "mesas",
                        principalColumn: "idmesa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservas_clienteid",
                table: "reservas",
                column: "clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_reservas_empleadoid",
                table: "reservas",
                column: "empleadoid");

            migrationBuilder.CreateIndex(
                name: "IX_reservas_mesaid",
                table: "reservas",
                column: "mesaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservas");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "mesas");
        }
    }
}
