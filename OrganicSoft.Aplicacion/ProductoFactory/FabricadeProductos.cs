using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.ProductoFactory
{
    public class FabricadeProductos : IFabrica
    {
        public object metodoFabrica(string tipo, int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock, List<Componente> componetes, double costo)
        {
            Producto producto;
            switch (tipo.ToUpper())
            {
                case "SIMPLE":
                    producto = new ProductoSimple(codigo, nombre, decripcion, precio, categoria, presentacion, minimoStock);
                    break;
                case "COMBO":
                    producto = new ProductoCombo(codigo, nombre, decripcion, precio, categoria, presentacion, minimoStock);
                    break;
                
                default:
                    producto = null;
                    break;
            }
            return producto;
        }

        
    }
}
