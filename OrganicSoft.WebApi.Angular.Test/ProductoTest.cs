using FluentAssertions;
using Newtonsoft.Json;
using OrganicSoft.Aplicacion.Facturas;
using OrganicSoft.Aplicacion.Pedidos;
using OrganicSoft.Aplicacion.Productos;
using OrganicSoft.Dominio;
using OrganicSoft.Infraestructura;
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

        private readonly OrganicSoftContext _context;

        public ProductoTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _context = _factory._context;
        }

        [Fact]
        public async Task PuedeCrearProductoSimpleCorrecto()
        {
            var request = new CrearProductosCommand()
            {   Id = 0,
                CodigoProducto = 2123,
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
            var producto3421 = _context.Producto.FirstOrDefault(t => t.CodigoProducto == 2123);
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
            var producto3421 = _context.Producto.FirstOrDefault(t => t.CodigoProducto == 2123);
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
            var producto3421 = _context.Producto.FirstOrDefault(t => t.CodigoProducto == 4231);
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
            var producto3421 = _context.Producto.FirstOrDefault(t => t.CodigoProducto == 2123);
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
            var producto3421 = _context.Producto.FirstOrDefault(t => t.CodigoProducto == 3241);
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
        }

        private async Task CrearProducto(int codigoProducto)
        {
            var request4 = new CrearProductosCommand()
            {
                Id = 0,
                CodigoProducto = codigoProducto,
                Nombre = "Jabón de cuerpo",
                Descripcion = "Jabón para el cuerpo",
                Precio = 10000,
                Categoria = "Jabones",
                Presentacion = "Pequeño",
                MinimoStock = 2,
                Costo = 12000
            };

            var jsonObject4 = JsonConvert.SerializeObject(request4);
            var content4 = new StringContent(jsonObject4, Encoding.UTF8, "application/json");
            var httpClient4 = _factory.CreateClient();
            var responseHttp4 = await httpClient4.PostAsync("api/Producto", content4);
            responseHttp4.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta4 = await responseHttp4.Content.ReadAsStringAsync();
        }
        private async Task<CrearCarritoCommand> CrearCarrito(int codigoCarrito)
        {
            var request2 = new CrearCarritoCommand()
            {

                Codigo = codigoCarrito,
                CedulaCliente = "1002543452"
            };

            var jsonObject2 = JsonConvert.SerializeObject(request2);
            var content2 = new StringContent(jsonObject2, Encoding.UTF8, "application/json");
            var httpClient2 = _factory.CreateClient();
            var responseHttp2 = await httpClient2.PostAsync("api/CarritoCompra", content2);
            responseHttp2.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta2 = await responseHttp2.Content.ReadAsStringAsync();

            return request2;
        }
        [Fact]
        public async Task PuedeAgregarACarritoCompraCorrecto()
        {
            //Creación de un producto para carrito
            await CrearProducto(21235);

            //Creación del carrito
            var request2 = await CrearCarrito(2534);

            ProductoVentaCommad productoVenta = new ProductoVentaCommad(codigoProducto: 21235, cantidadVenta: 2);
            var request = new AgregarAlCarritoCommand()
            {
                Id = request2.Codigo,
                ProductoVenta = productoVenta
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync("api/CarritoCompra/add", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            var respuesta3 = respuesta.Substring(12, 40);
            respuesta3.Should().Be("Se ha agregado correctamente el producto");
            //var context = _factory.CreateContext();
            var carrito = _context.CarritoCompra.FirstOrDefault(t => t.Codigo == 2534);
            carrito.Should().NotBeNull();
        }

        [Fact]
        public async Task NoPuedeAgregarACarritoCompraCorrecto()
        {

            //Creación del carrito
            await CrearCarrito(25345);

            ProductoVentaCommad productoVenta = new ProductoVentaCommad(codigoProducto: 212, cantidadVenta: 2);
            var request = new AgregarAlCarritoCommand()
            {
                Id = 16875757,
                ProductoVenta = productoVenta
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync("api/CarritoCompra/add", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Be("el carrito no existe");
            //var context = _factory.CreateContext();
            var carrito = _context.CarritoCompra.FirstOrDefault(t => t.Codigo == 25345);
            carrito.Should().NotBeNull();
        }

        private async Task<string> CrearPedido(int codigoPedido, CrearCarritoCommand carrito)
        {
            var request5 = new CrearPedidoCommand()
            {
                CodigoPedido = codigoPedido,
                Carrito = carrito
            };

            var jsonObject5 = JsonConvert.SerializeObject(request5);
            var content5 = new StringContent(jsonObject5, Encoding.UTF8, "application/json");
            var httpClient5 = _factory.CreateClient();
            var responseHttp5 = await httpClient5.PostAsync("api/Pedido", content5);
            responseHttp5.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respuesta5 = await responseHttp5.Content.ReadAsStringAsync();
            return respuesta5;
        }
        [Fact]
        public async Task NoPuedeCrearPedidoCorrecto()
        {
            //Creación de un producto para carrito
            await CrearProducto(212356);

            //Creación del carrito
            var request2 = await CrearCarrito(257);

            ProductoVentaCommad productoVenta = new ProductoVentaCommad(codigoProducto: 2123567, cantidadVenta: 2);
            var request = new AgregarAlCarritoCommand()
            {
                Id = request2.Codigo,
                ProductoVenta = productoVenta
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync("api/CarritoCompra/add", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();

            //Pedido correcto
            await CrearPedido(3245, request2);

            //Pedido incorrecto
            String respuesta6 = await CrearPedido(3245, new CrearCarritoCommand(745, 745, "100"));

            respuesta6.Should().Be("No se encontró un carrito de compras para el pedido");
            var pedido3421 = _context.Pedido.FirstOrDefault(t => t.CodigoPedido == 3245);
            pedido3421.Should().BeNull();
        }

        [Fact]
        public async Task PuedeCrearPedidoCorrecto()
        {
            //Creación de un producto para carrito
            await CrearProducto(2123567);

            //Creación del carrito
            var request2 = await CrearCarrito(2521);

            ProductoVentaCommad productoVenta = new ProductoVentaCommad(codigoProducto: 2123567, cantidadVenta: 2);
            var request = new AgregarAlCarritoCommand()
            {
                Id = request2.Codigo,
                ProductoVenta = productoVenta
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync("api/CarritoCompra/add", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();

            var request5 = new CrearPedidoCommand()
            {
                CodigoPedido = 32456,
                Carrito = request2
            };

            var jsonObject5 = JsonConvert.SerializeObject(request5);
            var content5 = new StringContent(jsonObject5, Encoding.UTF8, "application/json");
            var httpClient5 = _factory.CreateClient();
            var responseHttp5 = await httpClient5.PostAsync("api/Pedido", content5);
            responseHttp5.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta5 = await responseHttp5.Content.ReadAsStringAsync();
            var respuesta6 = respuesta5.Substring(12, 28);
            respuesta6.Should().Be("Se creó con exito el pedido.");
            var pedido3421 = _context.Pedido.FirstOrDefault(t => t.CodigoPedido == 32456);
            pedido3421.Should().NotBeNull();
        }

        [Fact]
        public async Task PuedeGenerarFacturaCorrecta()
        {
            //Creación de un producto para carrito
            await CrearProducto(2123567788);

            //Creación del carrito
            var request2 = await CrearCarrito(2564644);

            ProductoVentaCommad productoVenta = new ProductoVentaCommad(codigoProducto: 2123567788, cantidadVenta: 2);
            var request = new AgregarAlCarritoCommand()
            {
                Id = request2.Codigo,
                ProductoVenta = productoVenta
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync("api/CarritoCompra/add", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();

            //Creación del pedido
            var request5 = new CrearPedidoCommand()
            {
                CodigoPedido = 3245696,
                Carrito = request2
            };

            var jsonObject5 = JsonConvert.SerializeObject(request5);
            var content5 = new StringContent(jsonObject5, Encoding.UTF8, "application/json");
            var httpClient5 = _factory.CreateClient();
            var responseHttp5 = await httpClient5.PostAsync("api/Pedido", content5);
            responseHttp5.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta5 = await responseHttp5.Content.ReadAsStringAsync();

            //Creación de la factura
            var respuesta7 = await CrearFactura(98754, request5);

            var respuesta8 = respuesta7.Substring(12, 28);
            respuesta8.Should().Be("Se creó con exito la factura");
            var factura = _context.Factura.FirstOrDefault(t => t.Codigo == 98754);
            factura.Should().NotBeNull();
        }

        private async Task<string> CrearFactura(int codigoFactura, CrearPedidoCommand pedido)
        {
            var request6 = new GenerarFacturaCommand()
            {
                Codigo = codigoFactura,
                Pedido = pedido
            };

            var jsonObject6 = JsonConvert.SerializeObject(request6);
            var content6 = new StringContent(jsonObject6, Encoding.UTF8, "application/json");
            var httpClient6 = _factory.CreateClient();
            var responseHttp6 = await httpClient6.PostAsync("api/Factura", content6);
            responseHttp6.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta7 = await responseHttp6.Content.ReadAsStringAsync();
            return respuesta7;
        }

        [Fact]
        public async Task NoPuedeGenerarFacturaCorrecta()
        {
            //Creación de un producto para carrito
            var request4 = new CrearProductosCommand()
            {
                Id = 0,
                CodigoProducto = 312356778,
                Nombre = "Jabón de cuerpo",
                Descripcion = "Jabón para el cuerpo",
                Precio = 10000,
                Categoria = "Jabones",
                Presentacion = "Pequeño",
                MinimoStock = 2,
                Costo = 12000
            };

            var jsonObject4 = JsonConvert.SerializeObject(request4);
            var content4 = new StringContent(jsonObject4, Encoding.UTF8, "application/json");
            var httpClient4 = _factory.CreateClient();
            var responseHttp4 = await httpClient4.PostAsync("api/Producto", content4);
            responseHttp4.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta4 = await responseHttp4.Content.ReadAsStringAsync();

            //Creación del carrito
            var request2 = new CrearCarritoCommand()
            {

                Codigo = 25646,
                CedulaCliente = "1002543452"
            };

            var jsonObject2 = JsonConvert.SerializeObject(request2);
            var content2 = new StringContent(jsonObject2, Encoding.UTF8, "application/json");
            var httpClient2 = _factory.CreateClient();
            var responseHttp2 = await httpClient2.PostAsync("api/CarritoCompra", content2);
            responseHttp2.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta2 = await responseHttp2.Content.ReadAsStringAsync();

            ProductoVentaCommad productoVenta = new ProductoVentaCommad(codigoProducto: 212356787, cantidadVenta: 2);
            var request = new AgregarAlCarritoCommand()
            {
                Id = request2.Codigo,
                ProductoVenta = productoVenta
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PutAsync("api/CarritoCompra/add", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();


            //Creación de la factura
            var request6 = new GenerarFacturaCommand()
            {
                Codigo = 98754,
                Pedido = new CrearPedidoCommand(1111, 1111, new CrearCarritoCommand(111,111,"100"))
            };

            var jsonObject6 = JsonConvert.SerializeObject(request6);
            var content6 = new StringContent(jsonObject6, Encoding.UTF8, "application/json");
            var httpClient6 = _factory.CreateClient();
            var responseHttp6 = await httpClient6.PostAsync("api/Factura", content6);
            responseHttp6.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respuesta7 = await responseHttp6.Content.ReadAsStringAsync();
            //var respuesta8 = respuesta7.Substring(12, 28);
            respuesta7.Should().Be("No hay pedido por confirmar");
            //var context = _factory.CreateContext();
            //var factura = _context.Factura.FirstOrDefault(t => t.Codigo == 98754);
            //factura.Should().NotBeNull();
        }
    }
}
