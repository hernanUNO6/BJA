﻿using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class MedicoConfiguration : EntityTypeConfiguration<Medico>
    {
        public MedicoConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();

            Property(m => m.Nombres).IsRequired().HasMaxLength(100);
            Property(m => m.PrimerApellido).IsRequired().HasMaxLength(100);
            Property(m => m.SegundoApellido).HasMaxLength(100);
            Property(m => m.TercerApellido).HasMaxLength(100);
            Property(m => m.DocumentoIdentidad).IsRequired().HasMaxLength(15);
            Property(m => m.TipoDocumentoIdentidad).IsRequired();
            Property(m => m.MatriculaColegioMedico).IsRequired().HasMaxLength(32);
            Property(m => m.CorreoElectronico).HasMaxLength(64);
            Property(m => m.FechaNacimiento).IsRequired();
            Property(m => m.IdLocalidadNacimiento).IsRequired();
            Property(m => m.Observaciones).HasMaxLength(1024);

        }
    }
}
