using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrganicSoft.Aplicacion;
using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura;
using OrganicSoft.Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrganicSoft.Aplicacion.CrearProductoCommandHandle;
using static OrganicSoft.Aplicacion.EntradadeProductosCommandHandle;

namespace OrganicSoft.Test.PruebasdeAplicacion
{
    class ProductoTestAplicaion
    {
        private CrearProductoCommandHandle _productoCrearService;
        private EntradadeProductosCommandHandle _productoEntradaService;
        private OrganicSoftContext _context;
       
        [SetUp]
        public void Setup()
        {
            var optionsInMemory = new DbContextOptionsBuilder<OrganicSoftContext>().UseInMemoryDatabase("organicSoft").Options;
            _context = new OrganicSoftContext(optionsInMemory);

            _productoCrearService = new CrearProductoCommandHandle(
                new UnitOfWork(_context),
                new ProductoRepository(_context));
            _productoEntradaService = new EntradadeProductosCommandHandle(
               new UnitOfWork(_context),
               new ProductoRepository(_context));
        }
        [Test]
        public void PuedoCrearProductosAplicacion()
        {
            //Arrange , Act
            var response = _productoCrearService.Handle(new CrearProductosCommand(tipoProducto:"Simple",id:10293,codigoProducto: 10976, nombre: "Jabon de Maracuya",
            descripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3));
            // Assert
            Assert.AreEqual("Se creó con exito el producto.", response.Mensaje);
        }
        [Test]
        public void PuedoRealizarEntradaProductosAplicacionCorreta()
        {
            //Arrange 

             Producto producto=new ProductoSimple(codigo: 2345, nombre: "Jabon de Arandano",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            _context.Producto.Add(producto);
            _context.SaveChanges();
            //Act

            var response = _productoEntradaService.Handle(new EntradadeProductosCommand(id: 2345, cantidad: 1));
            
            // Assert
            Assert.AreEqual($"La cantidad de Jabon de Arandano es: 1", response.Mensaje);
            _context.Producto.Remove(producto);
            _context.SaveChanges();
        }
        [Test]
        public void PuedoRealizarEntradaProductosAplicacionIncorrecta()
        {
            //Arrange 

            Producto producto = new ProductoSimple(codigo: 567, nombre: "Jabon de Fresas",
           decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
           " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
           " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
           "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            _context.Producto.Add(producto);
            _context.SaveChanges();
            //Act

            var response = _productoEntradaService.Handle(new EntradadeProductosCommand(id: 567, cantidad: -1));

            // Assert
            Assert.AreEqual($"La cantidad debe ser mayor a cero", response.Mensaje);
            _context.Producto.Remove(producto);
            _context.SaveChanges();
        }
    }
}
