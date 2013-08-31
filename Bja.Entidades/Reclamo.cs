using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    [MetadataType(typeof(ReclamoMetaData))]
    public class Reclamo
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstadoSincronizacion { get; set; }

        public DateTime FechaReclamo { get; set; }
        public long IdTipoReclamo { get; set; }
        public TipoReclamo TipoReclamo { get; set; }
        public String NombreBeneficiario { get; set; }
        public String DetalleReclamo { get; set; }
    }
}
