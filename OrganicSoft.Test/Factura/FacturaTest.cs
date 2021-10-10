using NUnit.Framework;
using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.Factura
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

            CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);
            #endregion
            #region CUANDO el cliente desea generar el pedido para dar por terminada la compra
            Pedido pedido = new Pedido();
            var respuesta = pedido.GenerarPedido(codigo: 1, CarritoCompra: carrito);
            #endregion
            #region ENTONCES  el sistema generará el pedido y mostrara el mensaje "Se creó un nuevo pedido para el cliente con cédula 1002353645"
            Assert.AreEqual("Se creó un nuevo pedido para el cliente con cédula 1002353645", respuesta);
            #endregion

        }

        
    }

    internal class Pedido
    {
        public int Codigo { get; private set; }
        public String Estado { get; private set; }
        public CarritoCompra Carrito { get; private set; }
        protected List<Pedido> _pedidos;

        public Pedido()
        {
            _pedidos = new List<Pedido>();
        }

        public IReadOnlyCollection<Pedido> Pedidos => _pedidos.AsReadOnly();

        internal String GenerarPedido(int codigo, CarritoCompra CarritoCompra)
        {
            if (CarritoCompra != null)
            {
                Codigo = codigo;
                Estado = "NO CONFIRMADO";
                Carrito= CarritoCompra;
                _pedidos.Add(this);
                return $"Se creó un nuevo pedido para el cliente con cédula {CarritoCompra.CedulaCliente}";
            }
            throw new NotImplementedException();
        }

        internal String ConfirmarPedido(int codigo)
        {
            foreach (Pedido pedido in Pedidos)
            {
                if (pedido.Carrito.Codigo.Equals(codigo))
                {
                    pedido.Estado = "CONFIRMADO";
                    return $"El nuevo estado del pedido es {pedido.Estado}";
                }
            }
            throw new NotImplementedException();
        }
    }
}
