using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Infraestructura.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
        public UnitOfWork(IDbContext context) => _context = context;

        public void Commit()
        {
            _context.SaveChanges();
        }


    }
}
