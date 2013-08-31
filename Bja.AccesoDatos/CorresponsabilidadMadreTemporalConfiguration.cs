using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class CorresponsabilidadMadreTemporalConfiguration : EntityTypeConfiguration<CorresponsabilidadMadreTemporal>
    {
        public CorresponsabilidadMadreTemporalConfiguration()
        {
            ToTable("CorresponsabilidadMadresTemporal");
            HasKey(m => m.IdTemporal);
            Property(m => m.UltimoRegistro).IsRequired();


            Property(c => c.Id).IsRequired();
            Property(c => c.IdSesion).IsRequired();
            Property(c => c.FechaRegistro).IsRequired();
            Property(c => c.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(c => c.IdEstablecimientoSalud).IsRequired();
            Property(c => c.TipoInscripcionMadre).IsRequired();
            Property(c => c.FechaInscripcion).IsRequired();
            Property(c => c.IdMadre).IsRequired();
            Property(c => c.DireccionMadre).IsOptional().HasMaxLength(512);
            Property(c => c.IdTutor).IsOptional();
            Property(c => c.IdTipoParentesco).IsOptional();
            Property(c => c.DireccionTutor).IsOptional().HasMaxLength(512);
            Property(c => c.CodigoFormulario).IsRequired().HasMaxLength(16);
            Property(c => c.FechaUltimaMenstruacion).IsRequired();
            Property(c => c.FechaUltimoParto).IsRequired();
            Property(c => c.NumeroEmbarazo).IsRequired();
            Property(c => c.ARO).IsOptional();
            Property(c => c.FechaSalidaPrograma).IsOptional();
            Property(c => c.TipoSalidaMadre).IsOptional();
            Property(c => c.AutorizadoPor).HasMaxLength(256);
            Property(c => c.CargoAutorizador).HasMaxLength(256);
            Property(c => c.Observaciones).IsOptional().HasMaxLength(1024);
        }
    }
}
