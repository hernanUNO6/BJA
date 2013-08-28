﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    [MetadataType(typeof(MedicoMetaData))]
    public class Medico
    {
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
        public long IdDepartamento { get; set; } 
        public Departamento Departamento { get; set; }
        public long IdProvincia { get; set; } 
        public Provincia Provincia { get; set; }
        public long IdMunicipio { get; set; } 
        public Municipio Municipio { get; set; }
        public String IdLocalidadNacimiento { get; set; }
        public String MatriculaColegioMedico { get; set; }
        public String CorreoElectronico { get; set; }
        public String Observaciones { get; set; }

        public virtual ICollection<AsignacionMedico> AsignacionesMedico { get; set; }
    }
}
