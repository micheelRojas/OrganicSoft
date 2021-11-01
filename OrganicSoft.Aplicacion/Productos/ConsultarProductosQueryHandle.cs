using OrganicSoft.Dominio.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace OrganicSoft.Aplicacion.Productos
{
    public class ConsultarProductosQueryHandle
    {
        private IProductoRepository _productoRepository;
        public ConsultarProductosQueryHandle(
          IProductoRepository productoRepository
        )
        {
            _productoRepository = productoRepository;
        }
        public ConsultarProductosQueryResponse Handle()
        {
            var productos = _productoRepository.GetAll().Select(t => new ProductoViewModel
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Costo = t.Costo,
                Precio= t.Precio,
                CantidadExistente = t.CantidadExistente,
                CantidadVendida = t.CantidadVendida
            }).ToList();
            return new ConsultarProductosQueryResponse(productos);
        }
    }

    public class ConsultarProductosQueryResponse
    {
        public ConsultarProductosQueryResponse(List<ProductoViewModel> productos)
        {
            Productos = productos;
        }
        public List<ProductoViewModel> Productos { get; set; }
    }

    public class ProductoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public int CantidadExistente { get; set; }
        public int CantidadVendida { get; set; }

    }
}
