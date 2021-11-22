using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrganicSoft.Aplicacion;
using OrganicSoft.Aplicacion.Productos;
using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura;
using OrganicSoft.Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrganicSoft.Aplicacion.CrearProductoSimpleCommandHandle;
using static OrganicSoft.Aplicacion.EntradadeProductosCommandHandle;
using static OrganicSoft.Aplicacion.SalidaProductoCommandHandle;

namespace OrganicSoft.Test.PruebasdeAplicacion
{
    class ProductoTestAplicaion
    {
        private CrearProductoSimpleCommandHandle _productoCrearService;
        private EntradadeProductosCommandHandle _productoEntradaService;
        private SalidaProductoCommandHandle _productoSalidaService;
        private OrganicSoftContext _context;
       
        [SetUp]
        public void Setup()
        {
            var optionsInMemory = new DbContextOptionsBuilder<OrganicSoftContext>().UseInMemoryDatabase("organicSoft").Options;
            _context = new OrganicSoftContext(optionsInMemory);

            _productoCrearService = new CrearProductoSimpleCommandHandle(
                new UnitOfWork(_context),
                new ProductoRepository(_context));
            _productoEntradaService = new EntradadeProductosCommandHandle(
               new UnitOfWork(_context),
               new ProductoRepository(_context));
            _productoSalidaService = new SalidaProductoCommandHandle(
              new UnitOfWork(_context),
              new ProductoRepository(_context));
        }
        [Test]
        public void PuedoCrearProductosAplicacion()
        {
            //Arrange , Act
            var response = _productoCrearService.Handle(new CrearProductosCommand(id:10293,codigoProducto: 10976, nombre: "Jabon de Maracuya",
            descripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3));
            // Assert
            Assert.AreEqual("Se creó con éxito el producto.", response.Result.Mensaje);
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
        [Test]
        public void PuedoRealizarSalidadProductosAplicacionCorrecta()
        {
            //Arrange 

            Producto producto = new ProductoSimple(codigo: 569, nombre: "Jabon de Te verde",
           decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
           " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
           " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
           "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 1);
            _context.Producto.Add(producto);
            _context.SaveChanges();
            _productoEntradaService.Handle(new EntradadeProductosCommand(id: 569, cantidad:4 ));
            //Act

            var response = _productoSalidaService.Handle(new SalidaProductosCommand(id: 569, cantidad: 1));

            // Assert
            Assert.AreEqual($"La cantidad de Jabon de Te verde es: 3", response.Mensaje);
            _context.Producto.Remove(producto);
            _context.SaveChanges();
        }
        [Test]
        public void NoPuedoRealizarSalidadProductosAplicacionConCatidadNegativa()
        {
            //Arrange 

            Producto producto = new ProductoSimple(codigo: 690, nombre: "Jabon de Te verde",
           decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
           " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
           " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
           "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 1);
            _context.Producto.Add(producto);
            _context.SaveChanges();
            _productoEntradaService.Handle(new EntradadeProductosCommand(id: 690, cantidad: 4));
            //Act

            var response = _productoSalidaService.Handle(new SalidaProductosCommand(id: 690, cantidad: -1));

            // Assert
            Assert.AreEqual($"La cantidad pedida debe ser mayor a cero", response.Mensaje);
            _context.Producto.Remove(producto);
            _context.SaveChanges();
        }
        [Test]
        public void NoPuedoRealizarSalidadProductosAplicacionProductosInsuficientes()
        {
            //Arrange 

            Producto producto = new ProductoSimple(codigo: 691, nombre: "Jabon de Te verde",
           decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
           " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
           " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
           "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 1);
            _context.Producto.Add(producto);
            _context.SaveChanges();
            _productoEntradaService.Handle(new EntradadeProductosCommand(id: 691, cantidad: 4));
            //Act

            var response = _productoSalidaService.Handle(new SalidaProductosCommand(id: 691, cantidad: 20));

            // Assert
            Assert.AreEqual($"No hay suficietes productos de Jabon de Te verde para realizar la operacion", response.Mensaje);
            _context.Producto.Remove(producto);
            _context.SaveChanges();
        }
    }
}
