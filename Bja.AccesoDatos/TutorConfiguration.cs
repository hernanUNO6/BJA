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

            Property(t => t.Nombres).IsRequired().HasMaxLength(100);
            Property(t => t.PrimerApellido).IsRequired().HasMaxLength(100);
            Property(t => t.SegundoApellido).HasMaxLength(100);
            Property(t => t.TercerApellido).HasMaxLength(100);
            Property(m => m.NombreCompleto).IsRequired().HasMaxLength(100);
            Property(t => t.DocumentoIdentidad).IsRequired().HasMaxLength(15);
            Property(t => t.TipoDocumentoIdentidad).IsRequired();
            Property(t => t.FechaNacimiento).IsRequired();
            Property(t => t.IdLocalidadNacimiento).IsRequired();
            Property(m => m.IdDepartamento).IsRequired();
            Property(m => m.IdProvincia).IsRequired();
            Property(m => m.IdMunicipio).IsRequired();
            Property(t => t.Defuncion).IsRequired();
            Property(t => t.Sexo).HasMaxLength(1).IsRequired();
            Property(t => t.Observaciones).HasMaxLength(1024);
        }
    }
}
