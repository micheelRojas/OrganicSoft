using Microsoft.EntityFrameworkCore;
using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Infraestructura
{
   public  class OrganicSoftContext:  DbContextBase
    {
        public OrganicSoftContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Producto> Producto { get; set; }//equivale a Repositorios
        public DbSet<ProductoSimple> ProductoSimple { get; set; }
        public DbSet<ProductoCombo> ProductoCombo { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<CarritoCompra> CarritoCompra { get; set; }
        public DbSet<Componente> Componente { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasKey(c => c.Id);
            modelBuilder.Entity<Descuento>().HasKey(c => c.Id);
            modelBuilder.Entity<Componente>().HasKey(c => c.Id);
            modelBuilder.Entity<Factura>().HasKey(c => c.Id);
            modelBuilder.Entity<Detalle>().HasKey(c => c.Id);
            modelBuilder.Entity<Pedido>().HasKey(c => c.Id);
            modelBuilder.Entity<CarritoCompra>().HasKey(c => c.Id);
            modelBuilder.Entity<ProductoVenta>().HasKey(c => c.Id);

            //control de concurrencia
            //Campo adicional de version 
            //modelBuilder.Entity<Producto>().Property(p => p.CantidadExistente).IsRowVersion();
        }
       
    }
}
