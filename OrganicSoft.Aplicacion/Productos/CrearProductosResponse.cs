using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Productos
{
    public record CrearProductosResponse
    {
        public CrearProductosResponse()
        {

        }

        public CrearProductosResponse(string mensaje)
        {
            Mensaje = mensaje;
        }

        public string Mensaje { get; set; }
        public bool isOk()
        {
            return this.Mensaje.Equals("Se creó con éxito el producto.");
        }
    }
}
