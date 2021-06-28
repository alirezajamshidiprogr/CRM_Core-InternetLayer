using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBASCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBASGraduation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASGraduation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBASIntroductionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASIntroductionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBASPayType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASPayType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBASPeopleTypeField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASPeopleTypeField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBASPotential",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASPotential", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBASPrefix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASPrefix", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBASServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManualCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SystemCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CertificateCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Job = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    P_Birthday = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    M_Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    P_MariedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_MariedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarriedType = table.Column<int>(type: "int", nullable: true),
                    IntroduceId = table.Column<int>(type: "int", nullable: true),
                    TBASCategoryId = table.Column<int>(type: "int", nullable: false),
                    TBASPotentialId = table.Column<int>(type: "int", nullable: true),
                    TBASPrefixId = table.Column<int>(type: "int", nullable: true),
                    TBASGraduationId = table.Column<int>(type: "int", nullable: true),
                    TBASIntroductionTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_People_IntroduceId",
                        column: x => x.IntroduceId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_TBASCategory_TBASCategoryId",
                        column: x => x.TBASCategoryId,
                        principalTable: "TBASCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_People_TBASGraduation_TBASGraduationId",
                        column: x => x.TBASGraduationId,
                        principalTable: "TBASGraduation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_TBASIntroductionType_TBASIntroductionTypeId",
                        column: x => x.TBASIntroductionTypeId,
                        principalTable: "TBASIntroductionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_TBASPotential_TBASPotentialId",
                        column: x => x.TBASPotentialId,
                        principalTable: "TBASPotential",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_TBASPrefix_TBASPrefixId",
                        column: x => x.TBASPrefixId,
                        principalTable: "TBASPrefix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Province = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Alley = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    OtherAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeopleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClerkServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TBASServicesId = table.Column<int>(type: "int", nullable: false),
                    PeopleId = table.Column<int>(type: "int", nullable: false),
                    Acitve = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClerkServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClerkServices_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClerkServices_TBASServices_TBASServicesId",
                        column: x => x.TBASServicesId,
                        principalTable: "TBASServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeopleProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TBASPeopleTypeField = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    PeopleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleProperty_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeopleVirtual",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telegram = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PeopleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleVirtual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleVirtual_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeopleId = table.Column<int>(type: "int", nullable: true),
                    P_ReservationDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    M_ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClerkId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PayTypeId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_People_ClerkId",
                        column: x => x.ClerkId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_TBASPayType_PayTypeId",
                        column: x => x.PayTypeId,
                        principalTable: "TBASPayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_TBASServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "TBASServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    ClerkServicesId = table.Column<int>(type: "int", nullable: false),
                    isSalonCustomer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerServices_ClerkServices_ClerkServicesId",
                        column: x => x.ClerkServicesId,
                        principalTable: "ClerkServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerServices_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PeopleId",
                table: "Address",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClerkServices_PeopleId",
                table: "ClerkServices",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClerkServices_TBASServicesId",
                table: "ClerkServices",
                column: "TBASServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServices_ClerkServicesId",
                table: "CustomerServices",
                column: "ClerkServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServices_ReservationId",
                table: "CustomerServices",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_People_IntroduceId",
                table: "People",
                column: "IntroduceId");

            migrationBuilder.CreateIndex(
                name: "IX_People_TBASCategoryId",
                table: "People",
                column: "TBASCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_People_TBASGraduationId",
                table: "People",
                column: "TBASGraduationId");

            migrationBuilder.CreateIndex(
                name: "IX_People_TBASIntroductionTypeId",
                table: "People",
                column: "TBASIntroductionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_People_TBASPotentialId",
                table: "People",
                column: "TBASPotentialId");

            migrationBuilder.CreateIndex(
                name: "IX_People_TBASPrefixId",
                table: "People",
                column: "TBASPrefixId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleProperty_PeopleId",
                table: "PeopleProperty",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleVirtual_PeopleId",
                table: "PeopleVirtual",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClerkId",
                table: "Reservation",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PayTypeId",
                table: "Reservation",
                column: "PayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PeopleId",
                table: "Reservation",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ServiceId",
                table: "Reservation",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CustomerServices");

            migrationBuilder.DropTable(
                name: "PeopleProperty");

            migrationBuilder.DropTable(
                name: "PeopleVirtual");

            migrationBuilder.DropTable(
                name: "TBASPeopleTypeField");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ClerkServices");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "TBASPayType");

            migrationBuilder.DropTable(
                name: "TBASServices");

            migrationBuilder.DropTable(
                name: "TBASCategory");

            migrationBuilder.DropTable(
                name: "TBASGraduation");

            migrationBuilder.DropTable(
                name: "TBASIntroductionType");

            migrationBuilder.DropTable(
                name: "TBASPotential");

            migrationBuilder.DropTable(
                name: "TBASPrefix");
        }
    }
}
