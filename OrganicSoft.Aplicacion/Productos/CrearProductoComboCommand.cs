using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Productos
{
    public class CrearProductoComboCommand : CrearProductosCommand
    {
        public CrearProductoComboCommand()
        {
        }

        public CrearProductoComboCommand(int id,int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock, List<ComponenteCommand> componentes) : base(id,codigo, nombre, decripcion, precio, categoria, presentacion, minimoStock,0)
        {
            Componentes = componentes;
        }


        public List<ComponenteCommand> Componentes { get; set; }
        public override IReadOnlyList<string> CanCrear() {
            var errors = new List<string>();
            if (Componentes==null)
                errors.Add("No se han establecidos los componentes del producto combo");
            return errors;
        }
    }
}
