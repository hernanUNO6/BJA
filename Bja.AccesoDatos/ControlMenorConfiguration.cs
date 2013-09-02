using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class ControlMenorConfiguration : EntityTypeConfiguration<ControlMenor>
    {
        public ControlMenorConfiguration()
        {
            ToTable("ControlMenores");
            HasKey(c => c.Id);
            Property(c => c.IdSesion).IsRequired();
            Property(c => c.FechaRegistro).IsRequired();
            Property(c => c.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(c => c.IdCorresponsabilidadMenor).IsRequired();
            Property(c => c.IdMedico).IsRequired();
            Property(c => c.IdMenor).IsRequired();
            Property(c => c.IdMadre).IsOptional();
            Property(c => c.IdTutor).IsOptional();
            Property(c => c.IdTipoParentesco).IsOptional();
            Property(c => c.FechaProgramada).IsRequired();
            Property(c => c.FechaControl).IsRequired();
            Property(c => c.TallaCm).IsRequired();
            Property(c => c.PesoKg).IsRequired();
            Property(c => c.NumeroControl).IsRequired();
            Property(c => c.EstadoPago).IsRequired();
            Property(c => c.Observaciones).IsOptional().HasMaxLength(1024);
            Property(c => c.TipoBeneficiario).IsRequired();

            //relaciones Por ahora 01/09/2013 rrsc
            //HasRequired(c => c.CorresponsabilidadMenor).WithMany(c => c.ControlesMenor).HasForeignKey(c => c.IdCorresponsabilidadMenor);
            //HasRequired(c => c.Medico).WithMany().HasForeignKey(c => c.IdMedico).WillCascadeOnDelete(false);
            //HasRequired(c => c.Menor).WithMany().HasForeignKey(c => c.IdMenor).WillCascadeOnDelete(false);
            //HasOptional(c => c.Madre).WithMany().HasForeignKey(c => c.IdMadre).WillCascadeOnDelete(false);
            //HasOptional(c => c.Tutor).WithMany().HasForeignKey(c => c.IdTutor).WillCascadeOnDelete(false);
            //HasRequired(c => c.EstablecimientoSalud).WithMany().HasForeignKey(c => c.IdEstablecimientoSalud).WillCascadeOnDelete(false);
        }
    }
}
