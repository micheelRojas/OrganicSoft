using NUnit.Framework;
using OrganicSoft.Aplicacion.Dobles;
using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.Dobles
{
    class FacturaSpyTest
    {
        [Test]
        public void PuedoGenerarFacturaCorrecta()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente ha hecho un pedido
            var jabonSandia = new ProductoSimple(codigo: 4, nombre: "Jabón de Papaya",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new ProductoSimple(codigo: 5, nombre: "Exfoliante Hombre",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 4, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);
            ProductoVenta productoVenta2 = new ProductoVenta(codigoProducto: 5, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta2);
            Pedido pedido = new Pedido();
            pedido.GenerarPedido(codigo: 1, CarritoCompra: carrito);
            #endregion
            #region CUANDO el administrador confirma el pedido para generar la factura
            pedido.ConfirmarPedido();
            var factura = new FacturaSpy(pedido);

            #endregion
            #region ENTONCES  el sistema mostrará la cantidad de llamadas del método CalcularTotal que es equivalente a la cantidad de detalles de la factura"
            Assert.AreEqual("La cantidad de detalles de la factura es 2", "La cantidad de detalles de la factura es " + factura.CantidadLlamadas);
            #endregion

        }
    }
}
