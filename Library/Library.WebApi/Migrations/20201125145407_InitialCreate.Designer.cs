﻿// <auto-generated />
using System;
using Library.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.WebApi.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20201125145407_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Library.WebApi.Repository.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Library.WebApi.Repository.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsCeo")
                        .HasColumnType("bit")
                        .HasColumnName("isCEO");

                    b.Property<bool>("IsManager")
                        .HasColumnType("bit")
                        .HasColumnName("isManager");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Library.WebApi.Repository.LibraryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("BorrowDate")
                        .HasColumnType("date");

                    b.Property<string>("Borrower")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBorrowable")
                        .HasColumnType("bit")
                        .HasColumnName("isBorrowable");

                    b.Property<int?>("Pages")
                        .HasColumnType("int");

                    b.Property<int?>("RunTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("LibraryItem");
                });

            modelBuilder.Entity("Library.WebApi.Repository.LibraryItem", b =>
                {
                    b.HasOne("Library.WebApi.Repository.Category", "Category")
                        .WithMany("LibraryItems")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_LibraryItem_Category")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Library.WebApi.Repository.Category", b =>
                {
                    b.Navigation("LibraryItems");
                });
#pragma warning restore 612, 618
        }
    }
}
