﻿using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bja.AccesoDatos
{
    public class DepartamentoConfiguration : EntityTypeConfiguration<Departamento>
    {
        public DepartamentoConfiguration()
        {
            ToTable("Departamentos");
            HasKey(d => d.Id);
            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.IdSesion).IsRequired();
            Property(d => d.FechaRegistro).IsRequired();
            Property(d => d.FechaUltimaTransaccion).IsRequired();

            Property(d => d.Codigo).IsRequired().HasMaxLength(4);
            Property(d => d.Descripcion).IsRequired().HasMaxLength(30);
        }
    }
}