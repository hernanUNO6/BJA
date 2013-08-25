using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class RedSaludConfiguration : EntityTypeConfiguration<RedSalud>
    {
        public RedSaludConfiguration()
        {
            ToTable("RedesSalud");
            HasKey(d => d.Id);
            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.IdSesion).IsRequired();
            Property(d => d.FechaRegistro).IsRequired();
            Property(d => d.FechaUltimaTransaccion).IsRequired();

            Property(d => d.Codigo).IsRequired().HasMaxLength(4);
            Property(d => d.Nombre).IsRequired().HasMaxLength(100);

            HasRequired(r => r.Municipio).WithMany(r => r.RedesSalud).HasForeignKey(r => r.IdMunicipio);
        }
    }
}
