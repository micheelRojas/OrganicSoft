using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion
{
    public class SalidaProductoCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IProductoRepository _productoRepository;
        public SalidaProductoCommandHandle(IUnitOfWork unitOfWork, IProductoRepository productoRepository)
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;
        }
        public SalidaProductosResponse Handle(SalidaProductosCommand command)
        {

            var producto = _productoRepository.FindFirstOrDefault(producto => producto.Id == command.Id || producto.CodigoProducto==command.Id);//infraestructura-datos
            if (producto == null) return new SalidaProductosResponse("el producto no existe");
            var response = producto.SalidaProductos(command.Cantidad);//domain
            _productoRepository.Update(producto);//proyectarse el cambio y registrarlo en la unidad de trabajo
            _unitOfWork.Commit();//infraestructura-datos

            return new SalidaProductosResponse(response);
        }
        public class SalidaProductosCommand
        {
            public SalidaProductosCommand()
            {
            }

            public SalidaProductosCommand(int id, int cantidad)
            {
                Id = id;
                Cantidad = cantidad;
            }

            public int Id { get; set; }
            public int Cantidad { get; set; }
        }

        //public record EntradadeProductosRequest(int id, int cantidad);
        public record SalidaProductosResponse
        {
            public SalidaProductosResponse()
            {

            }

            public SalidaProductosResponse(string mensaje)
            {
                Mensaje = mensaje;
            }

            public string Mensaje { get; set; }
        }
    }
}
