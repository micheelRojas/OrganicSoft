using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.CarritoDeCompra
{
    public class AgregarAlCarritoCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICarritoCompraRepository _carritoCompraRepository;
        public AgregarAlCarritoCommandHandle(IUnitOfWork unitOfWork, ICarritoCompraRepository carritoCompraRepository)
        {
            _unitOfWork = unitOfWork;
            _carritoCompraRepository = carritoCompraRepository;
        }
        public AgregarAlCarritoResponse Handle(AgregarAlCarritoCommand command)
        {

            var carritoCompra = _carritoCompraRepository.FindFirstOrDefault(carrito => carrito.Id == command.Id);//infraestructura-datos
            if (carritoCompra == null) return new AgregarAlCarritoResponse("el producto no existe");
            var response = carritoCompra.AgregarAlCarrito(command.ProductoVenta);//domain
            _carritoCompraRepository.Update(carritoCompra);//proyectarse el cambio y registrarlo en la unidad de trabajo
            _unitOfWork.Commit();//infraestructura-datos

            return new AgregarAlCarritoResponse(response);
        }
        public class AgregarAlCarritoCommand
        {
            public int Id { get; set; }
            public ProductoVenta ProductoVenta { get; set; }
        }

        public record AgregarAlCarritoResponse
        {
            public AgregarAlCarritoResponse()
            {

            }

            public AgregarAlCarritoResponse(string mensaje)
            {
                Mensaje = mensaje;
            }

            public string Mensaje { get; set; }
            public bool isOk()
            {
                return this.Mensaje != ("No se pudo añadir el producto al carrito");
            }
        }
    }
}
