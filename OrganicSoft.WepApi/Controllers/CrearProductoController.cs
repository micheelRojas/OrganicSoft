using Microsoft.AspNetCore.Mvc;
using OrganicSoft.Aplicacion;
using OrganicSoft.Dominio.Contracts;
using static OrganicSoft.Aplicacion.EntradadeProductosCommandHandle;

namespace OrganicSoft.WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsignacionController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public ConsignacionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        /*
        [HttpPost]
        public EntradadeProductosResponse Post(EntradadeProductosCommand command)
        {
            var service = new EntradadeProductosCommandHandle(_unitOfWork);
            var response = service.Handle(command);
            return response;
        }*/
    }
}