using FluentAssertions;
using Newtonsoft.Json;
using OrganicSoft.Aplicacion.Productos;
using OrganicSoft.WebApi.Angular.Test.Base;
using OrganicSoft.WepApi.Angular;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
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
            {
                
                TipoProducto = "SIMPLE",
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
            var context = _factory.CreateContext();
            var producto3421 = context.Producto.FirstOrDefault(t => t.CodigoProducto == 2123);
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
    }
}
