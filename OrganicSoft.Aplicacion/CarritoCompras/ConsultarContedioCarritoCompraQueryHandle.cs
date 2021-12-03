using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System.Collections.Generic;
using System.Linq;
using static OrganicSoft.Aplicacion.CarritoDeCompra.AgregarAlCarritoCommandHandle;

namespace OrganicSoft.Aplicacion.CarritoCompras
{
    public class ConsultarContedioCarritoCompraQueryHandle
    {
        private IProductoVentaRepository _productoVentaRepository;
        private IProductoRepository _productoRepository;
        public ConsultarContedioCarritoCompraQueryHandle(IProductoVentaRepository productoVentaRepository, IProductoRepository productoRepository)
        {
            _productoVentaRepository = productoVentaRepository;
            _productoRepository = productoRepository;
        }
        public ConsultarContedidoCarritoQueryResponse Handle(int id)
        {
            List<ProductoVenta> productoVentas = _productoVentaRepository.GetAll().Where(t => t.CarritoCompraId== id).ToList();
            List<Producto> productos = _productoRepository.GetAll().ToList();
          
            return new ConsultarContedidoCarritoQueryResponse(ViewContenido.LLenar(productos,productoVentas));
        }
    }

    public class ConsultarContedidoCarritoQueryResponse
    {
        public ConsultarContedidoCarritoQueryResponse(List<ViewContenido> contenidos)
        {
            Productos = contenidos;
        }
        public List<ViewContenido> Productos { get; set; }
    }

    
    public class ViewContenido
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Total { get; set; }

     
        public static List<ViewContenido>  LLenar(List<Producto> productos, List<ProductoVenta> productoVentas)
        {
            List<ViewContenido> contenidos = new List<ViewContenido>();


            foreach (ProductoVenta productoVenta in productoVentas)
            {
                foreach (Producto producto in productos)
                {
                    if (productoVenta.CodigoProducto.Equals(producto.Id))
                    {
                        ViewContenido contenido = new ViewContenido();
                        contenido.Id = producto.Id;
                        contenido.Nombre = producto.Nombre;
                        contenido.Cantidad = productoVenta.CantidadVenta;
                        contenido.PrecioUnitario = producto.PrecioConDescuento;
                        contenido.Total = producto.PrecioConDescuento * productoVenta.CantidadVenta;
                        contenidos.Add(contenido);
                       
                    }
                }
            }
            return contenidos;

        }
    }

}
