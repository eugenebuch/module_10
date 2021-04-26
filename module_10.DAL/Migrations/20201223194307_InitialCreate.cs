using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace module_10.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AverageMark = table.Column<float>(type: "real", nullable: false),
                    MissedLections = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lections_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    LectionId = table.Column<int>(type: "int", nullable: true),
                    StudentPresence = table.Column<bool>(type: "bit", nullable: false),
                    HomeworkPresence = table.Column<bool>(type: "bit", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_Lections_LectionId",
                        column: x => x.LectionId,
                        principalTable: "Lections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homeworks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Anastasia", "Yarovikova" },
                    { 2, "Vlad", "Sinotov" },
                    { 3, "Ilya", "Maddyson" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AverageMark", "FirstName", "LastName", "MissedLections" },
                values: new object[,]
                {
                    { 1, 4.3f, "Eugene", "Buchenkov", 0 },
                    { 2, 0f, "Kirill", "Makarov", 3 },
                    { 3, 4.2f, "Mikhail", "Eremin", 0 },
                    { 4, 1.5f, "Alexander", "Nikitin", 2 },
                    { 5, 3.9f, "Ivan", "Shkikavy", 0 }
                });

            migrationBuilder.InsertData(
                table: "Lections",
                columns: new[] { "Id", "LecturerId", "Name" },
                values: new object[] { 1, 1, "Maths" });

            migrationBuilder.InsertData(
                table: "Lections",
                columns: new[] { "Id", "LecturerId", "Name" },
                values: new object[] { 2, 1, "Physics" });

            migrationBuilder.InsertData(
                table: "Lections",
                columns: new[] { "Id", "LecturerId", "Name" },
                values: new object[] { 3, 3, "English" });

            migrationBuilder.InsertData(
                table: "Homeworks",
                columns: new[] { "Id", "Date", "HomeworkPresence", "LectionId", "Mark", "StudentId", "StudentPresence" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 5, 1, true },
                    { 2, new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 0, 2, false },
                    { 3, new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 4, 3, true },
                    { 4, new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 5, 4, true },
                    { 5, new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 5, 5, true },
                    { 6, new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 5, 1, true },
                    { 7, new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 0, 2, false },
                    { 8, new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 4, 3, true },
                    { 9, new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 0, 4, false },
                    { 10, new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 5, 5, true },
                    { 11, new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 4, 1, true },
                    { 12, new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 0, 2, false },
                    { 13, new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 4, 3, true },
                    { 14, new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 0, 4, false },
                    { 15, new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 1, 5, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_LectionId",
                table: "Homeworks",
                column: "LectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_StudentId",
                table: "Homeworks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lections_LecturerId",
                table: "Lections",
                column: "LecturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Lections");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Lecturers");
        }
    }
}
