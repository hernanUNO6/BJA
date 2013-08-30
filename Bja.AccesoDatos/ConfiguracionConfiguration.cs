using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class ConfiguracionConfiguration : EntityTypeConfiguration<Configuracion>
    {
        public ConfiguracionConfiguration()
        {
            ToTable("Configuraciones");
            HasKey(c => c.Id);
            Property(c => c.IdSesion).IsRequired();

            Property(c => c.Nombre).IsRequired().HasMaxLength(512);
            Property(c => c.Clase).IsRequired().HasMaxLength(512);
            Property(c => c.Valor).IsRequired().HasMaxLength(512);
            Property(c => c.TipoDatoValor).IsRequired();
        }
    }
}
