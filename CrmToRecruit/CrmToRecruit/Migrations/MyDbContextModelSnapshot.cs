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

            modelBuilder.Entity("CrmToRecruit.ExcelData", b =>
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
