﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalHealthApp.DataAccess.Migrations
{
    public partial class Valabilitate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tara = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Judet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Oras = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StradaNumar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BlocScaraApartament = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Sector = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodPostal = table.Column<string>(type: "nchar(6)", fixedLength: true, maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Denumire = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simptome",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Denumire = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simptome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumberCountryPrefix = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfFailLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AdresaId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdresaId",
                        column: x => x.AdresaId,
                        principalTable: "Adresa",
                        principalColumn: "Id");
                }); ;

            migrationBuilder.CreateTable(
                name: "SimptomeDiagnostic",
                columns: table => new
                {
                    DiagnosticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SimptomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimptomeDiagnostic", x => new { x.DiagnosticId, x.SimptomeId });
                    table.ForeignKey(
                        name: "FK_DiagnosticSimptome_Diagnostic_DiagnosticId",
                        column: x => x.DiagnosticId,
                        principalTable: "Diagnostic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosticSimptome_Simptome_SimptomeId",
                        column: x => x.SimptomeId,
                        principalTable: "Simptome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discutie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    titlu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    continut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataCreare = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discutie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discutie_User_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserIdentityRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserIdentityRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserIdentityRole_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserIdentityRole_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenValue = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    RefreshTokenValue = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTokenRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserToken_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserTokenConfirmation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmationTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    ConfirmationToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserTokenConfirmation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserTokenConfirmation_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specialist",
                columns: table => new
                {
                    SpecialistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Specialitate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialist", x => x.SpecialistId);
                    table.ForeignKey(
                        name: "FK_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilizator",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Asigurat = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizator", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComentariiDiscutie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscutieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    continut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataComentariu = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentariiDiscutie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentariiDiscutie_Discutie_DiscutieId",
                        column: x => x.DiscutieId,
                        principalTable: "Discutie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComentariiDiscutie_User_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Valabilitati",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkDayId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    End = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valabilitati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Valabilitati_Specialist_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialist",
                        principalColumn: "SpecialistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CameraConferinta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UtilizatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    OraInceput = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OraSfarsit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraConferinta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialistIdConf",
                        column: x => x.SpecialistId,
                        principalTable: "Specialist",
                        principalColumn: "SpecialistId");
                    table.ForeignKey(
                        name: "FK_UtilizatorIdConf",
                        column: x => x.UtilizatorId,
                        principalTable: "Utilizator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "IstoricDiagnosticUtilizator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UtilizatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataDiagnosticare = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IstoricDiagnosticUtilizator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IstoricDiagnosticUtilizator_Diagnostic_DiagnosticId",
                        column: x => x.DiagnosticId,
                        principalTable: "Diagnostic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IstoricDiagnosticUtilizator_Utilizator_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "Utilizator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UtilizatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataProgramare = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipProgramare = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StareProgramare = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialistIdProg",
                        column: x => x.SpecialistId,
                        principalTable: "Specialist",
                        principalColumn: "SpecialistId");
                    table.ForeignKey(
                        name: "FK_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "Utilizator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UtilizatorSimptome",
                columns: table => new
                {
                    UtilizatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SimptomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilizatorSimptome", x => new { x.UtilizatorId, x.SimptomeId });
                    table.ForeignKey(
                        name: "FK_UtilizatorSimptome_Simptome_SimptomeId",
                        column: x => x.SimptomeId,
                        principalTable: "Simptome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilizatorSimptome_Utilizator_UserId",
                        column: x => x.UtilizatorId,
                        principalTable: "Utilizator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CameraConferinta_SpecialistId",
                table: "CameraConferinta",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_CameraConferinta_UtilizatorId",
                table: "CameraConferinta",
                column: "UtilizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentariiDiscutie_DiscutieId",
                table: "ComentariiDiscutie",
                column: "DiscutieId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentariiDiscutie_UserId",
                table: "ComentariiDiscutie",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__Diagnost__19701A4F23232DD2",
                table: "Diagnostic",
                column: "Denumire",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discutie_UserId",
                table: "Discutie",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_AdresaId",
                table: "IdentityUser",
                column: "AdresaId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserIdentityRole_RoleId",
                table: "IdentityUserIdentityRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserToken_UserId",
                table: "IdentityUserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserTokenConfirmation_UserId",
                table: "IdentityUserTokenConfirmation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IstoricDiagnosticUtilizator_DiagnosticId",
                table: "IstoricDiagnosticUtilizator",
                column: "DiagnosticId");

            migrationBuilder.CreateIndex(
                name: "IX_IstoricDiagnosticUtilizator_UtilizatorId",
                table: "IstoricDiagnosticUtilizator",
                column: "UtilizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Programare_SpecialistId",
                table: "Programare",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_Programare_UtilizatorId",
                table: "Programare",
                column: "UtilizatorId");

            migrationBuilder.CreateIndex(
                name: "UQ__Simptome__19701A4F05BDFAFB",
                table: "Simptome",
                column: "Denumire",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimptomeDiagnostic_SimptomeId",
                table: "SimptomeDiagnostic",
                column: "SimptomeId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizatorSimptome_SimptomeId",
                table: "UtilizatorSimptome",
                column: "SimptomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Valabilitati_SpecialistId",
                table: "Valabilitati",
                column: "SpecialistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CameraConferinta");

            migrationBuilder.DropTable(
                name: "ComentariiDiscutie");

            migrationBuilder.DropTable(
                name: "IdentityUserIdentityRole");

            migrationBuilder.DropTable(
                name: "IdentityUserToken");

            migrationBuilder.DropTable(
                name: "IdentityUserTokenConfirmation");

            migrationBuilder.DropTable(
                name: "IstoricDiagnosticUtilizator");

            migrationBuilder.DropTable(
                name: "Programare");

            migrationBuilder.DropTable(
                name: "SimptomeDiagnostic");

            migrationBuilder.DropTable(
                name: "UtilizatorSimptome");

            migrationBuilder.DropTable(
                name: "Valabilitati");

            migrationBuilder.DropTable(
                name: "Discutie");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "Diagnostic");

            migrationBuilder.DropTable(
                name: "Simptome");

            migrationBuilder.DropTable(
                name: "Utilizator");

            migrationBuilder.DropTable(
                name: "Specialist");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "Adresa");
        }
    }
}
