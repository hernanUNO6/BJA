using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class CorresponsabilidadMenorConfiguration : EntityTypeConfiguration<CorresponsabilidadMenor>
    {
        public CorresponsabilidadMenorConfiguration()
        {
            ToTable("CorresponsabilidadMenores");
            HasKey(c => c.Id);
            Property(c => c.IdSesion).IsRequired();
            Property(c => c.FechaRegistro).IsRequired();
            Property(c => c.FechaUltimaTransaccion).IsRequired();

            Property(c => c.IdEstablecimientoSalud).IsRequired();
            Property(c => c.TipoInscripcionMenor).IsRequired();
            Property(c => c.FechaInscripcion).IsRequired();
            Property(c => c.IdMenor).IsRequired();
            Property(c => c.DireccionMenor).IsOptional().HasMaxLength(512);
            Property(c => c.IdMadre).IsOptional();
            Property(c => c.DireccionMadre).IsOptional().HasMaxLength(512);
            Property(c => c.IdTutor).IsOptional();
            Property(c => c.DireccionTutor).IsOptional().HasMaxLength(512);
            Property(c => c.CodigoFormulario).IsRequired().HasMaxLength(16);
            Property(c => c.FechaSalidaPrograma).IsOptional();
            Property(c => c.TipoSalidaMenor).IsOptional();
            Property(c => c.AutorizadoPor).HasMaxLength(256);
            Property(c => c.CargoAutorizador).HasMaxLength(256);
            Property(c => c.Observaciones).HasMaxLength(1024);
        }
    }
}
