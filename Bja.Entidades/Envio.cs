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

        //lista de registros con cambios
        public List<Madre> Madres { get; set; }
        public List<Tutor> Tutores { get; set; }
        public List<Menor> Menores { get; set; }
        public List<CorresponsabilidadMadre> CorresponsabilidadMadres { get; set; }
        public List<ControlMadre> ControlMadres { get; set; }
        public List<CorresponsabilidadMenor> CorresponsabilidadMenores { get; set; }
        public List<ControlMenor> ControlMenores { get; set; }
    }
}
