using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{

    public class MadreLogConfiguration : EntityTypeConfiguration<MadreLog>
    {
        public MadreLogConfiguration()
        {
            ToTable("MadresLog");
            HasKey(m => m.IdLog);
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstado).IsRequired().HasMaxLength(512);
            Property(m => m.UltimoRegistro).IsRequired();

            Property(m => m.Id).IsRequired();
            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();

            Property(m => m.Nombres).IsRequired().HasMaxLength(100);
            Property(m => m.PrimerApellido).IsRequired().HasMaxLength(100);
            Property(m => m.SegundoApellido).HasMaxLength(100);
            Property(m => m.TercerApellido).HasMaxLength(100);
            Property(m => m.DocumentoIdentidad).IsRequired().HasMaxLength(15);
            Property(m => m.TipoDocumentoIdentidad).IsRequired();
            Property(m => m.FechaNacimiento).IsRequired();
            Property(m => m.IdDepartamento).IsRequired();
            Property(m => m.IdProvincia).IsRequired();
            Property(m => m.IdMunicipio).IsRequired();
            Property(m => m.LocalidadNacimiento).IsRequired();
            Property(m => m.Defuncion).IsRequired();
            Property(c => c.Observaciones).IsOptional().HasMaxLength(1024);
        }
    }
}
