using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganicSoft.Aplicacion;
using OrganicSoft.Aplicacion.CarritoCompras;
using OrganicSoft.Aplicacion.CarritoDeCompra;
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
using static OrganicSoft.Aplicacion.CarritoCompras.EliminarDelCarritoCommandHandle;
using static OrganicSoft.Aplicacion.CarritoDeCompra.AgregarAlCarritoCommandHandle;
using static OrganicSoft.Aplicacion.CarritoDeCompra.CrearCarritoCompraCommandHandle;

namespace OrganicSoft.WepApi.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoCompraController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly OrganicSoftContext _context;

        public CarritoCompraController(IUnitOfWork unitOfWork, IProductoRepository productoRepository, ICarritoCompraRepository carritoCompraRepository, OrganicSoftContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _carritoCompraRepository = carritoCompraRepository;
            _productoRepository = productoRepository;

        }
        [HttpGet]
        public ActionResult<List<CarritoCompraViewModel>> GetCarritos()
        {
            var result = new ConsultarCarritoCompraQueryHandle(_carritoCompraRepository).Handle();

            return Ok(result.Carritos);
        }
        /*
        [HttpPut("{id}")]
        public CrearPedidoResponse Put([FromRoute] int id, CrearPedidoCommand command)
        {
            var service = new CrearPedidoCommandHandle(_unitOfWork, _pedidoRepository);
            var response = service.Handle(command); 
            return response; //Lo único modificable del pedido es el estado y se hace automáticamente
        } */

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarrito([FromRoute] int id)
        {
            CarritoCompra carritoCompra = await _context.CarritoCompra.SingleOrDefaultAsync(t => t.Id == id);
            if (carritoCompra == null)
                return NotFound();
            return Ok(carritoCompra);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCarritoCompra([FromBody] CrearCarritoCommand command)
        {
            var service = new CrearCarritoCompraCommandHandle(_unitOfWork, _carritoCompraRepository);
            var response = service.Handle(command);

            if (response.isOk())
            {
                await _context.SaveChangesAsync();
                return Ok(response);
            }
            return BadRequest(response.Mensaje);

        }

        [HttpPut("add")]
        public ActionResult addToCarrito([FromBody] AgregarAlCarritoCommand command)
        {
            var service = new AgregarAlCarritoCommandHandle(_unitOfWork, _carritoCompraRepository, _productoRepository);
            var response = service.Handle(command);
            if (response.isOk())
            {
                return Ok(response);
            }
            return BadRequest(response.Mensaje);

        }

        [HttpPut("remove")]
        public ActionResult removeOfCarrito([FromBody] EliminarDelCarritoCommand command)
        {
            var service = new EliminarDelCarritoCommandHandle(_unitOfWork, _carritoCompraRepository);
            var response = service.Handle(command);
            if (response.isOk())
            {
                return Ok(response);
            }
            return BadRequest(response.Mensaje);

        }

    }
}
