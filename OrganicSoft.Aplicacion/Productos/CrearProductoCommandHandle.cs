using OrganicSoft.Aplicacion.ProductoFactory;
using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System.Collections.Generic;

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
        public CrearProductosResponse Handle(CrearProductosCommand command)
        {
            Producto producto = _productoRepository.FindFirstOrDefault(t => t.Id == command.Id);
            if (producto == null)
            {
               
                Producto productoNuevo = TipoProducto.CrearProducto(
                                                command.TipoProducto,
                                                command.CodigoProducto,
                                                command.Nombre,
                                                command.Descripcion,
                                                command.Precio,
                                                command.Categoria,
                                                command.Presentacion,
                                                command.MinimoStock,
                                                command.Componetes,
                                                command.Costo
                                                );


                _productoRepository.Add(productoNuevo);
                _unitOfWork.Commit();
                return new CrearProductosResponse($"Se creó con exito el producto.");
            }
            else
            {
                return new CrearProductosResponse($"El producto ya exite");
            }
        }
        public class CrearProductosCommand
        {
            public CrearProductosCommand()
            {
            }

            public CrearProductosCommand(int id, string tipoProducto, int codigoProducto, string nombre, string descripcion, double precio, string categoria, string presentacion, int minimoStock, double costo)
            {
                Id = id;
                TipoProducto = tipoProducto;
                CodigoProducto = codigoProducto;
                Nombre = nombre;
                Descripcion = descripcion;
                Precio = precio;
                Categoria = categoria;
                Presentacion = presentacion;
                MinimoStock = minimoStock;
                Componetes = null;
                Costo = costo;
            }

            public int Id { get;  set; }
            public string TipoProducto { get; set; }
            public int CodigoProducto { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            
            public double Precio { get; set; }
            public string Categoria { get; set; }
            public string Presentacion { get; set; }
            public int MinimoStock { get; set; }
            public List<Componente> Componetes { get; set; }
            public double Costo { get; set; }
           
        }

        public static class TipoProducto
        {
            //mejorarar la creacion de productos
            public static Producto CrearProducto(string tipoProducto, int codigo, string nombre, string decripcion, double precio, string categoria, string presentacion, int minimoStock, List<Componente> componetes, double costo)
            {
                Producto producto = (Producto)new FabricadeProductos().MetodoFabrica(tipoProducto, codigo, nombre, decripcion, precio, categoria, presentacion, minimoStock, componetes, costo);

                return producto;
            }

        }
        public record CrearProductosResponse
        {
            public CrearProductosResponse()
            {

            }

            public CrearProductosResponse(string mensaje)
            {
                Mensaje = mensaje;
            }

            public string Mensaje { get; set; }
            public bool isOk()
            {
                return this.Mensaje.Equals("Se creó con exito el producto.");
            }
        }
    }
}
