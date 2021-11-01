using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganicSoft.Aplicacion;
using OrganicSoft.Aplicacion.Facturas;
using OrganicSoft.Aplicacion.Productos;
using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using OrganicSoft.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicSoft.WepApi.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IFacturaRepository _facturaRepository;
        private readonly OrganicSoftContext _context;

        public FacturaController(IUnitOfWork unitOfWork, IFacturaRepository productoRepository, OrganicSoftContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _facturaRepository = productoRepository;

        }
        [HttpGet]
        public ActionResult<List<FacturaViewModel>> GetFacturas()
        {
            var result = new ConsultarFacturasQueryHandle(_facturaRepository).Handle();

            return Ok(result.Facturas);
        }

        /*[HttpPut("{id}")]
        public GenerarFacturaResponse PutSalidaProducto([FromRoute] int id, GenerarFacturaCommand command)
        {
            var service = new GenerarFacturaCommandHandle(_unitOfWork, _facturaRepository);
            var response = service.Handle(command); //Se comenta porque factura no se debería actualizar
            return response;
        } */ 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactura([FromRoute] int id)
        {
            Factura factura = await _context.Factura.SingleOrDefaultAsync(t => t.Id == id);
            if (factura == null)
                return NotFound();
            return Ok(factura);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFactura([FromBody] GenerarFacturaCommand command)
        {
            var service = new GenerarFacturaCommandHandle(_unitOfWork, _facturaRepository);
            var response = service.Handle(command);

            if (response.isOk())
            {
                await _context.SaveChangesAsync();
                return Ok(response);
            }
            return BadRequest(response.Mensaje);

        }

    }
}