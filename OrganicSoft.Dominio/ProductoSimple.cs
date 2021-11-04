using System;

namespace OrganicSoft.Dominio
{
    public class ProductoSimple : Producto
    {
        public ProductoSimple(int codigo, string nombre, string decripcion, double costo, double precio, string categoria, string presentacion, int minimoStock) : base(codigo, nombre, decripcion,  precio, categoria,  presentacion,  minimoStock) {
            AsignarCosto(costo);
        }
       
        ProductoSimple() { }
        public override string SalidaProductos(int cantidad)
        {

            if (cantidad > 0 && CantidadExistente >= cantidad)
            {
                int temporarl = CantidadExistente - cantidad;
               
                if (temporarl >= MinimoStock)
                {
                    DisminuirCantidadProducto(cantidad);
                    return $"La cantidad de {Nombre} es: {CantidadExistente}";
                }

            }
            if (cantidad <= 0)
            {
                return $"La cantidad pedida debe ser mayor a cero";
            }
            return $"No hay suficietes productos de {Nombre} para realizar la operacion"; 
        }
    }
}
