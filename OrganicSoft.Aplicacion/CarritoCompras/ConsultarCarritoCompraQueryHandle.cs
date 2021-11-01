using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.CarritoCompras
{
    public class ConsultarCarritoCompraQueryHandle
    {
        private ICarritoCompraRepository _carritoCompraRepository;
        public ConsultarCarritoCompraQueryHandle(ICarritoCompraRepository carritoCompraRepository)
        {
            _carritoCompraRepository = carritoCompraRepository;
        }
        public ConsultarCarritoCompraQueryResponse Handle()
        {
            var carritos = _carritoCompraRepository.GetAll().Select(t => new CarritoCompraViewModel
            {
                Id = t.Id,
                CedulaCliente = t.CedulaCliente,
                Codigo = t.Codigo,
            }).ToList();
            return new ConsultarCarritoCompraQueryResponse(carritos);
        }
    }

    public class ConsultarCarritoCompraQueryResponse
    {
        public ConsultarCarritoCompraQueryResponse(List<CarritoCompraViewModel> carritos)
        {
            Carritos = carritos;
        }
        public List<CarritoCompraViewModel> Carritos { get; set; }
    }

    public class CarritoCompraViewModel
    {
        public int Id { get; set; }
        public String CedulaCliente { get; set; }
        public int Codigo { get; set; }

    }
}
