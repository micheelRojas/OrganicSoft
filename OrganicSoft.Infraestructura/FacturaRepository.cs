using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura.Base;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Infraestructura
{
    public class FacturaRepository : GenericRepository<Factura>, IFacturaRepository
    {
        public FacturaRepository(IDbContext context) : base(context)
        {
        }
    }
}
