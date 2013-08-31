using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class TutorConfiguration : EntityTypeConfiguration<Tutor>
    {
        public TutorConfiguration()
        {
            ToTable("Tutores");
            HasKey(t => t.Id);
            Property(t => t.IdSesion).IsRequired();
            Property(t => t.FechaRegistro).IsRequired();
            Property(t => t.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(t => t.Nombres).IsRequired().HasMaxLength(100);
            Property(t => t.PrimerApellido).IsRequired().HasMaxLength(100);
            Property(t => t.SegundoApellido).HasMaxLength(100);
            Property(t => t.TercerApellido).HasMaxLength(100);
            Property(m => m.NombreCompleto).IsRequired().HasMaxLength(100);
            Property(t => t.DocumentoIdentidad).IsRequired().HasMaxLength(15);
            Property(t => t.TipoDocumentoIdentidad).IsRequired();
            Property(t => t.FechaNacimiento).IsRequired();
            Property(t => t.LocalidadNacimiento).IsRequired();
            Property(m => m.IdDepartamento).IsRequired();
            Property(m => m.IdProvincia).IsRequired();
            Property(m => m.IdMunicipio).IsRequired();
            Property(t => t.Defuncion).IsRequired();
            Property(t => t.Sexo).HasMaxLength(1).IsRequired();
            Property(c => c.Observaciones).IsOptional().HasMaxLength(1024);

            //relations
            HasRequired(m => m.Departamento).WithMany().HasForeignKey(m => m.IdDepartamento).WillCascadeOnDelete(false);
            HasRequired(m => m.Provincia).WithMany().HasForeignKey(m => m.IdProvincia).WillCascadeOnDelete(false);
            HasRequired(m => m.Municipio).WithMany().HasForeignKey(m => m.IdMunicipio).WillCascadeOnDelete(false);

        }
    }
}
