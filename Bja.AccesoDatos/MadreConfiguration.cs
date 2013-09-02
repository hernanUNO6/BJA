using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class MadreConfiguration : EntityTypeConfiguration<Madre>
    {
        public MadreConfiguration()
        {
            ToTable("Madres");
            HasKey(m => m.Id);
            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(m => m.Nombres).IsRequired().HasMaxLength(100);
            Property(m => m.PrimerApellido).IsRequired().HasMaxLength(100);
            Property(m => m.SegundoApellido).HasMaxLength(100);
            Property(m => m.TercerApellido).HasMaxLength(100);
            Property(m => m.NombreCompleto).IsRequired().HasMaxLength(100);
            Property(m => m.DocumentoIdentidad).IsRequired().HasMaxLength(15);
            Property(m => m.TipoDocumentoIdentidad).IsRequired();
            Property(m => m.FechaNacimiento).IsRequired();
            Property(m => m.IdDepartamento).IsRequired();
            Property(m => m.IdProvincia).IsRequired();
            Property(m => m.IdMunicipio).IsRequired();
            Property(m => m.LocalidadNacimiento).IsRequired().HasMaxLength(100);
            Property(m => m.Defuncion).IsRequired();
            Property(c => c.Observaciones).IsOptional().HasMaxLength(1024);

            //relations
            HasRequired(m => m.Departamento).WithMany().HasForeignKey(m => m.IdDepartamento).WillCascadeOnDelete(false);
            HasRequired(m => m.Provincia).WithMany().HasForeignKey(m => m.IdProvincia).WillCascadeOnDelete(false);
            HasRequired(m => m.Municipio).WithMany().HasForeignKey(m => m.IdMunicipio).WillCascadeOnDelete(false);

            
        }
    }
}
