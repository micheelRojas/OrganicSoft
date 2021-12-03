using OrganicSoft.Aplicacion.Pedidos;
using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Facturas
{
    public class GenerarFacturaCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFacturaRepository _facturaRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICarritoCompraRepository _carritoRepository;
        private readonly IProductoVentaRepository _productoVentaRepository;
        private readonly IProductoRepository _productoRepository;
        public GenerarFacturaCommandHandle(IUnitOfWork unitOfWork, IFacturaRepository facturaRepository,
            IPedidoRepository pedidoRepository, ICarritoCompraRepository carritoCompraRepository,
            IProductoVentaRepository productoVentaRepository, IProductoRepository productoRepository)
        {
            _unitOfWork = unitOfWork;
            _facturaRepository = facturaRepository;
            _pedidoRepository = pedidoRepository;
            _carritoRepository = carritoCompraRepository;
            _productoVentaRepository = productoVentaRepository;
            _productoRepository = productoRepository;
        }
        public GenerarFacturaResponse Handle(GenerarFacturaCommand command)
        {
            Factura factura = _facturaRepository.FindFirstOrDefault(t => t.Id == command.Id || t.Codigo == command.Id);
            if (factura == null)
            {
                Pedido pedido = _pedidoRepository.FindFirstOrDefault(t => t.Id == command.Pedido.CodigoPedido || t.CodigoPedido == command.Pedido.CodigoPedido);
                if (pedido != null)
                {
                    var carrito = _carritoRepository.FindFirstOrDefault(t => t.Id == pedido.CarritoId);
                    List<ProductoVenta> productoVentas = _productoVentaRepository.GetAll().Where(t => t.CarritoCompraId == pedido.CarritoId).ToList();
                    List<Producto> productos = _productoRepository.GetAll().ToList();
                    pedido.ConfirmarPedido();
                    Factura facturaNueva = new Factura(
                        command.Codigo, DateTime.Now, carrito.CedulaCliente, pedido, productos, productoVentas
                        );

                    _facturaRepository.Add(facturaNueva);
                    _unitOfWork.Commit();
                    return new GenerarFacturaResponse($"Se creó con exito la factura.");
                }
                else
                {
                    return new GenerarFacturaResponse("No hay pedido por confirmar");
                }
            }
            else
            {
                return new GenerarFacturaResponse($"La factura ya existe");
            }
        }
    }

    public class GenerarFacturaCommand
    {
        public GenerarFacturaCommand()
        {
        }

        public GenerarFacturaCommand(int id, int codigo, CrearPedidoCommand
            pedido)
        {
            Id = id;
            Codigo = codigo;
            Pedido = pedido;
        }

        public int Id { get; set; }
        public int Codigo { get;  set; }
        public CrearPedidoCommand Pedido { get;  set; }


    }

    public class GenerarFacturaResponse
    {
        public GenerarFacturaResponse()
        {

        }

        public GenerarFacturaResponse(string mensaje)
        {
            Mensaje = mensaje;
        }

        public string Mensaje { get; set; }
        public bool isOk()
        {
            return this.Mensaje.Equals("Se creó con exito la factura.");
        }
    }
}
