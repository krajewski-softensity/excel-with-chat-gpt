﻿// <auto-generated />
using System;
using CrmToRecruit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrmToRecruit.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CrmToRecruit.Domain.CrmToRecruitEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ConfirmedST")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ConfirmedVendor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DealName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DealOwner")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("InterviewedST")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("InterviewedVendor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("JobOpeningCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastActivityTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MustHaveSkills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NiceToHaveSkills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotesPriority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfResources")
                        .HasColumnType("int");

                    b.Property<string>("ProjectDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RejectedST")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RejectedVendor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RmOwnership")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StageOfClosed")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StageOfOpen")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubmittedST")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubmittedVendor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CrmToRecruit");
                });

            modelBuilder.Entity("CrmToRecruit.Domain.ExcelData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmedST")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmedVendor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealOwner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterviewedST")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterviewedVendor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("JobOpeningCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastActivityTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MustHaveSkills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NiceToHaveSkills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotesPriority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfResources")
                        .HasColumnType("int");

                    b.Property<string>("ProjectDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RMOwnership")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RejectedST")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RejectedVendor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StageOfClosed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StageOfOpen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubmittedST")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubmittedVendor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExcelData");
                });
#pragma warning restore 612, 618
        }
    }
}
