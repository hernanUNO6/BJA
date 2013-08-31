using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class EncargadoLogConfiguration : EntityTypeConfiguration<EncargadoLog>
    {
        public EncargadoLogConfiguration()
        {
            ToTable("EncargadosLog");
            HasKey(m => m.IdLog);
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstado).IsRequired().HasMaxLength(512);
            Property(m => m.UltimoRegistro).IsRequired();

            Property(c => c.Id).IsRequired();
            Property(e => e.IdSesion).IsRequired();
            Property(e => e.FechaRegistro).IsRequired();
            Property(e => e.FechaUltimaTransaccion).IsRequired();

            Property(e => e.Nombres).IsRequired().HasMaxLength(100);
            Property(e => e.PrimerApellido).IsRequired().HasMaxLength(100);
            Property(e => e.SegundoApellido).HasMaxLength(100);
            Property(e => e.TercerApellido).HasMaxLength(100);
            Property(e => e.DocumentoIdentidad).IsRequired().HasMaxLength(15);
            Property(e => e.TipoDocumentoIdentidad).IsRequired();
            Property(e => e.FechaNacimiento).IsRequired();
            Property(e => e.LocalidadNacimiento).IsRequired();
            Property(e => e.Observaciones).HasMaxLength(1024);
        }
    }
}
