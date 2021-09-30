using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
   public class CarritoCompra
    {
        public string CedulaCliente { get; private set; }
        protected List<ProductoVenta> _productoVentas;

        public CarritoCompra(string cedulaCliente)
        {
            CedulaCliente = cedulaCliente;
            _productoVentas = new List<ProductoVenta>();
        }
        public IReadOnlyCollection<ProductoVenta> ProductosVenta => _productoVentas.AsReadOnly();

        public string AgregarAlCarrito(ProductoVenta productoVenta)
        {
            String respuesta = "No se encontró el producto";
            if (productoVenta.CantidadVenta <= 0)
            {
                respuesta = "La cantidad del producto debe ser mayor a cero";
                return respuesta;
            }
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

        public string EliminarDelCarrito(int codigoProductoVenta)
        {

            String respuesta = "No se pudo eliminar el producto";
            foreach (var producto in ProductosVenta)
            {
                if (producto.CodigoProducto.Equals(codigoProductoVenta))
                {
                    _productoVentas.Remove(producto);
                    respuesta = "Se eliminó el productó correctamente";
                    return respuesta;
                }
            }
            return respuesta;
        }

        public string DisminuirCantidadProducto(int codigoProductoVenta, int cantidad)
        {
            String respuesta = "No se encontró el producto";
            if (cantidad <= 0)
            {
                respuesta = "La cantidad a disminuir debe ser mayor a cero";
                return respuesta;
            }
            foreach (var productoVenta in ProductosVenta)
            {
                if (codigoProductoVenta.Equals(productoVenta.CodigoProducto))
                {
                    if (productoVenta.CantidadVenta > cantidad)
                    {
                        productoVenta.DisminuirCantidadProductoVenta(cantidad);
                        foreach (var producto in Producto.Productos)
                        {
                            if (codigoProductoVenta.Equals(producto.CodigoProducto))
                            {
                                return respuesta = $"La nueva cantidad del producto {producto.Nombre} en el carrito es {productoVenta.CantidadVenta}";

                            }
                        }
                    }
                    else if (productoVenta.CantidadVenta <= cantidad)
                    {
                        respuesta = "La cantidad a disminuir debe ser menor a la cantidad existente en el carrito";
                    }


                }
            }

            return respuesta;
        }

        public string AumentarCantidadProducto(int codigoProductoVenta, int cantidad)
        {
            String respuesta = "No se encontró el producto";
            if (cantidad <= 0)
            {
                respuesta = "La cantidad a aumentar debe ser mayor a cero";
                return respuesta;
            }
            foreach (var productoVenta in ProductosVenta)
            {
                if (codigoProductoVenta.Equals(productoVenta.CodigoProducto))
                {
                    productoVenta.AumentarCantidadProductoVenta(cantidad);
                    foreach (var producto in Producto.Productos)
                    {
                        if (codigoProductoVenta.Equals(producto.CodigoProducto))
                        {
                            return respuesta = $"La nueva cantidad del producto {producto.Nombre} en el carrito es {productoVenta.CantidadVenta}";

                        }
                    }
                }
            }
            return respuesta;
        }
    }
}
