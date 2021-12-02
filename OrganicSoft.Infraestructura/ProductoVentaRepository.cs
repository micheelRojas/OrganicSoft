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
    public class ProductoVentaRepository : GenericRepository<ProductoVenta>, IProductoVentaRepository
    {
        public ProductoVentaRepository(IDbContext context) : base(context)
        {

        }
    }
}
