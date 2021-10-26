using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.ProductoFactory
{
   public  interface IFabrica
    {
       public Object metodoFabrica(string tipo, int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock, List<Componente> componetes, double costo);
    }
}
