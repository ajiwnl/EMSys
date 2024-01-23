using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    Password = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileIcon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => new { x.UserName, x.Password });
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    ENRHFSTUDID = table.Column<long>(type: "bigint", nullable: false),
                    ENRHFSTUDDATEENROLL = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENRHFSTUDSCHLYR = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ENRHFSTUDENCODER = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ENRHFSTUDTOTALUNITS = table.Column<double>(type: "float", nullable: false),
                    ENRHFSTUDSTATUS = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.ENRHFSTUDID);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentDetails",
                columns: table => new
                {
                    ENRDFSTUDID = table.Column<long>(type: "bigint", nullable: false),
                    ENRDFSTUDEDPCODE = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ENRDFSTUDSUBJCODE = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ENRDFSTUDSTATUS = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentDetails", x => new { x.ENRDFSTUDID, x.ENRDFSTUDEDPCODE });
                });

            migrationBuilder.CreateTable(
                name: "SSFSched",
                columns: table => new
                {
                    SSFEDPCODE = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    SSFCOURSECODE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SSFSUBJCODE = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SSFSTARTTIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SSFENDTIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SSFDAYS = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    SSFROOM = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    SSFMAXSIZE = table.Column<int>(type: "int", nullable: false),
                    SSFCLASSSIZE = table.Column<int>(type: "int", nullable: false),
                    SSFSTATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSFXM = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    SSFSECTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSFSCHOOLYEAR = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSFSched", x => x.SSFEDPCODE);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    STFSTUDID = table.Column<long>(type: "bigint", nullable: false),
                    STFSTUDLNAME = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    STFSTUDFNAME = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    STFSTUDMNAME = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    STFSTUDCOURSE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    STFSTUDYEAR = table.Column<int>(type: "int", nullable: false),
                    STFSTUDREMARKS = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    STFSTUDSTATUS = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.STFSTUDID);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SUBJCODE = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SUBJCOURSECODE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SUBJPREQ = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    SUBJCODEPREQ = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SUBJCATEGORY = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    SUBJDESC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SUBJUNITS = table.Column<int>(type: "int", nullable: false),
                    SUBJREGOFRNG = table.Column<int>(type: "int", nullable: false),
                    SUBJSTATUS = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    SUBJCURRCODE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => new { x.SUBJCODE, x.SUBJCOURSECODE, x.SUBJCATEGORY, x.SUBJCODEPREQ });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "EnrollmentDetails");

            migrationBuilder.DropTable(
                name: "SSFSched");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
