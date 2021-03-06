using OrganicSoft.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public class ProductoVenta : Entity<int>, IAggregateRoot
    {
        public int CodigoProducto { get; private set; }
        public int CantidadVenta { get; private set; }
        public int CarritoCompraId { get; private set; }

        public ProductoVenta(int codigoProducto, int cantidadVenta)
        {
            CodigoProducto = codigoProducto;
            CantidadVenta = cantidadVenta;
        }
        public ProductoVenta() { }
        public int AumentarCantidadProductoVenta(int cantidad)
        {
            return CantidadVenta += cantidad;
        }

        public int DisminuirCantidadProductoVenta(int cantidad)
        {
            return CantidadVenta -= cantidad;
        }
    }
}
