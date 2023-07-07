using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Portal_API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    au_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    au_lname = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    au_fname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    phone = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false, defaultValueSql: "('UNKNOWN')"),
                    address = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    city = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    state = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    zip = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    contract = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UPKCL_auidind", x => x.au_id);
                });

            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    job_id = table.Column<short>(type: "smallint", maxLength: 2, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_desc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('New Position - title not formalized yet')"),
                    min_lvl = table.Column<byte>(type: "tinyint", maxLength: 1, nullable: false),
                    max_lvl = table.Column<byte>(type: "tinyint", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__jobs__6E32B6A57BC9FAF6", x => x.job_id);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    pub_id = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    pub_name = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    city = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    state = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    country = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, defaultValueSql: "('USA')"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UPKCL_pubind", x => x.pub_id);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    stor_id = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    stor_name = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    stor_address = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    city = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    state = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    zip = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UPK_storeid", x => x.stor_id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    emp_id = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    fname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    minit = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    lname = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    job_id = table.Column<short>(type: "smallint", maxLength: 2, nullable: false, defaultValueSql: "((1))"),
                    job_lvl = table.Column<byte>(type: "tinyint", maxLength: 1, nullable: false, defaultValueSql: "((10))"),
                    pub_id = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false, defaultValueSql: "('9952')"),
                    hire_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_id", x => x.emp_id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK__employee__job_id__48CFD27E",
                        column: x => x.job_id,
                        principalTable: "jobs",
                        principalColumn: "job_id");
                    table.ForeignKey(
                        name: "FK__employee__pub_id__4BAC3F29",
                        column: x => x.pub_id,
                        principalTable: "publishers",
                        principalColumn: "pub_id");
                });

            migrationBuilder.CreateTable(
                name: "pub_info",
                columns: table => new
                {
                    pub_id = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    logo = table.Column<byte[]>(type: "image", nullable: true),
                    pr_info = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UPKCL_pubinfo", x => x.pub_id);
                    table.ForeignKey(
                        name: "FK__pub_info__pub_id__440B1D61",
                        column: x => x.pub_id,
                        principalTable: "publishers",
                        principalColumn: "pub_id");
                });

            migrationBuilder.CreateTable(
                name: "titles",
                columns: table => new
                {
                    title_id = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    title = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    type = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false, defaultValueSql: "('UNDECIDED')"),
                    pub_id = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: true),
                    advance = table.Column<decimal>(type: "money", nullable: true),
                    royalty = table.Column<int>(type: "int", nullable: true),
                    ytd_sales = table.Column<int>(type: "int", nullable: true),
                    notes = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    pubdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("UPKCL_titleidind", x => x.title_id);
                    table.ForeignKey(
                        name: "FK__titles__pub_id__2E1BDC42",
                        column: x => x.pub_id,
                        principalTable: "publishers",
                        principalColumn: "pub_id");
                });

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    discounttype = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    stor_id = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    lowqty = table.Column<short>(type: "smallint", maxLength: 2, nullable: false),
                    highqty = table.Column<short>(type: "smallint", maxLength: 2, nullable: false),
                    discount = table.Column<decimal>(type: "decimal(4,2)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__discounts__stor___3C69FB99",
                        column: x => x.stor_id,
                        principalTable: "stores",
                        principalColumn: "stor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roysched",
                columns: table => new
                {
                    title_id = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    lorange = table.Column<int>(type: "int", nullable: false),
                    hirange = table.Column<int>(type: "int", nullable: false),
                    royalty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__roysched__title___3A81B327",
                        column: x => x.title_id,
                        principalTable: "titles",
                        principalColumn: "title_id");
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    stor_id = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    ord_num = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    title_id = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    ord_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    qty = table.Column<short>(type: "smallint", nullable: false),
                    payterms = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UPKCL_sales", x => new { x.stor_id, x.ord_num, x.title_id });
                    table.ForeignKey(
                        name: "FK__sales__stor_id__37A5467C",
                        column: x => x.stor_id,
                        principalTable: "stores",
                        principalColumn: "stor_id");
                    table.ForeignKey(
                        name: "FK__sales__title_id__38996AB5",
                        column: x => x.title_id,
                        principalTable: "titles",
                        principalColumn: "title_id");
                });

            migrationBuilder.CreateTable(
                name: "titleauthor",
                columns: table => new
                {
                    au_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    title_id = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    au_ord = table.Column<byte>(type: "tinyint", nullable: true),
                    royaltyper = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UPKCL_taind", x => new { x.au_id, x.title_id });
                    table.ForeignKey(
                        name: "FK__titleauth__au_id__31EC6D26",
                        column: x => x.au_id,
                        principalTable: "authors",
                        principalColumn: "au_id");
                    table.ForeignKey(
                        name: "FK__titleauth__title__32E0915F",
                        column: x => x.title_id,
                        principalTable: "titles",
                        principalColumn: "title_id");
                });

            migrationBuilder.CreateIndex(
                name: "aunmind",
                table: "authors",
                columns: new[] { "au_lname", "au_fname" });

            migrationBuilder.CreateIndex(
                name: "IX_discounts_stor_id",
                table: "discounts",
                column: "stor_id");

            migrationBuilder.CreateIndex(
                name: "employee_ind",
                table: "employee",
                columns: new[] { "lname", "fname", "minit" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_job_id",
                table: "employee",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_pub_id",
                table: "employee",
                column: "pub_id");

            migrationBuilder.CreateIndex(
                name: "titleidind",
                table: "roysched",
                column: "title_id");

            migrationBuilder.CreateIndex(
                name: "titleidind",
                table: "sales",
                column: "title_id");

            migrationBuilder.CreateIndex(
                name: "auidind",
                table: "titleauthor",
                column: "au_id");

            migrationBuilder.CreateIndex(
                name: "titleidind",
                table: "titleauthor",
                column: "title_id");

            migrationBuilder.CreateIndex(
                name: "IX_titles_pub_id",
                table: "titles",
                column: "pub_id");

            migrationBuilder.CreateIndex(
                name: "titleind",
                table: "titles",
                column: "title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "pub_info");

            migrationBuilder.DropTable(
                name: "roysched");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "titleauthor");

            migrationBuilder.DropTable(
                name: "jobs");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "titles");

            migrationBuilder.DropTable(
                name: "publishers");
        }
    }
}
