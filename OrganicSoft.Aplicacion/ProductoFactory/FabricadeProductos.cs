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
        public object metodoFabrica(string tipo)
        {
            Producto producto;
            switch (tipo.ToUpper())
            {
                case "SIMPLE":
                    producto = new ProductoSimple();
                    break;
                case "COMBO":
                    producto = new ProductoCombo();
                    break;
                
                default:
                    producto = null;
                    break;
            }
            return producto;
        }
    }
}
