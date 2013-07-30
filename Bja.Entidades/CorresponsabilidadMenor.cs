﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class CorresponsabilidadMenor
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }

        public long IdEstablecimientoSalud { get; set; }
        public TipoInscripcion TipoInscripcionMenor { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public long IdMenor { get; set; }
        public Menor Menor { get; set; }
        public String DireccionMenor { get; set; }
        public long IdTutor { get; set; }
        public Tutor Tutor { get; set; }
        public String DireccionTutor { get; set; }
        public String CodigoFormulario { get; set; }
        public DateTime FechaSalidaPrograma { get; set; }
        public TipoSalidaMenor TipoSalidaMenor { get; set; }       
        public String Observaciones { get; set; }
        public String AutorizadoPor { get; set; }
        public String CargoAutorizador { get; set; }
    }
}
