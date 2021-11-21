using OrganicSoft.Aplicacion.ProductoFactory;
using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<CrearProductosResponse> Handle(CrearProductosCommand command)
        {
            Producto producto = _productoRepository.FindFirstOrDefault(t => t.Id == command.Id);
            if (producto != null) {
                return new CrearProductosResponse($"El producto ya exite");
            }
            IReadOnlyList<string> errors = command.CanCrear();
            if (errors.Any())
            {
                string ListaErrors = "Errores: " + string.Join(",", errors);
                return new CrearProductosResponse(ListaErrors);
            }
           
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
                await _unitOfWork.CommitAsync();
                return new CrearProductosResponse("Se creó con éxito el producto."); 
          
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

            public IReadOnlyList<string> CanCrear()
            {
                var errors = new List<string>();

                if ((CodigoProducto==0))
                    errors.Add("Codigo del producto no especificado");

                if (string.IsNullOrEmpty(Nombre))
                    errors.Add("Nombre del producto no especificado");
                if (string.IsNullOrEmpty(Descripcion))
                    errors.Add("Descripcion del producto no especificado");
                if (string.IsNullOrEmpty(Categoria))
                    errors.Add("Categoria del producto no especificado");
                if (string.IsNullOrEmpty(Presentacion))
                    errors.Add("Presenctacion del producto no especificado");
                if (Precio == 0)
                    errors.Add("Precio del producto no especificado");
                return errors;
            }
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
                return this.Mensaje.Equals("Se creó con éxito el producto.");
            }
        }
    }
}
