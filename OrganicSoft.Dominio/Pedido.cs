using OrganicSoft.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public class Pedido : Entity<int>, IAggregateRoot
    {
        public int CodigoPedido { get; private set; }
        public String Estado { get; private set; }
        public CarritoCompra Carrito { get; private set; }

       // Encargos encargos = new Encargos();

        public Pedido()
        {

        }

       // public IReadOnlyCollection<Pedido> Pedidos => encargos.Pedidos.AsReadOnly();

        public String GenerarPedido(int codigo, CarritoCompra CarritoCompra)
        {
            if (CarritoCompra != null)
            {
                CodigoPedido = codigo;
                Estado = "NO CONFIRMADO";
                Carrito = CarritoCompra;
               // encargos.Pedidos.Add(this);
                return $"Se creó un nuevo pedido para el cliente con cédula {CarritoCompra.CedulaCliente}";
            }
            throw new NotImplementedException();
        }

        public String ConfirmarPedido()
        {
            Estado = "CONFIRMADO";
            return $"El nuevo estado del pedido es {Estado}";
        }
    }

    public class Encargos
    {
        public List<Pedido> Pedidos { get; private set; } 

        public Encargos()
        {
            Pedidos = new List<Pedido>();
        }
    }
}
