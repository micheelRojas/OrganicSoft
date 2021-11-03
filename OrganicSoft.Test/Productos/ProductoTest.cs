using NUnit.Framework;
using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.Productos
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
            var producto = new ProductoSimple(codigo:1, nombre: "Jabon de sandia", 
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
            var producto = new ProductoSimple(codigo: 1, nombre: "Jabon de sandia",
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
            var producto = new ProductoSimple(codigo: 1, nombre: "Jabon de sandia",
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
            var producto = new ProductoSimple(codigo: 1, nombre: "Jabon de sandia",
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
            var producto = new ProductoSimple(codigo: 1, nombre: "Jabon de sandia",
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
            var producto = new ProductoSimple(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            producto.EntradaProductos(cantidad: 10);
            #endregion
            #region CUANDO se desee registrar el un descuente de este del 20% durante tres dias
            var decuento = new Descuento(codigoDescuento:1, fechaInicio: new DateTime(2021, 09, 28), fechaFin: new DateTime(2022, 10, 20),porcentajeDescuento:0.2);
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
            var producto = new ProductoSimple(codigo: 1, nombre: "Jabon de sandia",
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
            var producto = new ProductoSimple(codigo: 1, nombre: "Jabon de sandia",
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

    
}
