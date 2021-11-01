using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using OrganicSoft.Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Infraestructura
{
    public class CarritoCompraRepository : GenericRepository<CarritoCompra>, ICarritoCompraRepository
    {
        public CarritoCompraRepository(IDbContext context) : base(context)
        {

        }
    }
}
