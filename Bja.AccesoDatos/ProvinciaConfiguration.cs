using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bja.AccesoDatos
{
    public class ProvinciaConfiguration : EntityTypeConfiguration<Provincia>
    {
        public ProvinciaConfiguration()
        {
            ToTable("Provincias");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.IdSesion).IsRequired();
            Property(p => p.FechaRegistro).IsRequired();
            Property(p => p.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(p => p.Codigo).IsRequired().HasMaxLength(4);
            Property(p => p.Descripcion).IsRequired().HasMaxLength(100);
            Property(p => p.IdDepartamento).IsRequired();

            HasRequired(p => p.Departamento)
                .WithMany(p => p.Provincias)
                .HasForeignKey(p => p.IdDepartamento).WillCascadeOnDelete(false);
        }
    }
}
