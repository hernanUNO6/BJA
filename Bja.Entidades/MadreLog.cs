using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class MadreLog //: Madre
    {
        public long IdLog { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstado { get; set; }
        public bool UltimoRegistro { get; set; }

        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }

        public String Nombres { get; set; }
        public String PrimerApellido { get; set; }
        public String SegundoApellido { get; set; }
        public String TercerApellido { get; set; }
        public String DocumentoIdentidad { get; set; }
        public long IdTipoDocumentoIdentidad { get; set; }
        public TipoDocumentoIdentidad TipoDocumentoIdentidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public long IdDepartamento { get; set; } //solo referencial con la tabla Departamento
        public long IdProvincia { get; set; } //solo referencial con la tabla Provincia
        public long IdMunicipio { get; set; } //solo referencial con la tabla Municipio
        public String LocalidadNacimiento { get; set; }
        public bool Defuncion { get; set; }
        public String Observaciones { get; set; }
    }
}
