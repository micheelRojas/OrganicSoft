using NUnit.Framework;
using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.Facturacion
{
    class FacturaTest
    {
        /*
      * Como usuario 
      * quiero poder generar pedido
      * luego de haber llenado mi carrito de compras
      */
        [Test]
        public void PuedoGenerarPedidoCorrecto()
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

            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);
            #endregion
            #region CUANDO el cliente desea generar el pedido para dar por terminada la compra
            Pedido pedido = new Pedido();
            var respuesta = pedido.GenerarPedido(codigo: 1, CarritoCompra: carrito);
            #endregion
            #region ENTONCES  el sistema generará el pedido y mostrara el mensaje "Se creó un nuevo pedido para el cliente con cédula 1002353645"
            Assert.AreEqual($"Se creó un nuevo pedido con código {pedido.CodigoPedido} en estado {pedido.Estado}", respuesta);
            #endregion

        }

        /*
        [Test]
        public void PuedoGenerarFacturaConDescuentosCorrecta()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente ha hecho un pedido de productos con descuento
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);
            var descuento = new Descuento(codigoDescuento: 1, fechaInicio: new DateTime(2021, 09, 28), fechaFin: new DateTime(2021, 10, 20), porcentajeDescuento: 0.2);
            jabonSandia.AplicarDescuento(descuento: descuento);
            var descuento2 = new Descuento(codigoDescuento: 2, fechaInicio: new DateTime(2021, 09, 28), fechaFin: new DateTime(2021, 10, 20), porcentajeDescuento: 0.2);
            exfoliante.AplicarDescuento(descuento: descuento2);

            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);
            ProductoVenta productoVenta2 = new ProductoVenta(codigoProducto: 2, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta2);
            Pedido pedido = new Pedido();
            pedido.GenerarPedido(codigo: 1, CarritoCompra: carrito);
            #endregion
            #region CUANDO el administrador confirma el pedido para generar la factura
            var respuesta = pedido.ConfirmarPedido(carrito.Codigo);
            Factura factura = new Factura(codigo: 1, fechaCreacion: DateTime.Now, cedulaCliente: carrito.CedulaCliente);
            var respuesta2 = factura.CalcularTotal(Pedido: pedido);
            #endregion
            #region ENTONCES  el sistema generará la factura con su total a pagar y descuentos, disminuirá los productos correspondientes en stock, cambiará el estado del pedido a CONFIRMADO y mostrará el mensaje "El total a pagar es de 32000 pesos"
            Assert.AreEqual("El total a pagar es de 32000 pesos", respuesta2);
            #endregion

        } */
        
        [Test]
        public void PuedoGenerarFacturaCorrecta()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente ha hecho un pedido
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

            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);
            ProductoVenta productoVenta2 = new ProductoVenta(codigoProducto: 2, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta2);
            Pedido pedido = new Pedido();
            pedido.GenerarPedido(codigo: 1, CarritoCompra: carrito);
            #endregion
            #region CUANDO el administrador confirma el pedido para generar la factura
            var respuesta = pedido.ConfirmarPedido();
            Factura factura = new Factura(codigo: 1, fechaCreacion: DateTime.Now, cedulaCliente: carrito.CedulaCliente, Pedido: pedido);
           
            #endregion
            #region ENTONCES  el sistema generará la factura con su total a pagar, disminuirá los productos correspondientes en stock, registrará los detalles, cambiará el estado del pedido a CONFIRMADO y mostrará el mensaje "El total a pagar es de 40000 pesos"
            Assert.AreEqual(40000, factura.TotalPagar);
            Assert.AreEqual("El nuevo estado del pedido es CONFIRMADO", respuesta);
            Assert.AreEqual("La cantidad de detalles de la factura es 2", "La cantidad de detalles de la factura es " + factura.Detalles.Count());
            #endregion

        }
        
    }
}
