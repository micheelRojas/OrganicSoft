using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.ProductoFactory
{
   public  interface IFabrica
    {
       public Object metodoFabrica(string tipo);
    }
}
