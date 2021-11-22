using Microsoft.AspNetCore.Mvc;
using OrganicSoft.Aplicacion;
using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using OrganicSoft.Infraestructura;
using static OrganicSoft.Aplicacion.CrearProductoSimpleCommandHandle;
using static OrganicSoft.Aplicacion.EntradadeProductosCommandHandle;
using System.Linq;
using static OrganicSoft.Aplicacion.SalidaProductoCommandHandle;
using System.Collections.Generic;
using OrganicSoft.Aplicacion.Productos;
using System.Threading.Tasks;

namespace OrganicSoft.WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {

       /* private readonly IUnitOfWork _unitOfWork;
   
        private readonly IProductoRepository _productoRepository;
        private readonly OrganicSoftContext _context;

        public ProductoController(IUnitOfWork unitOfWork, IProductoRepository productoRepository, OrganicSoftContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;

        }
        [HttpGet]
        public ActionResult<List<ProductoViewModel>> GetProductos()
        {
            var result = new ConsultarProductosQueryHandle(_productoRepository).Handle();
                        
            return Ok(result.Productos);
        }

        [HttpPut]
        public EntradadeProductosResponse Put (EntradadeProductosCommand command)
        {
            var service = new EntradadeProductosCommandHandle(_unitOfWork, _productoRepository);
            var response = service.Handle(command);
            return response;
        }

        [HttpPut("{id}")]
        public SalidaProductosResponse PutSalidaProducto([FromRoute] int id, SalidaProductosCommand command)
        {
            var service = new SalidaProductoCommandHandle(_unitOfWork, _productoRepository);
            var response = service.Handle(command);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProducto([FromBody] CrearProductosCommand command)
        {
            var service = new CrearProductoSimpleCommandHandle(_unitOfWork, _productoRepository);
            var response = await service.Handle(command);

            if (response.isOk())
            {

                return Ok(response);

            }
            return BadRequest(response.Mensaje);

        }*/

    }
}