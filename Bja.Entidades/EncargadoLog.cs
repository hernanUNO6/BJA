using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class EncargadoLog //: Encargado
    {
        public long IdLog { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstado { get; set; }
        public bool UltimoRegistro { get; set; }

        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }

        public long IdTipoEncargado { get; set; }
        public TipoEncargado TipoEncargado { get; set; }
        public String Nombres { get; set; }
        public String PrimerApellido { get; set; }
        public String SegundoApellido { get; set; }
        public String TercerApellido { get; set; }
        public String DocumentoIdentidad { get; set; }
        public long IdTipoDocumentoIdentidad { get; set; }
        public TipoDocumentoIdentidad TipoDocumentoIdentidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public String Sexo { get; set; } //char(1)
        public String IdLocalidadNacimiento { get; set; }
        public String Observaciones { get; set; }
        public long IdTipoEstadoRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }
         
    }
}
