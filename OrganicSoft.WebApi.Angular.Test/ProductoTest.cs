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
                Id = 3421,
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
    }
}
