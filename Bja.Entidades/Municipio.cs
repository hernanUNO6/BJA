using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class Municipio
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public long IdProvincia { get; set; }
        public virtual Provincia Provincia { get; set; }

        public virtual ICollection<RedSalud> RedesSalud { get; set; }
    }
}
