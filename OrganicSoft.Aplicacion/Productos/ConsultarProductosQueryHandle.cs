using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var productos = _productoRepository.GetAll().Select(t => new ProductoViewModel { CodigoProducto = t.CodigoProducto }).ToList();
            return new ConsultarProductosQueryResponse(productos);
        }
    }

    public class ConsultarProductosQueryResponse 
    {
        public ConsultarProductosQueryResponse(List<ProductoViewModel> productos)
        {
            Productos = productos;
        }
        public List<ProductoViewModel> Productos{ get; set; }
    }

    public class ProductoViewModel
    {
        public int CodigoProducto { get;  set; }
        public string Nombre { get;  set; }
        public string Decripcion { get;  set; }
        public double Costo { get;  set; }
        public double Precio { get;  set; }
        public string Categoria { get;  set; }
        public string Presentacion { get;  set; }
        public int MinimoStock { get;  set; }
        public int CantidadExistente { get;  set; }
        
    }
}
