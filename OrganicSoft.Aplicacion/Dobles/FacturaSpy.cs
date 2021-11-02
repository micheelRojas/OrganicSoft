using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Dobles
{
    public class FacturaSpy : IFactura
    {
        public int CantidadLlamadas { get; private set; }

        public FacturaSpy(Pedido pedido)
        {
            CalcularTotal(pedido);
        }

        Inventario inventario = Inventario.getInventario();

        public void CalcularTotal(Pedido Pedido)
        {
            if (Pedido.Estado.Equals("CONFIRMADO")) { 
                    foreach (ProductoVenta productoVenta in Pedido.Carrito.ProductosVenta)
                    {
                        foreach (Producto producto in inventario.productos)
                        {
                            if (productoVenta.CodigoProducto.Equals(producto.CodigoProducto))
                            {

                                CantidadLlamadas++;
                            }
                        }
                    }
            }
        }
    }
}
