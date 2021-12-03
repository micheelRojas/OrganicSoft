using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrganicSoft.Aplicacion.CarritoDeCompra.CrearCarritoCompraCommandHandle;

namespace OrganicSoft.Aplicacion.Pedidos
{
    public class CrearPedidoCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IProductoVentaRepository _productoVentaRepository;
        public CrearPedidoCommandHandle(IUnitOfWork unitOfWork, IPedidoRepository pedidoRepository, ICarritoCompraRepository carritoCompraRepository, IProductoVentaRepository productoVentaRepository)
        {
            _unitOfWork = unitOfWork;
            _pedidoRepository = pedidoRepository;
            _carritoCompraRepository = carritoCompraRepository;
            _productoVentaRepository = productoVentaRepository;
        }
        public CrearPedidoResponse Handle(CrearPedidoCommand command)
        {
            Pedido pedido = _pedidoRepository.FindFirstOrDefault(t => t.Id == command.Id || t.CodigoPedido == command.Id);
            if (pedido == null)
            {
                var carritoCompra = _carritoCompraRepository.FindFirstOrDefault(carrito => carrito.Id == command.Carrito.Codigo || carrito.Codigo == command.Carrito.Codigo || carrito.Id==command.Carrito.Id);
                if (carritoCompra != null)
                {
                    ProductoVenta productos = _productoVentaRepository.FindFirstOrDefault(t => t.CarritoCompraId == carritoCompra.Id);
                    if (productos== null) { return new CrearPedidoResponse("Error el carrito no tiene productos"); }
                    Pedido pedidoNuevo = new Pedido();
                    pedidoNuevo.GenerarPedido(command.CodigoPedido, carritoCompra);
                    _pedidoRepository.Add(pedidoNuevo);
                    _unitOfWork.Commit();
                    return new CrearPedidoResponse("Se creó con exito el pedido.");
                }
                return new CrearPedidoResponse("No se encontró un carrito de compras para el pedido");
            }
            else
            {
                return new CrearPedidoResponse("El pedido ya existe");
            }
        }
    }

    public class CrearPedidoCommand
    {
        public CrearPedidoCommand()
        {
        }

        public CrearPedidoCommand(int id, int codigoPedido, CrearCarritoCommand carrito)
        {
            Id = id;
            CodigoPedido = codigoPedido;
            Carrito = carrito;
        }

       

        public int Id { get; set; }
        public int CodigoPedido { get; set; }
        public CrearCarritoCommand Carrito { get; set; }
       
    }
   
    public class CrearPedidoResponse
    {
        public CrearPedidoResponse()
        {

        }

        public CrearPedidoResponse(string mensaje)
        {
            Mensaje = mensaje;
        }

        public string Mensaje { get; set; }
        public bool isOk()
        {
            return this.Mensaje.Equals("Se creó con exito el pedido.");
        }
    }
}

