using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrganicSoft.Aplicacion;
using OrganicSoft.Infraestructura;
using OrganicSoft.Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrganicSoft.Aplicacion.CrearProductoCommandHandle;

namespace OrganicSoft.Test.PruebasdeAplicacion
{
    class ProductoTestAplicaion
    {
        private CrearProductoCommandHandle _productoCrearService;
        private OrganicSoftContext _context;
       
        [SetUp]
        public void Setup()
        {
            var optionsInMemory = new DbContextOptionsBuilder<OrganicSoftContext>().UseInMemoryDatabase("organicSoft").Options;
            _context = new OrganicSoftContext(optionsInMemory);

            _productoCrearService = new CrearProductoCommandHandle(
                new UnitOfWork(_context),
                new ProductoRepository(_context));
        }
        [Test]
        public void PuedoCrearProductosAplicacion()
        {
            //Arrange , Act
            var response = _productoCrearService.Handle(new CrearProductosCommand(tipoProducto:"Simple",id:10293,codigoProducto: 10976, nombre: "Jabon de Maracuya",
            descripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3));
            // Assert
            Assert.AreEqual("Se creó con exito el producto.", response.Mensaje);
        }
    }
}
