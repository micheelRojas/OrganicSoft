using OrganicSoft.Aplicacion.ProductoFactory;
using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion
{
    public class CrearProductoCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;
        public CrearProductoCommandHandle(
           IUnitOfWork unitOfWork,
           IProductoRepository productoRepository
       )
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;
           
        }
        public string CrearProducto(ProductoRequest request)
        {
            Producto producto = _productoRepository.FindFirstOrDefault(t => t.Id == request.Id);
            if (producto == null)
            {
                Producto productoNuevo = TipoProducto.CrearProducto(
                                                request.TipoProducto,
                                                request.CodigoProducto,
                                                request.Nombre,
                                                request.Decripcion,
                                                 request.Precio,
                                                 request.Categoria,
                                                request.Presentacion,
                                                request.MinimoStock,
                                                request.Componetes,
                                                request.Costo
                                                );
                
                
                _productoRepository.Add(productoNuevo);
                _unitOfWork.Commit();
                return $"Se creó con exito el producto {productoNuevo.Nombre}.";
            }
            else
            {
                return $"El producto ya exite";
            }
        }
        public class ProductoRequest {
            public int Id { get; private set; }
            public string TipoProducto { get; set; }
            public int CodigoProducto { get; private set; }
            public string Nombre { get; private set; }
            public string Decripcion { get; private set; }
            public double Precio { get; private set; }
            public string Categoria { get; private set; }
            public string Presentacion { get; private set; }
            public int MinimoStock { get; private set; }
            public List<Componente> Componetes { get; private set; }
            public double Costo { get; private set; }
        }
        //public class ProductoComboRequest : ProductoRequest
        //{
        //    public List<Componente> Componetes { get; private set; }
        //}
        //public class ProductoSimpleRequest : ProductoRequest
        //{
        //    public double Costo { get; private set; }
        //}
        public static class TipoProducto
        {
            //mejorarar la creacion de productos
            public static Producto CrearProducto(string tipoProducto, int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock, List<Componente> componetes, double costo)
            {
                Producto producto = (Producto)new FabricadeProductos().metodoFabrica(tipoProducto, codigo, nombre, decripcion, precio, categoria, presentacion, minimoStock, componetes, costo);
                
                return producto;
            }
            
        }
        
    }
}
