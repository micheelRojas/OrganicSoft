using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrganicSoft.Aplicacion.CarritoDeCompra;
using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura;
using OrganicSoft.Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrganicSoft.Aplicacion.CarritoDeCompra.AgregarAlCarritoCommandHandle;
using static OrganicSoft.Aplicacion.CarritoDeCompra.CrearCarritoCompraCommandHandle;

namespace OrganicSoft.Test.PruebasdeAplicacion
{
    class CarritoCompraTestAplicacion
    {
        private OrganicSoftContext _context;
        private AgregarAlCarritoCommandHandle _agregarAlCarritoService;
        private CrearCarritoCompraCommandHandle _crearCarritoService;
        [SetUp]
        public void Setup()
        {
            var optionsInMemory = new DbContextOptionsBuilder<OrganicSoftContext>().UseInMemoryDatabase("organicSoft").Options;
            _context = new OrganicSoftContext(optionsInMemory);

            _agregarAlCarritoService = new AgregarAlCarritoCommandHandle(
                new UnitOfWork(_context),
                new CarritoCompraRepository(_context),
                new ProductoRepository(_context));

            _crearCarritoService = new CrearCarritoCompraCommandHandle(
                new UnitOfWork(_context),
                new CarritoCompraRepository(_context));
        }
        [Test]
        public void PuedoAgregarProductosAlCarritoAplicacion()
        {
            //Arange
            var jabonSandia = new ProductoSimple(codigo: 543, nombre: "Jabón de Frutal",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new ProductoSimple(codigo: 544, nombre: "Exfoliante Frutal",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);
            _context.Producto.Add(jabonSandia);
            _context.SaveChanges();

            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 543, cantidadVenta: 2);
            _context.CarritoCompra.Add(carrito);
            _context.SaveChanges();

            //Act
            var response = _agregarAlCarritoService.Handle(new AgregarAlCarritoCommand(1, productoVenta, carrito.Id));
            //Assert
            Assert.AreEqual($"Se ha agregado correctamente el producto", response.Mensaje);

            _context.CarritoCompra.Remove(carrito);
            _context.Producto.Remove(jabonSandia);
            _context.SaveChanges();

        }

        [Test]
        public void PuedoCrearCarritoCompraAplicacion()
        {
            //Arange
            var jabonSandia = new ProductoSimple(codigo: 543, nombre: "Jabón de Frutal",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new ProductoSimple(codigo: 544, nombre: "Exfoliante Frutal",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);
            _context.Producto.Add(jabonSandia);
            _context.SaveChanges();

            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");

            _context.CarritoCompra.Add(carrito);
            _context.SaveChanges();

            //Act
            var response = _crearCarritoService.Handle(new CrearCarritoCommand(132, 342, "1002343454"));
            //Assert
            Assert.AreEqual($"Se creó con exito el carrito de compras.", response.Mensaje);

            _context.CarritoCompra.Remove(carrito);
            _context.Producto.Remove(jabonSandia);
            _context.SaveChanges();

        }

        [Test]
        public void NoPuedoCrearCarritoCompraAplicacion()
        {
            //Arange
            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");

            _context.CarritoCompra.Add(carrito);
            _context.SaveChanges();

            //Act
            var response = _crearCarritoService.Handle(new CrearCarritoCommand(1, 342, "1002343454"));
            //Assert
            Assert.AreEqual($"El carrito ya exite", response.Mensaje);

            _context.CarritoCompra.Remove(carrito);
            _context.SaveChanges();

        }
    }
}
