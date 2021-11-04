using Moq;
using NUnit.Framework;
using OrganicSoft.Aplicacion.Pedidos;
using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura;
using OrganicSoft.Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.Dobles
{
    class FacturaDummyTest
    {
       /* private CrearPedidoCommandHandle _crearPedidoService;
        private OrganicSoftContext _dbContext;

        [Test]
        public void PuedoGenerarPedidoCorrectoDummy()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras lleno
            var jabonSandia = new ProductoSimple(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new ProductoSimple(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);


            Mock<CarritoCompra> _carrito = new Mock<CarritoCompra>();
            Mock<ProductoVenta> _productoVenta = new Mock<ProductoVenta>();

            //CarritoCompra carrito = new CarritoCompra();//Dummy
            //ProductoVenta productoVenta = new ProductoVenta();//Dummy
           // _carrito.Setup(p =>p.AgregarAlCarrito(_productoVenta.Object)).Returns(It.IsAny<String>);
            //carrito.AgregarAlCarrito(productoVenta);
            #endregion
            #region CUANDO el cliente desea generar el pedido para dar por terminada la compra
            _crearPedidoService = new CrearPedidoCommandHandle(
                 new UnitOfWork(_dbContext),
                 new PedidoRepository(_dbContext));

            Pedido pedido = new Pedido();
            var respuesta = _crearPedidoService.Handle(new CrearPedidoCommand(10000, 10000, _carrito.Object));
            #endregion
            #region ENTONCES  el sistema generará el pedido y mostrara el mensaje "Se creó un nuevo pedido para el cliente con cédula 1002353645"
            Assert.AreEqual($"Se creó con exito el pedido.", respuesta);
            #endregion

        }*/
    }
}
