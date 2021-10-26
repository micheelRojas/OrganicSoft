using Microsoft.AspNetCore.Mvc;
using OrganicSoft.Aplicacion;
using OrganicSoft.Dominio.Contracts;
using static OrganicSoft.Aplicacion.CrearProductoCommandHandle;
using static OrganicSoft.Aplicacion.EntradadeProductosCommandHandle;

namespace OrganicSoft.WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IUnitOfWork unitOfWork, IProductoRepository productoRepository)
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;

        }

        
        [HttpPut]
        public EntradadeProductosResponse Put (EntradadeProductosCommand command)
        {
            var service = new EntradadeProductosCommandHandle(_unitOfWork, _productoRepository);
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