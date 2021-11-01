﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrganicSoft.Infraestructura;

namespace OrganicSoft.Infraestructura.Migrations
{
    [DbContext(typeof(OrganicSoftContext))]
    [Migration("20211101045008_MyMigration")]
    partial class MyMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrganicSoft.Dominio.Componente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("ProductoComboId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoComboId");

                    b.HasIndex("ProductoId");

                    b.ToTable("Componente");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Descuento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoDescuento")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<double>("PorcentajeDescuento")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Descuento");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Detalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CantidadVendida")
                        .HasColumnType("int");

                    b.Property<int>("CodigoFactura")
                        .HasColumnType("int");

                    b.Property<int>("CodigoProducto")
                        .HasColumnType("int");

                    b.Property<int?>("FacturaId")
                        .HasColumnType("int");

                    b.Property<double>("Subtotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FacturaId");

                    b.ToTable("Detalle");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Factura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CedulaCliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<int?>("FacturaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPagar")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FacturaId");

                    b.ToTable("Factura");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CantidadExistente")
                        .HasColumnType("int");

                    b.Property<int>("CantidadVendida")
                        .HasColumnType("int");

                    b.Property<string>("Categoria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CodigoProducto")
                        .HasColumnType("int");

                    b.Property<double>("Costo")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DescuentoId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MinimoStock")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.Property<double>("PrecioConDescuento")
                        .HasColumnType("float");

                    b.Property<string>("Presentacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DescuentoId");

                    b.ToTable("Producto");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Producto");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.ProductoCombo", b =>
                {
                    b.HasBaseType("OrganicSoft.Dominio.Producto");

                    b.Property<double>("Utilidad")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("ProductoCombo");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.ProductoSimple", b =>
                {
                    b.HasBaseType("OrganicSoft.Dominio.Producto");

                    b.HasDiscriminator().HasValue("ProductoSimple");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Componente", b =>
                {
                    b.HasOne("OrganicSoft.Dominio.ProductoCombo", null)
                        .WithMany("Componentes")
                        .HasForeignKey("ProductoComboId");

                    b.HasOne("OrganicSoft.Dominio.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Detalle", b =>
                {
                    b.HasOne("OrganicSoft.Dominio.Factura", null)
                        .WithMany("Detalles")
                        .HasForeignKey("FacturaId");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Factura", b =>
                {
                    b.HasOne("OrganicSoft.Dominio.Factura", null)
                        .WithMany("Facturas")
                        .HasForeignKey("FacturaId");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Producto", b =>
                {
                    b.HasOne("OrganicSoft.Dominio.Descuento", "Descuento")
                        .WithMany()
                        .HasForeignKey("DescuentoId");

                    b.Navigation("Descuento");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.Factura", b =>
                {
                    b.Navigation("Detalles");

                    b.Navigation("Facturas");
                });

            modelBuilder.Entity("OrganicSoft.Dominio.ProductoCombo", b =>
                {
                    b.Navigation("Componentes");
                });
#pragma warning restore 612, 618
        }
    }
}
