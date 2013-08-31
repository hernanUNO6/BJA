using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class Pago
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstadoSincronizacion { get; set; }

        public long IdTutor { get; set; }
        public Tutor Tutor { get; set; }
        public String NombreCompletoMadre { get; set; }
        public String NombreCompletoTutor { get; set; }
        public long IdBeneficiario { get; set; }
        public String NombreCompletoBeneficiario { get; set; }
        public String SexoTitular { get; set; } //char(1)
        public DateTime FechaNacimientoTitular { get; set; }
        public Decimal Monto { get; set; }
        public String Control { get; set; } //char(16) 

    }
}
