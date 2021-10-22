using OrganicSoft.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public abstract class Producto : Entity<int>, IAggregateRoot
    {
        public int CodigoProducto { get; private set; }
        public string Nombre { get; private set; }
        public string Decripcion { get; private set; }
        public double Costo { get; private set; }
        public double Precio { get; private set; }
        public string Categoria { get; private set; }
        public string Presentacion { get; private set; }
        public int MinimoStock { get; private set; }
        public int CantidadExistente { get; private set; }
        public int CantidadVendidad { get; private set; }
        public Descuento Descuento { get; private set; }
        public double PrecioConDescuento { get; private set; }
        private static List<Producto> _productos = new List<Producto>();
        public Producto(int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock)
        {
            CodigoProducto = codigo;
            Nombre = nombre;
            Decripcion = decripcion;
            Precio = precio;
            PrecioConDescuento = precio;
            Categoria = categoria;
            Presentacion = presentacion;
            MinimoStock = minimoStock;
        }
        public static IReadOnlyCollection<Producto> Productos => _productos.AsReadOnly();
        public virtual string EntradaProductos(int cantidad)
        {

            if (cantidad > 0)
            {
                CantidadExistente += cantidad;
                _productos.Add(this);
                return $"La cantidad de {Nombre} es: {CantidadExistente}";
            }
            else
            {
                return $"La cantidad debe ser mayor a cero";
            }

        }

        public abstract string SalidaProductos(int cantidad);
       

        public string AplicarDescuento(Descuento descuento)
        {
            Descuento = descuento;
            if (descuento.FechaInicio <= DateTime.Now && descuento.FechaFin >= DateTime.Now)
            {
                PrecioConDescuento = Precio - (Precio * descuento.PorcentajeDescuento);

            }
            return $"Precio de {Nombre}, es de: {PrecioConDescuento}";


        }

        public string RetirarDescuento()
        {
            if (Descuento != null)
            {
                PrecioConDescuento = Precio + (Precio * Descuento.PorcentajeDescuento);
                return $"El nuevo precio de {Nombre}, es de: {Precio}";
            }
            throw new NotImplementedException();
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
        public virtual double AsignarCosto(double costo) {
            return Costo = costo;
        }

        public virtual void DisminuirCantidadProducto(int cantidad)
        {
            if (cantidad > 0)
            {

                if (CantidadExistente >= cantidad)
                {
                    CantidadExistente -= cantidad;
                    CantidadVendidad += cantidad;
                }

            }


        }
    }
}
