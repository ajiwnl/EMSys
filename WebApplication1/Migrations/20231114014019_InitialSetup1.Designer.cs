﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231114014019_InitialSetup1")]
    partial class InitialSetup1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.Credentials", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnOrder(0)
                        .UseCollation("SQL_Latin1_General_CP1_CS_AS");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(1);

                    b.Property<int?>("ProfileIcon")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("UserName", "Password");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("WebApplication1.Models.Enrollment", b =>
                {
                    b.Property<long?>("ENRHFSTUDID")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ENRHFSTUDDATEENROLL")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("ENRHFSTUDENCODER")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ENRHFSTUDSCHLYR")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ENRHFSTUDSTATUS")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<double?>("ENRHFSTUDTOTALUNITS")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("ENRHFSTUDID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("WebApplication1.Models.EnrollmentDetails", b =>
                {
                    b.Property<long?>("ENRDFSTUDID")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("ENRDFSTUDEDPCODE")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnOrder(2);

                    b.Property<string>("ENRDFSTUDSTATUS")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("ENRDFSTUDSUBJCODE")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ENRDFSTUDID", "ENRDFSTUDEDPCODE");

                    b.ToTable("EnrollmentDetails");
                });

            modelBuilder.Entity("WebApplication1.Models.Student", b =>
                {
                    b.Property<long>("STFSTUDID")
                        .HasColumnType("bigint")
                        .HasColumnOrder(0);

                    b.Property<string>("STFSTUDCOURSE")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("STFSTUDFNAME")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("STFSTUDLNAME")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("STFSTUDMNAME")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("STFSTUDREMARKS")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("STFSTUDSTATUS")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<int>("STFSTUDYEAR")
                        .HasColumnType("int");

                    b.HasKey("STFSTUDID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("WebApplication1.Models.Subject", b =>
                {
                    b.Property<string>("SUBJCODE")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnOrder(1);

                    b.Property<string>("SUBJCOURSECODE")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnOrder(2);

                    b.Property<string>("SUBJCATEGORY")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("SUBJCODEPREQ")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnOrder(4);

                    b.Property<string>("SUBJCURRCODE")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SUBJDESC")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SUBJPREQ")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnOrder(3);

                    b.Property<int>("SUBJREGOFRNG")
                        .HasColumnType("int");

                    b.Property<string>("SUBJSTATUS")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<int>("SUBJUNITS")
                        .HasColumnType("int");

                    b.HasKey("SUBJCODE", "SUBJCOURSECODE", "SUBJCATEGORY", "SUBJCODEPREQ");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("WebApplication1.Models.SubjectSched", b =>
                {
                    b.Property<string>("SSFEDPCODE")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int?>("SSFCLASSSIZE")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("SSFCOURSECODE")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SSFDAYS")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<DateTime>("SSFENDTIME")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SSFMAXSIZE")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("SSFROOM")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int?>("SSFSCHOOLYEAR")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("SSFSECTION")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SSFSTARTTIME")
                        .HasColumnType("datetime2");

                    b.Property<string>("SSFSTATUS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SSFSUBJCODE")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("SSFXM")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("SSFEDPCODE");

                    b.ToTable("SSFSched");
                });
#pragma warning restore 612, 618
        }
    }
}
