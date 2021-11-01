using NUnit.Framework;
using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Test.Productos
{
    class ProductoComboTest
    {
        [Test]
        public void PuedoRegistrarProductosdeSalidaddeCombos()
        {
            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, avena y frutos rojos que se venden en combo de 3 unidades cada uno

            var jabonSandia = new ProductoSimple(codigo: 5, nombre: "Jabon de sandia Pequeño",
            decripcion: " Ea hidrante facial y corporal 🍉.", 
            costo: 1000.00, precio: 40000.00, categoria: "Jabon", presentacion: "mini, 30 gr", minimoStock: 3);

            var jabonAvena = new ProductoSimple(codigo: 6, nombre: "Jabon de avena Pequeño",
            decripcion: " Ea hidrante facial y corporal Avena.",
            costo: 1000.00, precio: 40000.00, categoria: "Jabon", presentacion: "mini, 30 gr", minimoStock: 3);

            var javonFrutosRojos = new ProductoSimple(codigo: 7, nombre: "Jabon de frutos rojos Pequeño",
            decripcion: " Ea hidrante facial y corporal Frutos.",
            costo: 1000.00, precio: 40000.00, categoria: "Jabon", presentacion: "mini, 30 gr", minimoStock: 3);

            int cantidadEntrada = 6;
            jabonSandia.EntradaProductos(cantidad: cantidadEntrada);
            jabonAvena.EntradaProductos(cantidad: cantidadEntrada);
            javonFrutosRojos.EntradaProductos(cantidad: cantidadEntrada);
            List<Componente> componentesCombo = new List<Componente>();
            componentesCombo.Add(new Componente(jabonSandia, 3));
            componentesCombo.Add(new Componente(jabonAvena, 3));
            componentesCombo.Add(new Componente(javonFrutosRojos, 3));
            var combo = new ProductoCombo(codigo: 1, nombre: "combo",
            decripcion: "Combo de 3 jabones minis de sandia, avena y frutos rojos.",
            precio: 20000.00, categoria: "Jabon", presentacion: "combo de jabones minis", minimoStock: 3,componentes:componentesCombo);
          
            #endregion
            #region CUANDO se solicited la venta de un combo

            int cantidadSalida = 1;
            string respuesta = combo.SalidaProductos(cantidad: cantidadSalida);
            #endregion
            #region ENTONCES la cantidad de la salida se le disminuirá a la cantidad existente de cada uno de sus componentes y se mostrara el mensaje utilidad  La utilidad del combo  es de: $ 11.000,00
            Assert.AreEqual($"La utilidad de combo es de: 11000", respuesta);
            Assert.AreEqual(3, jabonSandia.CantidadExistente);
            #endregion

        }

        
        [Test]
        public void PuedoRegistrarProductosdeSalidaddeComboIncorrecta()
        {
            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, avena y frutos rojos que se venden en combo de 3 unidades cada uno

            var jabonSandia = new ProductoSimple(codigo: 1, nombre: "Jabon de sandia",
            decripcion: " Ea hidrante facial y corporal 🍉.",
            costo: 1000.00, precio: 40000.00, categoria: "Jabon", presentacion: "mini, 30 gr", minimoStock: 3);

            var jabonAvena = new ProductoSimple(codigo: 2, nombre: "Jabon de avena",
            decripcion: " Ea hidrante facial y corporal Avena.",
            costo: 1000.00, precio: 40000.00, categoria: "Jabon", presentacion: "mini, 30 gr", minimoStock: 3);

            var javonFrutosRojos = new ProductoSimple(codigo: 3, nombre: "Jabon de frutos rojos",
            decripcion: " Ea hidrante facial y corporal Frutos.",
            costo: 1000.00, precio: 40000.00, categoria: "Jabon", presentacion: "mini, 30 gr", minimoStock: 3);

            int cantidadEntrada = 6;
            jabonSandia.EntradaProductos(cantidad: cantidadEntrada);
            jabonAvena.EntradaProductos(cantidad: cantidadEntrada);
            javonFrutosRojos.EntradaProductos(cantidad: cantidadEntrada);
            List<Componente> componentesCombo = new List<Componente>();
            componentesCombo.Add(new Componente(jabonSandia, 3));
            componentesCombo.Add(new Componente(jabonAvena, 3));
            componentesCombo.Add(new Componente(javonFrutosRojos, 3));
            var combo = new ProductoCombo(codigo: 1, nombre: "combo",
            decripcion: "Combo de 3 jabones minis de sandia, avena y frutos rojos.",
            precio: 20000.00, categoria: "Jabon", presentacion: "combo de jabones minis", minimoStock: 3, componentes: componentesCombo);
           
            #endregion
            #region CUANDO se solicited la venta de 3 combos

            int cantidadSalida = 3;
            string respuesta = combo.SalidaProductos(cantidad: cantidadSalida);
            #endregion
            #region ENTONCES  se mostrara el mensaje   "No existe la Cantidad de productos suficientes para la venta"
            Assert.AreEqual($"No existe la Cantidad de productos suficientes para la venta", respuesta);
            #endregion

        }

    }
}
