using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class GrupoFamiliar
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstadoSincronizacion { get; set; }

        public long IdFamilia { get; set; }
        public Familia Familia { get; set; }
        public long? IdMenor { get; set; }
        public long? IdMadre { get; set; }
        public long? IdTutor { get; set; }
        public long? IdTipoParentesco { get; set; }
        public TipoGrupoFamiliar TipoGrupoFamiliar { get; set; } // Tipo: Madre, Menor, Tutor
        public bool? TitularPagoVigente { get; set; }
    }
}
