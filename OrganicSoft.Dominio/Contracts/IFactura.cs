using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio.Contracts
{
    public interface IFactura
    {
        abstract void CalcularTotal(Pedido Pedido);
    }
}
