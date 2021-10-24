﻿using OrganicSoft.Dominio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio
{
    public class Descuento : Entity<int>, IAggregateRoot
    {
        
        public int CodigoDescuento { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public double PorcentajeDescuento { get; private set; }

        public Descuento(int codigoDescuento, DateTime fechaInicio, DateTime fechaFin, double porcentajeDescuento)
        {
            CodigoDescuento = codigoDescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            PorcentajeDescuento = porcentajeDescuento;
        }
    }
}
