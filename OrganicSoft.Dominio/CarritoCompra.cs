using OrganicSoft.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
   public class CarritoCompra : Entity<int>, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public string CedulaCliente { get; private set; }
        public List<ProductoVenta> ProductoVentas { get; private set; }

        Inventario inventario = Inventario.getInventario();
        public CarritoCompra() { }
        public CarritoCompra(int codigo, string cedulaCliente)
        {
            Codigo = codigo;
            CedulaCliente = cedulaCliente;
            ProductoVentas = new List<ProductoVenta>();
        }
        public IReadOnlyCollection<ProductoVenta> ProductosVenta => ProductoVentas.AsReadOnly();

        public string AgregarAlCarrito(ProductoVenta productoVenta)
        {
            String respuesta = "";
            if (productoVenta.CantidadVenta <= 0)
            {
                respuesta = "La cantidad del producto debe ser mayor a cero";
                return respuesta;
            }
            


            ProductoVentas.Add(productoVenta);
            respuesta = $"Se ha agregado correctamente el producto";
               
            return respuesta;
        }

        public string EliminarDelCarrito(int codigoProductoVenta)
        {

            String respuesta = "No se pudo eliminar el producto";
            foreach (var producto in ProductosVenta)
            {
                if (producto.CodigoProducto.Equals(codigoProductoVenta))
                {
                    ProductoVentas.Remove(producto);
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
                        foreach (var producto in inventario.productos)
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
                    foreach (var producto in inventario.productos)
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
