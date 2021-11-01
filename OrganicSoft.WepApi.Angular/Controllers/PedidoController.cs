using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganicSoft.Aplicacion;
using OrganicSoft.Aplicacion.Facturas;
using OrganicSoft.Aplicacion.Pedidos;
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
    public class PedidoController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IPedidoRepository _pedidoRepository;
        private readonly OrganicSoftContext _context;

        public PedidoController(IUnitOfWork unitOfWork, IPedidoRepository pedidoRepository, OrganicSoftContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _pedidoRepository = pedidoRepository;

        }
        [HttpGet]
        public ActionResult<List<PedidoViewModel>> GetPedidos()
        {
            var result = new ConsultarPedidosQueryHandle(_pedidoRepository).Handle();

            return Ok(result.Pedidos);
        }

        /*[HttpPut("{id}")]
        public GenerarFacturaResponse PutSalidaProducto([FromRoute] int id, GenerarFacturaCommand command)
        {
            var service = new GenerarFacturaCommandHandle(_unitOfWork, _facturaRepository);
            var response = service.Handle(command); //Se comenta porque factura no se debería actualizar
            return response;
        } */

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedido([FromRoute] int id)
        {
            Pedido pedido = await _context.Pedido.SingleOrDefaultAsync(t => t.Id == id);
            if (pedido == null)
                return NotFound();
            return Ok(pedido);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePedido([FromBody] CrearPedidoCommand command)
        {
            var service = new CrearPedidoCommandHandle(_unitOfWork, _pedidoRepository);
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
