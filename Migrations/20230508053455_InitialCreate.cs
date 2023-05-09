using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ex_personal",
                columns: table => new
                {
                    personalid = table.Column<int>(name: "personal_id", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdate = table.Column<DateTime>(name: "create_date", type: "datetime(6)", nullable: true),
                    uid = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ex_personal", x => x.personalid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ex_group",
                columns: table => new
                {
                    groupid = table.Column<int>(name: "group_id", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    personalidcreate = table.Column<int>(name: "personal_id_create", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ex_group", x => x.groupid);
                    table.ForeignKey(
                        name: "FK_ex_group_ex_personal_personal_id_create",
                        column: x => x.personalidcreate,
                        principalTable: "ex_personal",
                        principalColumn: "personal_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ex_exam",
                columns: table => new
                {
                    examid = table.Column<int>(name: "exam_id", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    access = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    totalquestion = table.Column<int>(name: "total_question", type: "int", nullable: false),
                    personalidcreate = table.Column<int>(name: "personal_id_create", type: "int", nullable: false),
                    groupid = table.Column<int>(name: "group_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ex_exam", x => x.examid);
                    table.ForeignKey(
                        name: "FK_ex_exam_ex_group_group_id",
                        column: x => x.groupid,
                        principalTable: "ex_group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ex_exam_ex_personal_personal_id_create",
                        column: x => x.personalidcreate,
                        principalTable: "ex_personal",
                        principalColumn: "personal_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "groups_personals",
                columns: table => new
                {
                    memberid = table.Column<int>(name: "member_id", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    groupid = table.Column<int>(name: "group_id", type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: true),
                    personalid = table.Column<int>(name: "personal_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups_personals", x => x.memberid);
                    table.ForeignKey(
                        name: "FK_groups_personals_ex_group_group_id",
                        column: x => x.groupid,
                        principalTable: "ex_group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groups_personals_ex_personal_personal_id",
                        column: x => x.personalid,
                        principalTable: "ex_personal",
                        principalColumn: "personal_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ex_question",
                columns: table => new
                {
                    questionid = table.Column<int>(name: "question_id", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    examid = table.Column<int>(name: "exam_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ex_question", x => x.questionid);
                    table.ForeignKey(
                        name: "FK_ex_question_ex_exam_exam_id",
                        column: x => x.examid,
                        principalTable: "ex_exam",
                        principalColumn: "exam_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ex_exam_group_id",
                table: "ex_exam",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_ex_exam_personal_id_create",
                table: "ex_exam",
                column: "personal_id_create");

            migrationBuilder.CreateIndex(
                name: "IX_ex_group_personal_id_create",
                table: "ex_group",
                column: "personal_id_create");

            migrationBuilder.CreateIndex(
                name: "IX_ex_question_exam_id",
                table: "ex_question",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_groups_personals_group_id",
                table: "groups_personals",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_groups_personals_personal_id",
                table: "groups_personals",
                column: "personal_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ex_question");

            migrationBuilder.DropTable(
                name: "groups_personals");

            migrationBuilder.DropTable(
                name: "ex_exam");

            migrationBuilder.DropTable(
                name: "ex_group");

            migrationBuilder.DropTable(
                name: "ex_personal");
        }
    }
}
