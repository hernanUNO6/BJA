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
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(c => c.IdEstablecimientoSalud).IsRequired();
            Property(c => c.TipoInscripcionMenor).IsRequired();
            Property(c => c.FechaInscripcion).IsRequired();
            Property(c => c.IdMenor).IsRequired();            
            Property(c => c.IdMadre).IsOptional();
            Property(c => c.IdTutor).IsOptional();
            Property(c => c.IdTipoParentesco).IsOptional();
            Property(c => c.CodigoFormulario).IsRequired().HasMaxLength(16);
            Property(c => c.FechaSalidaPrograma).IsOptional();
            Property(c => c.TipoSalidaMenor).IsOptional();
            Property(c => c.AutorizadoPor).HasMaxLength(256);
            Property(c => c.CargoAutorizador).HasMaxLength(256);
            Property(c => c.Observaciones).IsOptional().HasMaxLength(1024);

            //relaciones
            HasRequired(c => c.EstablecimientoSalud).WithMany().HasForeignKey(c => c.IdEstablecimientoSalud).WillCascadeOnDelete(false);
            HasRequired(c => c.Menor).WithMany().HasForeignKey(c => c.IdMenor).WillCascadeOnDelete(false);
            HasOptional(c => c.Madre).WithMany().HasForeignKey(c => c.IdMadre).WillCascadeOnDelete(false);
            HasOptional(c => c.Tutor).WithMany().HasForeignKey(c => c.IdTutor).WillCascadeOnDelete(false);
            HasOptional(c => c.TipoParentesco).WithMany().HasForeignKey(c => c.IdTipoParentesco).WillCascadeOnDelete(false);
        }
    }
}
