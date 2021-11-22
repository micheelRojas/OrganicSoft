using OrganicSoft.Aplicacion.ProductoFactory;
using OrganicSoft.Aplicacion.Productos;
using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion
{
    public class CrearProductoSimpleCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;
        public CrearProductoSimpleCommandHandle(
           IUnitOfWork unitOfWork,
           IProductoRepository productoRepository
       )
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;

        }
        public async Task<CrearProductosResponse> Handle(CrearProductosCommand command)
        {
            Producto producto = _productoRepository.FindFirstOrDefault(t => t.Id == command.Id || t.CodigoProducto== command.CodigoProducto);
            if (producto != null) {
                return new CrearProductosResponse($"El producto ya exite");
            }
            IReadOnlyList<string> errors = command.CanCrear();
            if (errors.Any())
            {
                string ListaErrors = "Errores: " + string.Join(",", errors);
                return new CrearProductosResponse(ListaErrors);
            }
           
                Producto productoNuevo = new ProductoSimple(
                                                command.CodigoProducto,
                                                command.Nombre,
                                                command.Descripcion,
                                                command.Costo,
                                                command.Precio,
                                                command.Categoria,
                                                command.Presentacion,
                                                command.MinimoStock
                                                );


                _productoRepository.Add(productoNuevo);
                await _unitOfWork.CommitAsync();
                return new CrearProductosResponse("Se creó con éxito el producto."); 
          
        }
       

        
       
    }
}