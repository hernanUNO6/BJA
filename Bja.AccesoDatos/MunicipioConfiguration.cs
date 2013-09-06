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
    public class MunicipioConfiguration : EntityTypeConfiguration<Municipio>
    {
        public MunicipioConfiguration()
        {
            ToTable("Municipios");
            HasKey(m => m.Id);
            //Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(m => m.Codigo).IsRequired().HasMaxLength(4);
            Property(m => m.Descripcion).IsRequired().HasMaxLength(100);
            Property(m => m.IdProvincia).IsRequired();

            HasRequired(m => m.Provincia).WithMany(m => m.Municipios).HasForeignKey(m => m.IdProvincia).WillCascadeOnDelete(false);
        }
    }
}
