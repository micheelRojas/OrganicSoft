using Microsoft.AspNetCore.Mvc;
using OrganicSoft.Aplicacion;
using OrganicSoft.Aplicacion.Productos;
using OrganicSoft.Dominio.Contracts;
using OrganicSoft.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OrganicSoft.Aplicacion.CrearProductoCommandHandle;
using static OrganicSoft.Aplicacion.EntradadeProductosCommandHandle;
using static OrganicSoft.Aplicacion.SalidaProductoCommandHandle;

namespace OrganicSoft.WepApi.Angular.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

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
        public EntradadeProductosResponse Put(EntradadeProductosCommand command)
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
        public CrearProductosResponse Post(CrearProductosCommand command)
        {
            var service = new CrearProductoCommandHandle(_unitOfWork, _productoRepository);
            var response = service.Handle(command);
            return response;
        }

    }
}
