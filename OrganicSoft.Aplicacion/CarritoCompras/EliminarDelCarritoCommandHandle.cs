using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.CarritoCompras
{
    public class EliminarDelCarritoCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IProductoRepository _productoRepository;
        public EliminarDelCarritoCommandHandle(IUnitOfWork unitOfWork, ICarritoCompraRepository carritoCompraRepository, IProductoRepository productoRepository)
        {
            _unitOfWork = unitOfWork;
            _carritoCompraRepository = carritoCompraRepository;
            _productoRepository = productoRepository;
        }
        public EliminarDelCarritoResponse Handle(EliminarDelCarritoCommand command)
        {

            var carritoCompra = _carritoCompraRepository.FindFirstOrDefault(carrito => carrito.Id == command.IdCarrito || carrito.Codigo == command.IdCarrito);//infraestructura-datos
            if (carritoCompra == null) return new EliminarDelCarritoResponse("el carrito no existe");

            String response = "No se pudo eliminar el producto del carrito";

            // VERSION1 COMO FILTRAR VERSION LENTA filtro en el cliente
            
            response = carritoCompra.EliminarDelCarrito(command.ProductoVenta.Id);
            _carritoCompraRepository.Update(carritoCompra);//proyectarse el cambio y registrarlo en la unidad de trabajo
            _unitOfWork.Commit();//infraestructura-datos

            return new EliminarDelCarritoResponse(response);

        }
        public class EliminarDelCarritoCommand
        {
            public EliminarDelCarritoCommand()
            {
            }

            public EliminarDelCarritoCommand(int id, ProductoVenta productoVenta, int idCarrito)
            {
                Id = id;
                ProductoVenta = productoVenta;
                IdCarrito = idCarrito;
            }

            public int Id { get; set; }
            public ProductoVenta ProductoVenta { get; set; }
            public int IdCarrito { get; set; }
        }

        public record EliminarDelCarritoResponse
        {
            public EliminarDelCarritoResponse()
            {

            }

            public EliminarDelCarritoResponse(string mensaje)
            {
                Mensaje = mensaje;
            }

            public string Mensaje { get; set; }
            public bool isOk()
            {
                return this.Mensaje != ("No se pudo eliminar el producto del carrito");
            }
        }
    }
}
