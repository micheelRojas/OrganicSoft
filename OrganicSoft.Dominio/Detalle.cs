using OrganicSoft.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public class Detalle : Entity<int>, IAggregateRoot
    {
        public int CodigoFactura { get; private set; }
        public int CantidadVendida { get; private set; }
        public double Subtotal { get; private set; }
        public int CodigoProducto { get; private set; }

        public Detalle(int codigoFactura, int cantidadVendida, double subtotal, int codigoProducto)
        {
            CodigoFactura = codigoFactura;
            CantidadVendida = cantidadVendida;
            Subtotal = subtotal;
            CodigoProducto = codigoProducto;
        }
    }
}
