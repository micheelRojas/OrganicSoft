using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Productos
{
    public class CrearProductosCommand
    {
        public CrearProductosCommand()
        {
        }

        public CrearProductosCommand(int id, int codigoProducto, string nombre, string descripcion, double precio, string categoria, string presentacion, int minimoStock, double costo)
        {
            Id = id;
            CodigoProducto = codigoProducto;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Categoria = categoria;
            Presentacion = presentacion;
            MinimoStock = minimoStock; 
            Costo = costo;
        }
 
        public int Id { get; set; }
        public string TipoProducto { get; set; }
        public int CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public double Precio { get; set; }
        public string Categoria { get; set; }
        public string Presentacion { get; set; }
        public int MinimoStock { get; set; }
        public double Costo { get; set; }

        public virtual IReadOnlyList<string> CanCrear()
        {
            var errors = new List<string>();

            if ((CodigoProducto == 0))
                errors.Add("Codigo del producto no especificado");

            if (string.IsNullOrEmpty(Nombre))
                errors.Add("Nombre del producto no especificado");
            if (string.IsNullOrEmpty(Descripcion))
                errors.Add("Descripcion del producto no especificado");
            if (string.IsNullOrEmpty(Categoria))
                errors.Add("Categoria del producto no especificado");
            if (string.IsNullOrEmpty(Presentacion))
                errors.Add("Presenctacion del producto no especificado");
            if (Precio == 0)
                errors.Add("Precio del producto no especificado");
            if (Costo == 0)
                errors.Add("El costo del producto no especificado");
            return errors;
        }
    }
}
