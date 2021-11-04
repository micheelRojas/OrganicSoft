using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrganicSoft.Aplicacion.Facturas;
using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura;
using OrganicSoft.Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.PruebasdeAplicacion
{
    class FacturaTestAplicacion
    {

       private OrganicSoftContext _context;
        private GenerarFacturaCommandHandle _facturaService;
        [SetUp]
        public void Setup()
        {
            var optionsInMemory = new DbContextOptionsBuilder<OrganicSoftContext>().UseInMemoryDatabase("organicSoft").Options;
            _context = new OrganicSoftContext(optionsInMemory);

            _facturaService = new GenerarFacturaCommandHandle(
                new UnitOfWork(_context),
                new FacturaRepository(_context));
        }
        [Test]
        public void PuedoGenerarFacturaAplicacion()
        {
            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente ha hecho un pedido
            var jabonSandia = new ProductoSimple(codigo: 20, nombre: "Jabón de Frutal",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new ProductoSimple(codigo: 21, nombre: "Exfoliante Frutal",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 20, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);
            ProductoVenta productoVenta2 = new ProductoVenta(codigoProducto: 21, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta2);
            Pedido pedido = new Pedido();
            pedido.GenerarPedido(codigo: 1, CarritoCompra: carrito);
            #endregion

            var respuesta = pedido.ConfirmarPedido();

            //Act
            var response = _facturaService.Handle(new GenerarFacturaCommand(id: 1, codigo: 1, cedulaCliente: "1202", pedido: pedido));
            //Assert
            Assert.AreEqual("Se creó con exito la factura.", response.Mensaje);



        }
    }
}
