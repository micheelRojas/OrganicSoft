using FluentAssertions;
using Newtonsoft.Json;
using OrganicSoft.Aplicacion.Pedidos;
using OrganicSoft.Aplicacion.Productos;
using OrganicSoft.Dominio;
using OrganicSoft.WebApi.Angular.Test.Base;
using OrganicSoft.WepApi.Angular;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static OrganicSoft.Aplicacion.CarritoDeCompra.AgregarAlCarritoCommandHandle;
using static OrganicSoft.Aplicacion.CarritoDeCompra.CrearCarritoCompraCommandHandle;
using static OrganicSoft.Aplicacion.CrearProductoSimpleCommandHandle;
using static OrganicSoft.Aplicacion.EntradadeProductosCommandHandle;
using static OrganicSoft.Aplicacion.SalidaProductoCommandHandle;

namespace OrganicSoft.WebApi.Angular.Test
{
    public class ProductoTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public ProductoTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
       [Fact]
        public async Task PuedeCrearProductoSimpleCorrecto()
        {
            var request = new CrearProductosCommand()
            {   Id = 0,
                CodigoProducto = 2125,
                Nombre = "Jabón de cuerpo",
                Descripcion = "Jabón para el cuerpo",
                Precio = 10000,
                Categoria = "Jabones",
                Presentacion = "Pequeño",
                MinimoStock = 2,
                Costo = 12000
        };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PostAsync("api/Producto", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta2 = await responseHttp.Content.ReadAsStringAsync();
            var respuesta = respuesta2.Substring(12, 30);
            respuesta.Should().Be("Se creó con éxito el producto.");
            var context = _factory.CreateContext();
            var producto3421 = context.Producto.FirstOrDefault(t => t.CodigoProducto == 2125);
            producto3421.Should().NotBeNull();
        }

        [Fact]
        public async Task PuedoHaceEntradaDeProductoSimpleCorrecto()
        {
            
            var request = new EntradadeProductosCommand()
            {
                Id = 2123,
                Cantidad = 40
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync("api/Producto", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta2 = await responseHttp.Content.ReadAsStringAsync();
            var respuesta = respuesta2.Substring(12, 37);
            respuesta.Should().Be("La cantidad de Jabón de cuerpo es: 40");
            var context = _factory.CreateContext();
            var producto3421 = context.Producto.FirstOrDefault(t => t.CodigoProducto == 2123);
            producto3421.Should().NotBeNull();
        }

        [Fact]
        public async Task NoPuedoHaceEntradaDeProductoSimpleCorrecto()
        {
            var request = new EntradadeProductosCommand()
            {
                Id = 4231,
                Cantidad = 40
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync($"api/Producto", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta2 = await responseHttp.Content.ReadAsStringAsync();
            var respuesta = respuesta2.Substring(12, 21);
            respuesta.Should().Be("el producto no existe");
            var context = _factory.CreateContext();
            var producto3421 = context.Producto.FirstOrDefault(t => t.CodigoProducto == 4231);
            producto3421.Should().BeNull();
        }

        [Fact]
        public async Task PuedoHaceSalidaDeProductoSimpleCorrecto()
        {
            var request = new SalidaProductosCommand()
            {
                Id = 2123,
                Cantidad = 20
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync($"api/Producto/{request.Id}", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta2 = await responseHttp.Content.ReadAsStringAsync();
            var respuesta = respuesta2.Substring(12, 37);
            respuesta.Should().Be("La cantidad de Jabón de cuerpo es: 20");
            var context = _factory.CreateContext();
            var producto3421 = context.Producto.FirstOrDefault(t => t.CodigoProducto == 2123);
            producto3421.Should().NotBeNull();
        }

        [Fact]
        public async Task NoPuedoHaceSalidaDeProductoSimpleCorrecto()
        {
            var request = new SalidaProductosCommand()
            {
                Id = 3241,
                Cantidad = 20
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync($"api/Producto/{request.Id}", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta2 = await responseHttp.Content.ReadAsStringAsync();
            var respuesta = respuesta2.Substring(12, 21);
            respuesta.Should().Be("el producto no existe");
            var context = _factory.CreateContext();
            var producto3421 = context.Producto.FirstOrDefault(t => t.CodigoProducto == 3241);
            producto3421.Should().BeNull();
        }

        [Fact]
        public async Task PuedeCrearCarritoCompraCorrecto()
        {

            var request = new CrearCarritoCommand()
            {
                Codigo = 1324,
                CedulaCliente = "1002543452"
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PostAsync("api/CarritoCompra", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta2 = await responseHttp.Content.ReadAsStringAsync();
            var respuesta = respuesta2.Substring(12, 40);
            respuesta.Should().Be("Se creó con exito el carrito de compras.");
            //var context = _factory.CreateContext();
            //var carrito = context.CarritoCompra.FirstOrDefault(t => t.Codigo == 1324);
            //carrito.Should().NotBeNull();
        }

        //[Fact]
        //public async Task PuedeAgregarACarritoCompraCorrecto()
        //{
        //    //var request2 = new CrearCarritoCommand()
        //    //{

        //    //    Codigo = 2534,
        //    //    CedulaCliente = "1002543452"
        //    //};

        //    //var jsonObject2 = JsonConvert.SerializeObject(request2);
        //    //var content2 = new StringContent(jsonObject2, Encoding.UTF8, "application/json");
        //    //var httpClient2 = _factory.CreateClient();
        //    //var responseHttp2 = await httpClient2.PostAsync("api/CarritoCompra", content2);
        //    //responseHttp2.StatusCode.Should().Be(HttpStatusCode.OK);
        //    //var respuesta2 = await responseHttp2.Content.ReadAsStringAsync();

        //    ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 543, cantidadVenta: 2);
        //    var request = new AgregarAlCarritoCommand(423,productoVenta,1);
        //    //{
        //    //    Id = 423,
        //    //    ProductoVenta = productoVenta,
        //    //    IdCarrito = request2.Id
        //    //};

        //    var jsonObject = JsonConvert.SerializeObject(request);
        //    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
        //    var httpClient = _factory.CreateClient();
        //    var responseHttp = await httpClient.PutAsync("api/CarritoCompra/add", content);
        //    responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
        //    var respuesta = await responseHttp.Content.ReadAsStringAsync();
        //    //var respuesta = respuesta2.Substring(12, 40);
        //    respuesta.Should().Be("Se ha agregado correctamente el producto");
        //    //var context = _factory.CreateContext();
        //    //var carrito = context.CarritoCompra.FirstOrDefault(t => t.Codigo == 1324);
        //    //carrito.Should().NotBeNull();
        //}

        //[Fact]
        //public async Task PuedeCrearPedidoCorrecto()
        //{
        //    var jabonSandia = new ProductoSimple(codigo: 1, nombre: "Jabón de Sandía",
        //    decripcion: " Ea hidrante facial y corporal", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
        //    var exfoliante = new ProductoSimple(codigo: 2, nombre: "Exfoliante Mujer",
        //    decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales",
        //    costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
        //    jabonSandia.EntradaProductos(cantidad: 10);
        //    exfoliante.EntradaProductos(cantidad: 10);

        //    CarritoCompra carrito = new CarritoCompra(codigo: 1, cedulaCliente: "1002353645");
        //    ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);

        //    carrito.AgregarAlCarrito(productoVenta);

        //    var request = new CrearPedidoCommand()
        //    {
        //        CodigoPedido = 1324,
        //        Carrito = carrito
        //    };

        //    var jsonObject = JsonConvert.SerializeObject(request);
        //    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
        //    var httpClient = _factory.CreateClient();
        //    var responseHttp = await httpClient.PostAsync("api/Pedido", content);
        //    responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
        //    var respuesta2 = await responseHttp.Content.ReadAsStringAsync();
        //    var respuesta = respuesta2.Substring(12, 30);
        //    respuesta.Should().Be("Se creó con éxito el pedido.");
        //    ////var context = _factory.CreateContext();
        //    ////var producto3421 = context.Producto.FirstOrDefault(t => t.CodigoProducto == 2123);
        //    ////producto3421.Should().NotBeNull();
        //}
    }
}
