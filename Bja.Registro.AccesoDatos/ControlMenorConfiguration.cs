﻿using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Registro.AccesoDatos
{
    public class ControlMenorConfiguration : EntityTypeConfiguration<ControlMenor>
    {
        public ControlMenorConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.IdSesion).IsRequired();
            Property(c => c.FechaRegistro).IsRequired();
            Property(c => c.FechaUltimaTransaccion).IsRequired();

            Property(c => c.IdCorresponsabilidadMenor).IsRequired();
            Property(c => c.IdMedico).IsRequired();
            Property(c => c.IdTutor).IsRequired();
            Property(c => c.FechaProgramada).IsRequired();
            Property(c => c.FechaControl).IsRequired();
            Property(c => c.TallaCm).IsRequired();
            Property(c => c.PesoKg).IsRequired();
            Property(c => c.NumeroControl).IsRequired();
            Property(c => c.EstadoPago).IsRequired();
            Property(c => c.Observaciones).HasMaxLength(1024);
        }
    }
}