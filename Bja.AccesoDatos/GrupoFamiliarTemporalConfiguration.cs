﻿using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class GrupoFamiliarTemporalConfiguration : EntityTypeConfiguration<GrupoFamiliarTemporal>
    {
        public GrupoFamiliarTemporalConfiguration()
        {
            ToTable("GruposFamiliaresTemporal");
            HasKey(m => m.IdTemporal);
            Property(m => m.UltimoRegistro).IsRequired();

            HasKey(m => m.Id);
            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(m => m.IdFamilia).IsRequired();
            Property(m => m.IdReferencial).IsRequired();
            Property(m => m.TipoGrupoFamiliar).IsRequired();

            HasRequired(m => m.Familia).WithMany().HasForeignKey(m => m.IdFamilia).WillCascadeOnDelete(false);
        }
    }
}
