using OrganicSoft.Dominio.Contracts;

namespace OrganicSoft.Aplicacion
{
    public class EntradadeProductosCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IProductoRepository _productoRepository;
        public EntradadeProductosCommandHandle(IUnitOfWork unitOfWork, IProductoRepository productoRepository)
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;
        }
        public EntradadeProductosResponse Handle(EntradadeProductosCommand command)
        {
            
                var producto= _productoRepository.FindFirstOrDefault(producto => producto.Id == command.Id);//infraestructura-datos
                if (producto == null) return new EntradadeProductosResponse("el producto no existe");
                var response = producto.EntradaProductos(command.Cantidad);//domain
                _productoRepository.Update(producto);//proyectarse el cambio y registrarlo en la unidad de trabajo
                _unitOfWork.Commit();//infraestructura-datos
               
                return new EntradadeProductosResponse(response);
        }
        public class EntradadeProductosCommand
        {
            public int Id { get; set; }
            public int Cantidad{ get; set; }
        }
       
        //public record EntradadeProductosRequest(int id, int cantidad);
        public record EntradadeProductosResponse
        {
            public EntradadeProductosResponse()
            {

            }

            public EntradadeProductosResponse(string mensaje)
            {
                Mensaje = mensaje;
            }

            public string Mensaje { get; set; }
            public bool isOk()
            {
                return this.Mensaje!=("La cantidad debe ser mayor a cero");
            }
        }
    }
}
