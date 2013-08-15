using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public enum TipoEstadoPago
    {
        Pagado,
        NoPagado,
        Observado,
        NoAplicable, //Cuando se trata de parto, pues se crea el registro de control respectivo
        NoAsignable  //Cuando la madre o menor pierde el derecho de cobrar por la no asistencia a un control
    }
}
