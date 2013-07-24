﻿using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class RecepcionConfiguration : EntityTypeConfiguration<Recepcion>
    {
        public RecepcionConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.IdSesion).IsRequired();
            Property(c => c.FechaRegistro).IsRequired();
            Property(c => c.FechaUltimaTransaccion).IsRequired();

            Property(c => c.IdEstablecimientoMedico).IsRequired();
            Property(c => c.IdMedico).IsRequired();
            Property(c => c.FechaEnvio).IsRequired();
            Property(c => c.CodigoVerificacion).IsRequired().HasMaxLength(128);
            Property(c => c.TotalNuevasMadres).IsRequired();
            Property(c => c.TotalNuevosTutores).IsRequired();
            Property(c => c.TotalNuevosMenores).IsRequired();
            Property(c => c.TotalNuevosControlMadre).IsRequired();
            Property(c => c.TotalNuevosControlMenor).IsRequired();
            Property(c => c.TotalNuevaCorresponsabilidadMadre).IsRequired();
            Property(c => c.TotalNuevaCorresponsabilidadMenor).IsRequired();
            Property(c => c.TotalModificacionMadres).IsRequired();
            Property(c => c.TotalModificacionMenores).IsRequired();
            Property(c => c.TotalModificacionTutores).IsRequired();
            Property(c => c.TotalModificacionControlMadre).IsRequired();
            Property(c => c.TotalModificacionControlMenor).IsRequired();
            Property(c => c.TotalModificacionCorresponsabilidadMadre).IsRequired();
            Property(c => c.TotalModificacionCorresponsabilidadMenor).IsRequired();
            Property(c => c.TotalBorradoMadres).IsRequired();
            Property(c => c.TotalBorradoTutores).IsRequired();
            Property(c => c.TotalBorradoMenores).IsRequired();
        }
    }
}
