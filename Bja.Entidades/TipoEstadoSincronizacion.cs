using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public enum  TipoEstadoSincronizacion
    {
        Pendiente = 0,//1
        Generado,//2
        Recepcionado,//3
        Depurado,//4.1
        Consolidado,//4.2
        Pagado,//5.1
        PagoObservado//5.2
    }
}
