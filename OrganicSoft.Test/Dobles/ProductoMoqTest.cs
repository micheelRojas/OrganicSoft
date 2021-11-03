using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using OrganicSoft.Aplicacion;
using OrganicSoft.Aplicacion.Dobles;
using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura;
using OrganicSoft.Infraestructura.Base;
using static OrganicSoft.Aplicacion.EntradadeProductosCommandHandle;

namespace OrganicSoft.Test.Dobles
{
    class ProductoMoqTest
    {
        private OrganicSoftContext _dbContext;
        //private EntradadeProductosCommandHandle _entradaService;//SUT - Objeto bajo prueba

        [SetUp]
        public void Setup()
        {
            //Arrange

            var optionsSqlite = new DbContextOptionsBuilder<OrganicSoftContext>()
           .UseSqlite(SqlLiteDatabaseInMemory.CreateConnection())
           .Options;



            _dbContext = new OrganicSoftContext(optionsSqlite);
            _dbContext.Database.EnsureCreated();


        }

        //[Test]
        //public void NoPuedeConsignarTest()
        //{

        //    //Arrange
        //    var productoSimple = new ProductoSimple(codigo: 1001, nombre: "Crema Facial",
        //    decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
        //    " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
        //    " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
        //    "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);


        //    _dbContext.Producto.Add(productoSimple);
        //    _dbContext.SaveChanges();

        //    var mockEmailServer = new Mock<Producto>();
        //    //mockEmailServer.Setup(emailServer =>
        //    //   emailServer.EntradaProductos(It.IsAny<int>())
        //    //    ).Returns("La cantidad de Crema Facial es: 2");

        //    int calls = 0;
        //    mockEmailServer.Setup(m => m.EntradaProductos(It.IsAny<int>()))
        //    .Returns("La cantidad de Crema Facial es: 2")
        //    .Callback(() => {  calls++; });

            

        //    _entradaService = new EntradadeProductosCommandHandle(
        //     new UnitOfWork(_dbContext),
        //     new ProductoRepository(_dbContext)
        //     );


        //    //Act
        //    var response = _entradaService.Handle(new EntradadeProductosCommand(1, 2));
        //    //Assert
        //    //mockEmailServer.Verify(x => x.EntradaProductos(2), Times.Once);
        //    //Assert.AreEqual("La cantidad de Crema Facial es: 2", response.Mensaje);
        //    Assert.AreEqual(1, calls);
        //    //
        //    //Revertir
        //    _dbContext.Producto.Remove(productoSimple);
        //    _dbContext.SaveChanges();

        //}

    }
}
