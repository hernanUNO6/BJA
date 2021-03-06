﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    [MetadataType(typeof(ProvinciaMetaData))]
    public class Provincia
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }
        public TipoEstadoSincronizacion EstadoSincronizacion { get; set; }
        public String DescripcionEstadoSincronizacion { get; set; }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public long IdDepartamento { get; set; }
        public Departamento Departamento { get; set; }

        public List<Municipio> Municipios { get; set; }
    }
}
