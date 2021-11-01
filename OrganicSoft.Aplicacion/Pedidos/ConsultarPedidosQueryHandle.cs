using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Pedidos
{
    public class ConsultarPedidosQueryHandle
    {
        private IPedidoRepository _pedidoRepository;
        public ConsultarPedidosQueryHandle(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public ConsultarPedidosQueryResponse Handle()
        {
            var pedidos = _pedidoRepository.GetAll().Select(t => new PedidoViewModel
            {
                Id = t.Id,
                CodigoPedido = t.CodigoPedido,
                Estado = t.Estado,
            }).ToList();
            return new ConsultarPedidosQueryResponse(pedidos);
        }
    }

    public class ConsultarPedidosQueryResponse
    {
        public ConsultarPedidosQueryResponse(List<PedidoViewModel> pedidos)
        {
            Pedidos = pedidos;
        }
        public List<PedidoViewModel> Pedidos { get; set; }
    }

    public class PedidoViewModel
    {
        public int Id { get;  set; }
        public int CodigoPedido { get;  set; }
        public String Estado { get;  set; }

    }
}
