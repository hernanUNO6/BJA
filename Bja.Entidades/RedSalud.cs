using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class RedSalud
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }

        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public long IdMunicipio { get; set; }
        public virtual Municipio Municipio { get; set; }

        public virtual ICollection<EstablecimientoSalud> EstablecimientosSalud { get; set; }
    }
}
