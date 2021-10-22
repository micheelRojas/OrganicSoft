using OrganicSoft.Dominio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
   public class ProductoCombo : Producto
    {
       
        public double Utilidad { get; private set; }
        public List<Componente> Componetes { get; private set; }
        public ProductoCombo(int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock, List<Componente> componentes) : base(codigo, nombre, decripcion, precio, categoria, presentacion, minimoStock)
        {
           
            Componetes = componentes;
        }
        ProductoCombo() { 
        }
        private static double calcularCostos(List<Componente> componentes)
        {
            double sumaCostos = 0;
            for (int i = 0; i < Productos.Count; i++)
            {
                for (int j = 0; j < componentes.LongCount(); j++)
                {
                    if (Productos.ToList()[i].Nombre.Equals(componentes[j].Producto.Nombre))
                    {
                        sumaCostos = sumaCostos + (Productos.ToList()[i].Costo * componentes[i].Cantidad);
                    }
                }

            }
            return sumaCostos;
        }
        public override string SalidaProductos(int cantidad)
        {
            if (cantidad >= 0)
            {
                var validacion = ValidarExistencia(this, cantidad);
                if (validacion)
                {
                    AumentarCantidadProducto(cantidad);
                    SalidadeProductosdelProductoCompuesto(Componetes, cantidad);
                   var v=  AsignarCosto( calcularCostos(Componetes));
                    Utilidad= cantidad * ( PrecioConDescuento- Costo);
                    return $"La utilidad de {Nombre} es de: {Utilidad}";
                }
                if (!validacion)
                {
                    return "No existe la Cantidad de productos suficientes para la venta";
                }

            }
            throw new NotImplementedException();
        }
       private void SalidadeProductosdelProductoCompuesto(List<Componente> componentes, int cantidad)
        {
            for (int i = 0; i < Productos.Count; i++)
            {
                for (int j = 0; j < componentes.Count(); j++)
                {

                    if (Productos.ToList()[i].Nombre.Equals(componentes[j].Producto.Nombre))
                    {
                        Productos.ToList()[i].DisminuirCantidadProducto(componentes[j].Cantidad * cantidad);

                    }

                }
            }

        }
        private bool ValidarExistencia(ProductoCombo producto, int cantidad)
        {
            int validador = 0;
            for (int i = 0; i < Productos.Count(); i++)
            {
                for (int j = 0; j < producto.Componetes.Count(); j++)
                {
                    if (Productos.ToList()[i].Nombre.Equals(producto.Componetes[j].Producto.Nombre))
                    {
                        if (Productos.ToList()[i].CantidadExistente >= producto.Componetes[j].Cantidad * cantidad)
                        {
                            validador++;
                        }
                    }

                }
            }
            if (validador == producto.Componetes.Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Componente : Entity<int>, IAggregateRoot
    {
        [Key]
        public int ID { get; set; }
        public Producto Producto { get; private set; }
        public int Cantidad { get; private set; }
        public Componente() { }
        public Componente(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }
    }
}
