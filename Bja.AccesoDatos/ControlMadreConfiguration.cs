using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class ControlMadreConfiguration : EntityTypeConfiguration<ControlMadre>
    {
        public ControlMadreConfiguration()
        {
            ToTable("ControlMadres");
            HasKey(c => c.Id);
            Property(c => c.IdSesion).IsRequired();
            Property(c => c.FechaRegistro).IsRequired();
            Property(c => c.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(c => c.IdCorresponsabilidadMadre).IsRequired();
            Property(c => c.IdMedico).IsRequired();
            Property(c => c.IdMadre).IsRequired();
            Property(c => c.IdTutor).IsOptional();
            Property(c => c.IdTipoParentesco).IsOptional();
            Property(c => c.IdEstablecimientoSalud).IsRequired();
            Property(c => c.FechaProgramada).IsRequired();
            Property(c => c.FechaControl).IsRequired();
            Property(c => c.TallaCm).IsRequired();
            Property(c => c.PesoKg).IsRequired();
            Property(c => c.TipoControlMadre).IsRequired();
            Property(c => c.NumeroControl).IsRequired();
            Property(c => c.EstadoPago).IsRequired();
            Property(c => c.Observaciones).IsOptional().HasMaxLength(1024);
            Property(c => c.TipoBeneficiario).IsRequired();

            //relaciones
            HasRequired(c => c.CorresponsabilidadMadre).WithMany(cm => cm.ControlesMadre).HasForeignKey(c => c.IdCorresponsabilidadMadre);
            HasRequired(c => c.Medico).WithMany().HasForeignKey(c => c.IdMedico).WillCascadeOnDelete(false);
            HasRequired(c => c.Madre).WithMany().HasForeignKey(c => c.IdMadre).WillCascadeOnDelete(false);
            HasOptional(c => c.Tutor).WithMany().HasForeignKey(c => c.IdTutor).WillCascadeOnDelete(false);
            HasOptional(c => c.TipoParentesco).WithMany().HasForeignKey(c => c.IdTipoParentesco).WillCascadeOnDelete(false);
            HasRequired(c => c.EstablecimientoSalud).WithMany().HasForeignKey(c => c.IdEstablecimientoSalud).WillCascadeOnDelete(false);
        }
    }
}
