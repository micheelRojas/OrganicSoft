using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public class Detalle
    {
        public int CodigoFactura { get; private set; }
        public int CantidadVendida { get; private set; }
        public double Subtotal { get; private set; }

        public Detalle(int codigoFactura, int cantidadVendida, double subtotal)
        {
            CodigoFactura = codigoFactura;
            CantidadVendida = cantidadVendida;
            Subtotal = subtotal;
        }
    }
}
