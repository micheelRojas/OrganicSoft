using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Pedidos
{
    public class CrearPedidoCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoRepository _pedidoRepository;
        public CrearPedidoCommandHandle(IUnitOfWork unitOfWork, IPedidoRepository pedidoRepository)
        {
            _unitOfWork = unitOfWork;
            _pedidoRepository = pedidoRepository;
        }
        public CrearPedidoResponse Handle(CrearPedidoCommand command)
        {
            Pedido pedido = _pedidoRepository.FindFirstOrDefault(t => t.Id == command.Id);
            if (pedido == null)
            {
                Pedido pedidoNuevo = new Pedido();
                pedidoNuevo.GenerarPedido(command.CodigoPedido, command.Carrito);
                _pedidoRepository.Add(pedidoNuevo);
                _unitOfWork.Commit();
                return new CrearPedidoResponse("Se creó con exito el pedido.");
            }
            else
            {
                return new CrearPedidoResponse("La factura ya exite");
            }
        }
    }

    public class CrearPedidoCommand
    {
        public CrearPedidoCommand()
        {
        }

        public CrearPedidoCommand(int id, int codigoPedido, CarritoCompra carrito)
        {
            Id = id;
            CodigoPedido = codigoPedido;
            Carrito = carrito;
        }

        public int Id { get;  set; }
        public int CodigoPedido { get;  set; }
        public CarritoCompra Carrito { get;  set; }
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

