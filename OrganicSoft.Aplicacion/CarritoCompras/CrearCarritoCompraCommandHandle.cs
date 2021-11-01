using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.CarritoDeCompra
{
    public class CrearCarritoCompraCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICarritoCompraRepository _carritoCompraRepository;
        public CrearCarritoCompraCommandHandle(
           IUnitOfWork unitOfWork,
           ICarritoCompraRepository carritoCompraRepository
       )
        {
            _unitOfWork = unitOfWork;
            _carritoCompraRepository = carritoCompraRepository;

        }
        public CrearCarritoResponse Handle(CrearCarritoCommand command)
        {
            CarritoCompra carritoCompra = _carritoCompraRepository.FindFirstOrDefault(t => t.Id == command.Id);
            if (carritoCompra == null)
            {
                CarritoCompra carritoCompraNuevo = new CarritoCompra(command.Codigo, command.CedulaCliente);

                _carritoCompraRepository.Add(carritoCompraNuevo);
                _unitOfWork.Commit();
                return new CrearCarritoResponse($"Se creó con exito el carrito de compras.");
            }
            else
            {
                return new CrearCarritoResponse($"El carrito ya exite");
            }
        }
        public class CrearCarritoCommand
        {
            public int Id { get; set; }
            public int Codigo { get;  set; }
            public string CedulaCliente { get;  set; }

        }

        public record CrearCarritoResponse
        {
            public CrearCarritoResponse()
            {

            }

            public CrearCarritoResponse(string mensaje)
            {
                Mensaje = mensaje;
            }

            public string Mensaje { get; set; }
            public bool isOk()
            {
                return this.Mensaje.Equals("Se creó con exito el carrito de compras.");
            }
        }
    }
}
