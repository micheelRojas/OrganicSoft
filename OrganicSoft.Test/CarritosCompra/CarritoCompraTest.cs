using NUnit.Framework;
using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.Facturas
{
    class CarritoCompraTest
    {
        /*
      * Como administrador 
      * quiero confirmar los pedidos de los clientes 
      * para poder generar las facturas correspondientes a cada pedido
      */
        [Test]
        public void PuedoAgregarProductoAlCarritoCompra()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras
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

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            
            #endregion
            #region CUANDO el cliente desea agregar productos al carrito de compra
            var respuesta = carrito.AgregarAlCarrito(productoVenta);
            #endregion
            #region ENTONCES  el sistema agregará el producto en el carrito y mostrara el mensaje "Se ha agregado 2 unidades del producto Jabón de Sandía"
            Assert.AreEqual("Se ha agregado 2 unidades del producto Jabón de Sandía", respuesta);
            #endregion

        }
    }

    internal class ProductoVenta
    {
        public int CodigoProducto { get; private set; }
        public int CantidadVenta { get; private set; }

        public ProductoVenta(int codigoProducto, int cantidadVenta)
        {
            CodigoProducto = codigoProducto;
            CantidadVenta = cantidadVenta;
        }
    }

    internal class CarritoCompra
    {
        public string CedulaCliente { get; private set; }
        protected List<ProductoVenta> _productoVentas;

        public CarritoCompra(string cedulaCliente)
        {
            CedulaCliente = cedulaCliente;
            _productoVentas = new List<ProductoVenta>();
        }
        public IReadOnlyCollection<ProductoVenta> ProductosVenta => _productoVentas.AsReadOnly();

        internal string AgregarAlCarrito(ProductoVenta productoVenta)
        {
            String respuesta = "No se encontró el producto";
            foreach (var producto in Producto.Productos)
            {
                if (productoVenta.CodigoProducto.Equals(producto.CodigoProducto))
                {
                    _productoVentas.Add(productoVenta);
                    respuesta = $"Se ha agregado {productoVenta.CantidadVenta} unidades del producto {producto.Nombre}";
                }
            }

            return respuesta;
        }
    }
}
