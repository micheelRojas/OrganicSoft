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
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasKey(c => c.Id);
            modelBuilder.Entity<Descuento>().HasKey(c => c.Id);
            modelBuilder.Entity<Componente>().HasKey(c => c.Id);

            //control de concurrencia
            //Campo adicional de version 
            //modelBuilder.Entity<Producto>().Property(p => p.CantidadExistente).IsRowVersion();
        }
       
    }
}
