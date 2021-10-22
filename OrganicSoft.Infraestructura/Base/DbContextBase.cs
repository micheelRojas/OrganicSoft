using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Infraestructura.Base
{
    public class DbContextBase : DbContext, IDbContext
    {
        public DbContextBase()
        {
        }
        public DbContextBase(DbContextOptions options) : base(options)
        {

        }
    }
}
