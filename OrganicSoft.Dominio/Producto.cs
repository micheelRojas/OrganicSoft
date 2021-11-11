using OrganicSoft.Dominio.Base;
using System;

namespace OrganicSoft.Dominio
{
    public abstract class Producto : Entity<int>, IAggregateRoot
    {
        public int CodigoProducto { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public double Costo { get; private set; }
        public double Precio { get; private set; }
        public string Categoria { get; private set; }
        public string Presentacion { get; private set; }
        public int MinimoStock { get; private set; }
        public int CantidadExistente { get; private set; }
        public int CantidadVendida { get; private set; }
        public Descuento Descuento { get; private set; }
        public DateTime FechadelDescuento{ get; private set; }
        public double PrecioConDescuento => ObtenerPrecioConDescuento(FechadelDescuento);

        Inventario inventario = Inventario.getInventario();
        public Producto() { }
        public Producto(int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock)
        {
            CodigoProducto = codigo;
            Nombre = nombre;
            Descripcion = decripcion;
            Precio = precio;
            Categoria = categoria;
            Presentacion = presentacion;
            MinimoStock = minimoStock;
            CantidadExistente = 0;
        }

        public virtual string EntradaProductos(int cantidad)
        {

            if (cantidad > 0)
            {
                CantidadExistente += cantidad;
                inventario.productos.Add(this);
                return $"La cantidad de {Nombre} es: {CantidadExistente}";
            }
            else
            {
                return $"La cantidad debe ser mayor a cero";
            }

        }

        public abstract string SalidaProductos(int cantidad);

        public string AplicarDescuento(Descuento descuento, DateTime fechaDescuento)
        {
            Descuento = descuento;
            FechadelDescuento = fechaDescuento;
            return $"Precio de {Nombre}, es de: {PrecioConDescuento}";
        }
        private double ObtenerPrecioConDescuento(DateTime fechaDescuento)
        {
            if (Descuento != null)
            {
                return Precio - (Precio * Descuento.ObtenerDescuentoVigente(fechaDescuento));
            }
            else 
            {
                return Precio;
            }
        }

      

        public void DisminuirCantidadProductoStock(int cantidad)
        {
            if (cantidad > 0 && MinimoStock <= (CantidadExistente - cantidad))
            {
                CantidadExistente -= cantidad;
            }

        }
        public virtual void AumentarCantidadProducto(int cantidad)
        {
            CantidadExistente += cantidad;
        }
        public virtual double AsignarCosto(double costo)
        {
            return Costo = costo;
        }

        public virtual void DisminuirCantidadProducto(int cantidad)
        {
            if (cantidad > 0)
            {

                if (CantidadExistente >= cantidad)
                {
                    CantidadExistente -= cantidad;
                    CantidadVendida += cantidad;
                }

            }


        }
    }
}
