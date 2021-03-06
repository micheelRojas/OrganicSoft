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
        static Inventario inventario = Inventario.getInventario();
        public double Utilidad { get; private set; }
        public List<Componente> Componentes { get; private set; }
        public ProductoCombo(int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock, List<Componente> componentes) : base(codigo, nombre, decripcion, precio, categoria, presentacion, minimoStock)
        {
            Componentes = new List<Componente>();
            LlenarListaComponente(componentes);
        }
      
        ProductoCombo() { 
        }

        private void LlenarListaComponente(List<Componente> componentes) {
            for (int i = 0; i < componentes.LongCount(); i++)
            {
                AddComponente(componentes[i].Producto, componentes[i].Cantidad);
            }
        }
        private static double CalcularCostos(List<Componente> componentes)
        {
            double sumaCostos = 0;
            for (int i = 0; i < inventario.productos.ToList().LongCount(); i++)
            {
                for (int j = 0; j < componentes.LongCount(); j++)
                {
                    if (inventario.productos.ToList()[i].Nombre.Equals(componentes[j].Producto.Nombre))
                    {
                        sumaCostos = sumaCostos + (inventario.productos.ToList()[i].Costo * componentes[j].Cantidad);
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
                    SalidadeProductosdelProductoCompuesto(Componentes, cantidad);
                   var v=  AsignarCosto( CalcularCostos(Componentes));
                    Utilidad= cantidad * ( PrecioConDescuento- Costo);
                    return $"La utilidad de {Nombre} es de: {Utilidad}";
                }
                if (!validacion)
                {
                    return "No existe la cantidad de productos suficientes para la venta";
                }

            }
            throw new NotImplementedException();
        }
        private void SalidadeProductosdelProductoCompuesto(List<Componente> componentes, int cantidad)
        {
            for (int i = 0; i < inventario.productos.Count; i++)
            {
                for (int j = 0; j < componentes.Count(); j++)
                {

                    if (inventario.productos.ToList()[i].Nombre.Equals(componentes[j].Producto.Nombre))
                    {
                        inventario.productos.ToList()[i].DisminuirCantidadProducto(componentes[j].Cantidad * cantidad);

                    }

                }
            }

        }
        private bool ValidarExistencia(ProductoCombo producto, int cantidad)
        {
            int validador = 0;
            for (int i = 0; i < inventario.productos.Count(); i++)
            {
                for (int j = 0; j < producto.Componentes.Count(); j++)
                {
                    if (inventario.productos.ToList()[i].Nombre.Equals(producto.Componentes[j].Producto.Nombre))
                    {
                        if (inventario.productos.ToList()[i].CantidadExistente >= producto.Componentes[j].Cantidad * cantidad)
                        {
                            validador++;
                        }
                    }

                }
            }
            if (validador == producto.Componentes.Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddComponente(Producto producto, int cantidad) 
        {
            Componentes.Add(new Componente(producto, cantidad));
        }
    }
    public class Componente : Entity<int>
    {

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
