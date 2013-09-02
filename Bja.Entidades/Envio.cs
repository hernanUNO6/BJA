using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class Envio
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
        public long IdMedico { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaEnvio { get; set; }
        public String CodigoVerificacion { get; set; } //md5, sha
        public int TotalNuevasMadres { get; set; }
        public int TotalNuevosTutores { get; set; }
        public int TotalNuevosMenores { get; set; }
        public int TotalNuevosControlMadre { get; set; }
        public int TotalNuevosControlMenor { get; set; }
        public int TotalNuevaCorresponsabilidadMenor { get; set; }
        public int TotalNuevaCorresponsabilidadMadre { get; set; }
        public int TotalModificacionMadres { get; set; }
        public int TotalModificacionTutores { get; set; }
        public int TotalModificacionMenores { get; set; }
        public int TotalModificacionControlMenor { get; set; }
        public int TotalModificacionControlMadre { get; set; }
        public int TotalModificacionCorresponsabilidadMenor { get; set; }
        public int TotalModificacionCorresponsabilidadMadre { get; set; }
        public int TotalBorradoMadres { get; set; }
        public int TotalBorradoTutores { get; set; }
        public int TotalBorradoMenores { get; set; }

        public List<Madre> NuevasMadres { get; set; }
        public List<Madre> MadresModificadas { get; set; }

        public List<Tutor> NuevosTutores { get; set; }
        public List<Menor> NuevosMenores { get; set; }
        public List<ControlMadre> NuevosControlesMadre { get; set; }
        public List<ControlMenor> NuevosControlesMenor { get; set; }

    }
}
