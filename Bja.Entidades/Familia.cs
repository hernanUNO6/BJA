using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class Familia
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstadoSincronizacion { get; set; }

        public String PrimerApellidoFamilia { get; set; }
        public String SegundoApellidoFamilia { get; set; }
        public long IdDepartamento { get; set; } //solo referencial con la tabla Departamento
        public Departamento Departamento { get; set; }
        public long IdProvincia { get; set; } //solo referencial con la tabla Provincia
        public Provincia Provincia { get; set; }
        public long IdMunicipio { get; set; } //solo referencial con la tabla Municipio
        public Municipio Municipio { get; set; }
        public String LocalidadFamilia { get; set; }
        
    }
}
