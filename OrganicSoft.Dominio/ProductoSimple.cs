using System;

namespace OrganicSoft.Dominio
{
    public class ProductoSimple : Producto
    {
        public ProductoSimple(int codigo, string nombre, string decripcion, double costo, double precio, string categoria, string presentacion, int minimoStock) : base(codigo, nombre, decripcion,  precio, categoria,  presentacion,  minimoStock) {
            AsignarCosto(costo);
        }
        public ProductoSimple() { }
        public override string SalidaProductos(int cantidad)
        {

            if (cantidad > 0 && CantidadExistente >= cantidad)
            {
                DisminuirCantidadProducto(cantidad);
                if (CantidadExistente >= MinimoStock)
                {

                    return $"La cantidad de {Nombre} es: {CantidadExistente}";
                }
                else if (CantidadExistente < MinimoStock)
                {
                    return $"La cantidad de {Nombre} es: {CantidadExistente}, considere unidades de este producto";
                }

            }
            if (cantidad <= 0)
            {
                return $"La cantidad pedida debe ser mayor a cero";
            }

            throw new NotImplementedException();
        }
    }
}
