using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using OrganicSoft.Infraestructura.Base;
using System;

namespace OrganicSoft.Infraestructura
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(IDbContext context) : base(context)
        {
        }
    }
}
