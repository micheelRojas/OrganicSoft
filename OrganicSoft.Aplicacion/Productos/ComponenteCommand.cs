using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Productos
{
    public class ComponenteCommand
    {
        public CrearProductosCommand Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
