﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace PatientREST.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240321230521_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("GivenName", b =>
                {
                    b.Property<int>("GivenId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("NamesId")
                        .HasColumnType("TEXT");

                    b.HasKey("GivenId", "NamesId");

                    b.HasIndex("NamesId");

                    b.ToTable("GivenName", (string)null);
                });

            modelBuilder.Entity("Persistence.Given", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Given");
                });

            modelBuilder.Entity("Persistence.Name", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Use")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Names");
                });

            modelBuilder.Entity("Persistence.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("NameId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasName("PatientId");

                    b.HasIndex("NameId")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("GivenName", b =>
                {
                    b.HasOne("Persistence.Given", null)
                        .WithMany()
                        .HasForeignKey("GivenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Name", null)
                        .WithMany()
                        .HasForeignKey("NamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Persistence.Patient", b =>
                {
                    b.HasOne("Persistence.Name", "Name")
                        .WithOne()
                        .HasForeignKey("Persistence.Patient", "NameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Name");
                });
#pragma warning restore 612, 618
        }
    }
}
