using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.Dominio.Base
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}
