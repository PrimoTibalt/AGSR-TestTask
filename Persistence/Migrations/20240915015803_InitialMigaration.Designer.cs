﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240915015803_InitialMigaration")]
    partial class InitialMigaration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Persistence.Given", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Given");
                });

            modelBuilder.Entity("Persistence.GivenName", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("GivenId")
                        .HasColumnType("integer");

                    b.Property<Guid>("NameId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GivenId");

                    b.HasIndex("NameId");

                    b.ToTable("GivenName");
                });

            modelBuilder.Entity("Persistence.Name", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Use")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Names");
                });

            modelBuilder.Entity("Persistence.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<Guid>("NameId")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("PatientId");

                    b.HasIndex("NameId")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Persistence.GivenName", b =>
                {
                    b.HasOne("Persistence.Given", "Given")
                        .WithMany("NameAssociations")
                        .HasForeignKey("GivenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Name", "Name")
                        .WithMany("GivenNames")
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Given");

                    b.Navigation("Name");
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

            modelBuilder.Entity("Persistence.Given", b =>
                {
                    b.Navigation("NameAssociations");
                });

            modelBuilder.Entity("Persistence.Name", b =>
                {
                    b.Navigation("GivenNames");
                });
#pragma warning restore 612, 618
        }
    }
}
