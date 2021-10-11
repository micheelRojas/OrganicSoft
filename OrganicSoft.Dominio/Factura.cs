using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public class Factura
    {
        public int Codigo { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public String CedulaCliente { get; private set; }
        public decimal TotalPagar { get; private set; }
        public List<Detalle> Detalles { get; private set; }
        protected List<Factura> _facturas = new List<Factura>();

        public Factura(int codigo, DateTime fechaCreacion, string cedulaCliente)
        {
            Codigo = codigo;
            FechaCreacion = fechaCreacion;
            CedulaCliente = cedulaCliente;
            Detalles = new List<Detalle>();
        }
        public IReadOnlyCollection<Factura> Facturas => _facturas.AsReadOnly();

        public object CalcularTotal(Pedido Pedido)
        {
            double totalPagar = 0;
            //int num = 0;
            double sumaSubtotalesDetalles = 0;
            foreach (ProductoVenta productoVenta in Pedido.Carrito.ProductosVenta)
            {
                foreach (Producto producto in Producto.Productos)
                {
                    if (productoVenta.CodigoProducto.Equals(producto.CodigoProducto))
                    {
                        totalPagar = totalPagar + (producto.PrecioConDescuento * productoVenta.CantidadVenta);
                        producto.DisminuirCantidadProductoStock(productoVenta.CantidadVenta);
                        Detalle detalle = new Detalle(codigoFactura: this.Codigo, cantidadVendida: productoVenta.CantidadVenta, subtotal: productoVenta.CantidadVenta * producto.Precio);
                        sumaSubtotalesDetalles += detalle.Subtotal;
                        this.Detalles.Add(detalle);
                        _facturas.Add(this);
                        //num += producto.CantidadExitente;
                    }
                }
            }
            return $"El total a pagar es de {totalPagar} pesos. La suma de subtotales de detalles es {sumaSubtotalesDetalles}";
            throw new NotImplementedException();
        }
    }
}
