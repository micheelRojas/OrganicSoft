using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public class Pedido
    {
        public int Codigo { get; private set; }
        public String Estado { get; private set; }
        public CarritoCompra Carrito { get; private set; }
        protected List<Pedido> _pedidos = new List<Pedido>();

        public Pedido()
        {

        }

        public IReadOnlyCollection<Pedido> Pedidos => _pedidos.AsReadOnly();

        public String GenerarPedido(int codigo, CarritoCompra CarritoCompra)
        {
            if (CarritoCompra != null)
            {
                Codigo = codigo;
                Estado = "NO CONFIRMADO";
                Carrito = CarritoCompra;
                _pedidos.Add(this);
                return $"Se creó un nuevo pedido para el cliente con cédula {CarritoCompra.CedulaCliente}";
            }
            throw new NotImplementedException();
        }

        public String ConfirmarPedido(int codigo)
        {
            foreach (Pedido pedido in Pedidos)
            {
                if (pedido.Carrito.Codigo.Equals(codigo))
                {
                    pedido.Estado = "CONFIRMADO";
                    return $"El nuevo estado del pedido es {pedido.Estado}";
                }
            }
            throw new NotImplementedException();
        }
    }
}
