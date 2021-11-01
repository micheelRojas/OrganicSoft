using OrganicSoft.Dominio;
using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Facturas
{
    public class GenerarFacturaCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFacturaRepository _facturaRepository;
        public GenerarFacturaCommandHandle(IUnitOfWork unitOfWork, IFacturaRepository facturaRepository)
        {
            _unitOfWork = unitOfWork;
            _facturaRepository = facturaRepository;
        }
        public GenerarFacturaResponse Handle(GenerarFacturaCommand command)
        {
            Factura factura = _facturaRepository.FindFirstOrDefault(t => t.Id == command.Id);
            if (factura == null)
            {
                Factura facturaNueva = new Factura(
                    command.Codigo, DateTime.Now, command.CedulaCliente, command.Pedido
                    );

                _facturaRepository.Add(facturaNueva);
                _unitOfWork.Commit();
                return new GenerarFacturaResponse($"Se creó con exito la factura.");
            }
            else
            {
                return new GenerarFacturaResponse($"La factura ya exite");
            }
        }
    }

    public class GenerarFacturaCommand
    {
        public int Id { get; set; }
        public int Codigo { get;  set; }
        public String CedulaCliente { get;  set; }
        public Pedido Pedido { get;  set; }


    }

    public class GenerarFacturaResponse
    {
        public GenerarFacturaResponse()
        {

        }

        public GenerarFacturaResponse(string mensaje)
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
