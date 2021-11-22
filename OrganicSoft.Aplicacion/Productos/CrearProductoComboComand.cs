using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Productos
{
    public class CrearProductoComboComand : CrearProductosCommand
    {
        public CrearProductoComboComand()
        {
        }

        public CrearProductoComboComand(int id,int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock, List<Componente> componentes) : base(id,codigo, nombre, decripcion, precio, categoria, presentacion, minimoStock,0)
        {
            Componentes = componentes;
        }


        public List<Componente> Componentes { get; set; }
        public override IReadOnlyList<string> CanCrear() {
            var errors = new List<string>();
            if (Componentes==null)
                errors.Add("No se han establecidos los componentes del producto combo");
            return errors;
        }
    }
}
