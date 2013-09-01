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
    public class TipoParentescoConfiguration : EntityTypeConfiguration<TipoParentesco>
    {
        public TipoParentescoConfiguration()
        {
            ToTable("TiposParentescos");
            HasKey(p => p.Id);
            Property(p => p.IdSesion).IsRequired();
            Property(p => p.FechaRegistro).IsRequired();
            Property(p => p.FechaUltimaTransaccion).IsRequired();
            Property(p => p.EstadoRegistro).IsRequired();
            Property(p => p.Descripcion).IsRequired().HasMaxLength(100);
        }
    }
}
