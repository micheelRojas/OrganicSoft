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
        public void PuedoRegistrarEntradadeProductosIncorrecta()
        {

            #region Dado que laly Organis tiene multiples productos,como jabon de sandia
            var producto = new Producto(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            #endregion
            #region CUANDO se desee registrar 10 jabones de sandia
            int cantidad = -1;
            string respuesta = producto.EntradaProductos(cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema  mostrara el mensaje "La cantidad debe ser mayor a cero"
            Assert.AreEqual("La cantidad debe ser mayor a cero", respuesta);
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
            #region ENTONCES  el sistema disminuira la cantidad del mismo y mostrara el mensaje "La cantidad de Jabon de sandia es 9"
            Assert.AreEqual("La cantidad de Jabon de sandia es: 7", respuesta);
            #endregion

        }
        [Test]
        public void PuedoRegistrarSalidadeProductosIncorrecta()
        {

            #region Dado que laly Organis tiene multiples productos,como jabon de sandia y se tienen 10 de este
            var producto = new Producto(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            producto.EntradaProductos(cantidad: 10);
            #endregion
            #region CUANDO se desee registrar la salidad de un -1 jabones de sandia
            int cantidad = -1;
            string respuesta = producto.SalidaProductos(cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema mostrara el mensaje "La cantidad pedida debe ser mayor a cero"
            Assert.AreEqual("La cantidad pedida debe ser mayor a cero", respuesta);
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
            #region ENTONCES  el sistema disminuira la cantidad del mismo y mostrara el mensaje "La cantidad de Jabon de sandia es 2, considere unidades de este producto"
            Assert.AreEqual("La cantidad de Jabon de sandia es: 2, considere unidades de este producto", respuesta);
            #endregion

        }

        /*
         * Como administrador 
         * quiero realizar descuentos en uno o varios productos en un  lapso determinado de tiempo
         * para llamar la atención de los clientes.
         */
        [Test]
        public void PuedoRegistrarDescuentos()
        {

            #region Dado que laly Organis tiene multiples productos,como jabon de sandia y se tienen 10 de este
            var producto = new Producto(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            producto.EntradaProductos(cantidad: 10);
            #endregion
            #region CUANDO se desee registrar el un descuente de este del 20% durante tres dias
            var decuento = new Descuento(codigoDescuento:1, fechaInicio: new DateTime(2021, 09, 28), fechaFin: new DateTime(2021, 10, 1),porcentajeDescuento:0.2);
            var respuesta = producto.AplicarDescuento(descuento:decuento);
            #endregion
            #region ENTONCES  el sistema Cambiara el precio por el tiempo correspondiente y mostrarara el mensaje "El nuevo precio de Jabon de sandia, es de: $ 8.000,00"
            Assert.AreEqual("Precio de Jabon de sandia, es de: 8000", respuesta);
            #endregion

        }
        [Test]
        public void PuedoRegistrarDescuentosIncorrecta()
        {

            #region Dado que laly Organis tiene multiples productos,como jabon de sandia y se tienen 10 de este
            var producto = new Producto(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            producto.EntradaProductos(cantidad: 10);
            #endregion
            #region CUANDO se desee registrar el un descuente de este del 20% durante tres dias pero las fechas son inferiores a la actula
            var decuento = new Descuento(codigoDescuento: 1, fechaInicio: new DateTime(2021, 09, 26), fechaFin: new DateTime(2021, 09, 28), porcentajeDescuento: 0.2);
            var respuesta = producto.AplicarDescuento(descuento: decuento);
            #endregion
            #region ENTONCES  el sistema no Cambiara el precio por el tiempo correspondiente y mostrarara el mensaje "El nuevo precio de Jabon de sandia, es de: $ 8.000,00"
            Assert.AreEqual("Precio de Jabon de sandia, es de: 10000", respuesta);
            #endregion

        }
        [Test]
        public void PuedoQuitarDescuentos()
        {

            #region Dado que laly Organis tiene multiples productos,como jabon de sandia y se tienen 10 de este con un descuennto por tres dias
            var producto = new Producto(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            producto.EntradaProductos(cantidad: 10);
            var decuento = new Descuento(codigoDescuento: 1, fechaInicio: new DateTime(2021, 09, 28), fechaFin: new DateTime(2021, 10, 1), porcentajeDescuento: 0.2);
            producto.AplicarDescuento(descuento: decuento);
            #endregion
            #region CUANDO hallan pasado los tres dias y se aplique el descuento el producto devera volver al precio original
           
            var respuesta = producto.RetirarDescuento();
            #endregion
            #region ENTONCES  el sistema Cambiara el precio por el tiempo correspondiente y mostrarara el mensaje "El nuevo precio de Jabon de sandia, es de: $ 8.000,00"
            Assert.AreEqual("El nuevo precio de Jabon de sandia, es de: 10000", respuesta);
            #endregion

        }
    }

    internal class Descuento
    {
        public int CodigoDescuento { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public double PorcentajeDescuento { get; private set; }

        public Descuento(int codigoDescuento, DateTime fechaInicio, DateTime fechaFin, double porcentajeDescuento)
        {
            CodigoDescuento = codigoDescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            PorcentajeDescuento = porcentajeDescuento;
        }
    }

    internal class Producto
    {
        public int CodigoProducto { get; private set; }
        public string Nombre { get; private set; }
        public string Decripcion { get; private set; }
        public double Costo { get; private set; }
        public double Precio { get; private set; }
        public string Categoria { get; private set; }
        public string Presentacion { get; private set; }
        public int MinimoStock { get; private set; }
        public int CantidadExitente { get; private set; }
        public Descuento Descuento { get; private set; }
        public double PrecioConDescuento { get; private set; }
        private List<Producto> _productos = new List<Producto>();
        public Producto(int codigo, string nombre, string decripcion, double costo, double precio, string categoria, string presentacion, int minimoStock)
        {
            CodigoProducto = codigo;
            Nombre = nombre;
            Decripcion = decripcion;
            Costo = costo;
            Precio = precio;
            PrecioConDescuento = precio;
            Categoria = categoria;
            Presentacion = presentacion;
            MinimoStock = minimoStock;
        }
        public IReadOnlyCollection<Producto> Productos => _productos.AsReadOnly();
        internal string EntradaProductos(int cantidad)
        {
            if (cantidad > 0)
            {
                CantidadExitente += cantidad;
                _productos.Add(this);
                return $"La cantidad de {Nombre} es: {CantidadExitente}";
            }
            else {
                return $"La cantidad debe ser mayor a cero";
            }

        }

        internal string SalidaProductos(int cantidad)
        {
            
            if (cantidad > 0 && CantidadExitente >= cantidad)
            {
                CantidadExitente -= cantidad;
                if (CantidadExitente >= MinimoStock) {
                  
                    return $"La cantidad de {Nombre} es: {CantidadExitente}";
                }
                else if (CantidadExitente<MinimoStock) {
                    return $"La cantidad de {Nombre} es: {CantidadExitente}, considere unidades de este producto";
                }
                
            }
            if (cantidad <=0)
            {
            return $"La cantidad pedida debe ser mayor a cero";
            }
            
            throw new NotImplementedException();
        }

        internal string AplicarDescuento(Descuento descuento)
        {
            Descuento = descuento;
            if (descuento.FechaInicio <= DateTime.Now && descuento.FechaFin >= DateTime.Now)
            {
                PrecioConDescuento = Precio - (Precio * descuento.PorcentajeDescuento);
                
            }
            return $"Precio de {Nombre}, es de: {PrecioConDescuento}";
            
            
        }

        internal string RetirarDescuento()
        {
            if (Descuento != null) {
                PrecioConDescuento = Precio + (Precio * Descuento.PorcentajeDescuento);
                return $"El nuevo precio de {Nombre}, es de: {Precio}";
            }
            throw new NotImplementedException();
        }
    }
}
