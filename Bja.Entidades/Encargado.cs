﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    [MetadataType(typeof(EncargadoMetaData))]
    public class Encargado
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstadoSincronizacion { get; set; }

        public long IdTipoEncargado { get; set; }
        public TipoEncargado TipoEncargado { get; set; }
        public String Nombres { get; set; }
        public String PrimerApellido { get; set; }
        public String SegundoApellido { get; set; }
        public String TercerApellido { get; set; }
        public String DocumentoIdentidad { get; set; }
        public TipoDocumentoIdentidad TipoDocumentoIdentidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public String Sexo { get; set; } //char(1)
        public String LocalidadNacimiento { get; set; }
        public String Observaciones { get; set; }
        
    }
}
