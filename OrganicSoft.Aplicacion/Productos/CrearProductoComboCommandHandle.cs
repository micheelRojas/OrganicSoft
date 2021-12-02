using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Productos
{
    public class CrearProductoComboCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;
        public CrearProductoComboCommandHandle(
           IUnitOfWork unitOfWork,
           IProductoRepository productoRepository
       )
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;

        }
        public async Task<CrearProductosResponse> Handle(CrearProductoComboCommand command)
        {
            Producto producto = _productoRepository.FindFirstOrDefault(t => t.Id == command.Id || t.CodigoProducto == command.CodigoProducto);
            if (producto != null)
            {
                return new CrearProductosResponse($"El producto ya existe");
            }
            IReadOnlyList<string> errors = command.CanCrear();
            if (errors.Any())
            {
                string ListaErrors = "Errores: " + string.Join(",", errors);
                return new CrearProductosResponse(ListaErrors);
            }

            var componentes = new List<Componente>();
            componentes = command.Componentes.Select(
                c => new Componente
                (
                    _productoRepository.FindFirstOrDefault(x => x.Id == c.Producto.Id),
                    c.Cantidad
                )).ToList();
            Producto productoNuevo = new ProductoCombo(
                                            command.CodigoProducto,
                                            command.Nombre,
                                            command.Descripcion,
                                            command.Precio,
                                            command.Categoria,
                                            command.Presentacion,
                                            command.MinimoStock,
                                            componentes
                                            );


            _productoRepository.Add(productoNuevo);
            await _unitOfWork.CommitAsync();
            return new CrearProductosResponse("Se creó con éxito el producto.");

        }

    }
}
