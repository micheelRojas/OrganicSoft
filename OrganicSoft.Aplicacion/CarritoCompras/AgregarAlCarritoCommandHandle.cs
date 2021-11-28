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
        private readonly IProductoRepository _productoRepository;
        public AgregarAlCarritoCommandHandle(IUnitOfWork unitOfWork, ICarritoCompraRepository carritoCompraRepository, IProductoRepository productoRepository)
        {
            _unitOfWork = unitOfWork;
            _carritoCompraRepository = carritoCompraRepository;
            _productoRepository = productoRepository;
        }
        public AgregarAlCarritoResponse Handle(AgregarAlCarritoCommand command)
        {

            var carritoCompra = _carritoCompraRepository.FindFirstOrDefault(carrito => carrito.Id == command.Id || carrito.Codigo == command.Id);//infraestructura-datos
            if (carritoCompra == null) return new AgregarAlCarritoResponse("el carrito no existe");

            String response = "No se pudo agregar el producto al carrito";

            // VERSION1 COMO FILTRAR VERSION LENTA filtro en el cliente
            var productoParaElCarrito = _productoRepository.FindBy(t => t.CodigoProducto == command.ProductoVenta.CodigoProducto || t.CodigoProducto==command.ProductoVenta.CodigoProducto);
            if (productoParaElCarrito == null)
            {
                return new AgregarAlCarritoResponse(response);
            }
            response = carritoCompra.AgregarAlCarrito(new ProductoVenta(command.ProductoVenta.CodigoProducto,command.ProductoVenta.CantidadVenta));
            _carritoCompraRepository.Update(carritoCompra);//proyectarse el cambio y registrarlo en la unidad de trabajo
            _unitOfWork.Commit();//infraestructura-datos

            ////VERSION2 COMO filtro en la base de datos
            //var productoParaElCarrito1 = _productoRepository.FindBy(t => t.CodigoProducto == command.ProductoVenta.CodigoProducto);
            //if (productoParaElCarrito1 == null)
            //{
            //    return new AgregarAlCarritoResponse(response);
            //}
            //response = carritoCompra.AgregarAlCarrito(command.ProductoVenta);
            //_carritoCompraRepository.Update(carritoCompra);//proyectarse el cambio y registrarlo en la unidad de trabajo
            //_unitOfWork.Commit();//infraestructura-datos



            //foreach (var producto in _productoRepository.GetAll().ToList())
            //{
            //    if (command.ProductoVenta.CodigoProducto.Equals(producto.CodigoProducto))
            //    {
            //        response = carritoCompra.AgregarAlCarrito(command.ProductoVenta);
            //        _carritoCompraRepository.Update(carritoCompra);//proyectarse el cambio y registrarlo en la unidad de trabajo
            //        _unitOfWork.Commit();//infraestructura-datos

            //        return new AgregarAlCarritoResponse(response);
            //    }
            //}
            return new AgregarAlCarritoResponse(response);


        }
        public class AgregarAlCarritoCommand
        {
            public AgregarAlCarritoCommand()
            {
            }

            public AgregarAlCarritoCommand(int id, ProductoVentaCommad productoVenta)
            {
                Id = id;
                ProductoVenta = productoVenta;

            }

            public int Id { get; set; }
            public ProductoVentaCommad ProductoVenta { get; set; }
        }
        public class ProductoVentaCommad
         {
            public ProductoVentaCommad()
            {
            }

            public ProductoVentaCommad(int codigoProducto, int cantidadVenta)
            {
                CodigoProducto = codigoProducto;
                CantidadVenta = cantidadVenta;
            }

            public int CodigoProducto { get; set; }
            public int CantidadVenta { get;  set; }
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
