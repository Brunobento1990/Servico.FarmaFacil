﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servico.FarmaFacil.Context;

#nullable disable

namespace Servico.FarmaFacil.Migrations
{
    [DbContext(typeof(ContextServico))]
    [Migration("20230422170315_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Servico.FarmaFacil.Database.Entidades.Servico.PrimeiraIntegracao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("DataDaIntegracao")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("PrimeiraIntegracao");
                });
#pragma warning restore 612, 618
        }
    }
}
