using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.Procutos
{
    class ProductoTest
    {/*
      * Como administrador 
      * quiero realizar la gestión de productos de inventario 
      * para poder realizar la venta y saber en que momento se deben producir más
      */
        [Test]
        public void PuedoRegistrarEntradadeProductos()
        {

            #region Dado que laly Organis tiene multiples productos,como jabon de sandia
            var producto = new Producto(codigo:1, nombre: "Jabon de sandia", 
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria:"Jabon", presentacion:"pequeño, 80 gr",minimoStock:3);
            #endregion
            #region CUANDO se desee registrar 10 jabones de sandia
            int cantidad = 10;
            string respuesta = producto.EntradaProductos(cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema registrara el producto y adicionara la cantidad del mismo y mostrara el mensaje "La cantidad de Jabon de sandia es 10"
            Assert.AreEqual("La cantidad de Jabon de sandia es: 10", respuesta);
            #endregion

        }
        [Test]
        public void PuedoRegistrarSalidadeProductos()
        {

            #region Dado que laly Organis tiene multiples productos,como jabon de sandia y se tienen 10 de este
            var producto = new Producto(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            producto.EntradaProductos(cantidad: 10);
            #endregion
            #region CUANDO se desee registrar la salidad de un 3 jabones de sandia
            int cantidad = 3;
            string respuesta = producto.SalidaProductos(cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema registrara el producto y adicionara la cantidad del mismo y mostrara el mensaje "La cantidad de Jabon de sandia es 9"
            Assert.AreEqual("La cantidad de Jabon de sandia es: 7", respuesta);
            #endregion

        }
        [Test]
        public void PuedoRegistrarSalidadeProductosStockMinimo()
        {

            #region Dado que laly Organis tiene multiples productos,como jabon de sandia y se tienen 10 de este
            var producto = new Producto(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            producto.EntradaProductos(cantidad: 10);
            #endregion
            #region CUANDO se desee registrar la salidad de un 3 jabones de sandia
            int cantidad = 8;
            string respuesta = producto.SalidaProductos(cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema registrara el producto y adicionara la cantidad del mismo y mostrara el mensaje "La cantidad de Jabon de sandia es 9"
            Assert.AreEqual("La cantidad de Jabon de sandia es: 7", respuesta);
            #endregion

        }
    }

    internal class Producto
    {
        public int Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string Decripcion { get; private set; }
        public double Costo { get; private set; }
        public double Precio { get; private set; }
        public string Categoria { get; private set; }
        public string Presentacion { get; private set; }
        public int MinimoStock { get; private set; }
        public int CantidadExitente { get; private set; }
        private List<Producto> _productos = new List<Producto>();
        public Producto(int codigo, string nombre, string decripcion, double costo, double precio, string categoria, string presentacion, int minimoStock)
        {
            Codigo = codigo;
            Nombre = nombre;
            Decripcion = decripcion;
            Costo = costo;
            Precio = precio;
            Categoria = categoria;
            Presentacion = presentacion;
            MinimoStock = minimoStock;
        }
        public IReadOnlyCollection<Producto> Productos => _productos.AsReadOnly();
        internal string EntradaProductos(int cantidad)
        {
            if (cantidad>0)
            {
                CantidadExitente += cantidad;
                _productos.Add(this);
                return $"La cantidad de {Nombre} es: {CantidadExitente}";
            }
            throw new NotImplementedException();

        }

        internal  void DisminuirCantidad(int cantidad) {
            CantidadExitente -= cantidad;
        }
        internal string SalidaProductos(int cantidad)
        {
            if (cantidad > 0 && CantidadExitente >= cantidad)
            {
               
                for (int i = 0; i < _productos.Count; i++)
                {
                    if (_productos[i].Nombre.Equals(Nombre))
                    {
                        _productos[i].DisminuirCantidad(cantidad);
                    }
                } 
                return $"La cantidad de {Nombre} es: {CantidadExitente}";
            }
            throw new NotImplementedException();
        }
    }
}
