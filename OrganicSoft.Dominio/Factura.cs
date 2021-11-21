using OrganicSoft.Dominio.Base;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public class Factura : Entity<int>, IAggregateRoot, IFactura
    {
        public int Codigo { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public String CedulaCliente { get; private set; }
        public double TotalPagar { get; private set; }
        public List<Detalle> Detalles { get; private set; }
        

        Inventario inventario = Inventario.getInventario();
        public Factura() { }
        public Factura(int codigo, DateTime fechaCreacion, string cedulaCliente, Pedido Pedido)
        {
            Codigo = codigo;
            FechaCreacion = fechaCreacion;
            CedulaCliente = cedulaCliente;
            Detalles = new List<Detalle>();
            CalcularTotal(Pedido);
        }

        public void CalcularTotal(Pedido Pedido)
        {
            if (Pedido.Estado.Equals("CONFIRMADO")) { 
                //int num = 0;
                double sumaSubtotalesDetalles = 0;
                foreach (ProductoVenta productoVenta in Pedido.Carrito.ProductosVenta)
                {
                    foreach (Producto producto in inventario.productos)
                    {
                        if (productoVenta.CodigoProducto.Equals(producto.CodigoProducto))
                        {
                            TotalPagar = TotalPagar + (producto.PrecioConDescuento * productoVenta.CantidadVenta);
                            producto.DisminuirCantidadProductoStock(productoVenta.CantidadVenta);
                            Detalle detalle = new Detalle(codigoFactura: this.Codigo, cantidadVendida: productoVenta.CantidadVenta, 
                                subtotal: productoVenta.CantidadVenta * producto.Precio, codigoProducto: producto.CodigoProducto);
                            sumaSubtotalesDetalles += detalle.Subtotal;
                            this.Detalles.Add(detalle);
                            //num += producto.CantidadExitente;
                        }
                    }
                }
            }
        }
    }
}
