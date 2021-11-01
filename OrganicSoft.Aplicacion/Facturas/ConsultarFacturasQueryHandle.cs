using OrganicSoft.Dominio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Aplicacion.Facturas
{
    public class ConsultarFacturasQueryHandle
    {
        private IFacturaRepository _facturaRepository;
        public ConsultarFacturasQueryHandle(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }
        public ConsultarFacturasQueryResponse Handle()
        {
            var facturas = _facturaRepository.GetAll().Select(t => new FacturaViewModel
            {
                Id = t.Id,
                FechaCreacion = t.FechaCreacion,
                CedulaCliente = t.CedulaCliente,
                TotalPagar = t.TotalPagar,
            }).ToList();
            return new ConsultarFacturasQueryResponse(facturas);
        }
    }

    public class ConsultarFacturasQueryResponse
    {
        public ConsultarFacturasQueryResponse(List<FacturaViewModel> facturas)
        {
            Facturas = facturas;
        }
        public List<FacturaViewModel> Facturas { get; set; }
    }

    public class FacturaViewModel
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CedulaCliente { get; set; }
        public double TotalPagar { get; set; }

    }
}
