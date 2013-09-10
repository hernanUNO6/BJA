using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bja.Entidades
{
    public class Tutor
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstadoSincronizacion { get; set; }

        public String Nombres { get; set; }
        public String PrimerApellido { get; set; }
        public String SegundoApellido { get; set; }
        public String TercerApellido { get; set; }
        public String NombreCompleto { get; set; }
        public String DocumentoIdentidad { get; set; }
        public TipoDocumentoIdentidad TipoDocumentoIdentidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public long IdDepartamento { get; set; }
        public Departamento Departamento { get; set; }
        public long IdProvincia { get; set; }
        public Provincia Provincia { get; set; }
        public long IdMunicipio { get; set; }
        public Municipio Municipio { get; set; }
        public String LocalidadNacimiento { get; set; }
        public bool Defuncion { get; set; }
        public String Observaciones { get; set; }
        public String Sexo { get; set; }

        [XmlIgnore]
        public List<Envio> Envios { get; set; }
    }
}
