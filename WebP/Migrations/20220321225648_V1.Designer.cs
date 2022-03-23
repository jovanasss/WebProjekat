﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace WebP.Migrations
{
    [DbContext(typeof(AgencijaContext))]
    [Migration("20220321225648_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Agencija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Agencija");
                });

            modelBuilder.Entity("Models.Dan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Dan");
                });

            modelBuilder.Entity("Models.Posao", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Nedelja")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Posao");
                });

            modelBuilder.Entity("Models.Radnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgencijaID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("JMBG")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("AgencijaID");

                    b.ToTable("Radnik");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DanID")
                        .HasColumnType("int");

                    b.Property<int>("Honorar")
                        .HasColumnType("int");

                    b.Property<int?>("PosaoID")
                        .HasColumnType("int");

                    b.Property<int?>("RadnikID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DanID");

                    b.HasIndex("PosaoID");

                    b.HasIndex("RadnikID");

                    b.ToTable("Spoj");
                });

            modelBuilder.Entity("Models.Radnik", b =>
                {
                    b.HasOne("Models.Agencija", "Agencija")
                        .WithMany("AgencijaRadnici")
                        .HasForeignKey("AgencijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agencija");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.HasOne("Models.Dan", "Dan")
                        .WithMany("RadniciPoslovi")
                        .HasForeignKey("DanID");

                    b.HasOne("Models.Posao", "Posao")
                        .WithMany("PosaoRadnik")
                        .HasForeignKey("PosaoID");

                    b.HasOne("Models.Radnik", "Radnik")
                        .WithMany("RadnikPosao")
                        .HasForeignKey("RadnikID");

                    b.Navigation("Dan");

                    b.Navigation("Posao");

                    b.Navigation("Radnik");
                });

            modelBuilder.Entity("Models.Agencija", b =>
                {
                    b.Navigation("AgencijaRadnici");
                });

            modelBuilder.Entity("Models.Dan", b =>
                {
                    b.Navigation("RadniciPoslovi");
                });

            modelBuilder.Entity("Models.Posao", b =>
                {
                    b.Navigation("PosaoRadnik");
                });

            modelBuilder.Entity("Models.Radnik", b =>
                {
                    b.Navigation("RadnikPosao");
                });
#pragma warning restore 612, 618
        }
    }
}
