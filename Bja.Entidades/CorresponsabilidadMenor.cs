using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class CorresponsabilidadMenor
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstadoSincronizacion { get; set; }

        public long IdEstablecimientoSalud { get; set; }
        public EstablecimientoSalud EstablecimientoSalud { get; set; }
        public TipoInscripcion TipoInscripcionMenor { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public long IdMenor { get; set; }
        public Menor Menor { get; set; }
        public String DireccionMenor { get; set; }
        public long? IdMadre { get; set; }
        public Madre Madre { get; set; }
        public String DireccionMadre { get; set; }
        public long? IdTutor { get; set; }
        public Tutor Tutor { get; set; }
        public String DireccionTutor { get; set; }
        public long IdTipoParentesco { get; set; }
        public TipoParentesco TipoParentesco { get; set; }
        public String CodigoFormulario { get; set; }
        public DateTime FechaSalidaPrograma { get; set; }
        public TipoSalidaMenor TipoSalidaMenor { get; set; }       
        public String Observaciones { get; set; }
        public String AutorizadoPor { get; set; }
        public String CargoAutorizador { get; set; }

        public virtual ICollection<ControlMenor> ControlesMenor { get; set; }
    }
}
