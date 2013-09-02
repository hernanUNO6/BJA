using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class EnvioConfiguration : EntityTypeConfiguration<Envio>
    {
        public EnvioConfiguration()
        {
            ToTable("Envios");
            HasKey(c => c.Id);
            Property(c => c.IdSesion).IsRequired();
            Property(c => c.FechaRegistro).IsRequired();
            Property(c => c.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(c => c.IdEstablecimientoSalud).IsRequired();
            Property(c => c.IdMedico).IsRequired();
            Property(c => c.FechaEnvio).IsRequired();
            Property(c => c.CodigoVerificacion).IsRequired().HasMaxLength(128);
            Property(c => c.TotalNuevasMadres).IsRequired();
            Property(c => c.TotalNuevosTutores).IsRequired();
            Property(c => c.TotalNuevosMenores).IsRequired();
            Property(c => c.TotalNuevosControlMadre).IsRequired();
            Property(c => c.TotalNuevosControlMenor).IsRequired();
            Property(c => c.TotalNuevaCorresponsabilidadMadre).IsRequired();
            Property(c => c.TotalNuevaCorresponsabilidadMenor).IsRequired();
            Property(c => c.TotalModificacionMadres).IsRequired();
            Property(c => c.TotalModificacionMenores).IsRequired();
            Property(c => c.TotalModificacionTutores).IsRequired();
            Property(c => c.TotalModificacionControlMadre).IsRequired();
            Property(c => c.TotalModificacionControlMenor).IsRequired();
            Property(c => c.TotalModificacionCorresponsabilidadMadre).IsRequired();
            Property(c => c.TotalModificacionCorresponsabilidadMenor).IsRequired();
            Property(c => c.TotalBorradoMadres).IsRequired();
            Property(c => c.TotalBorradoTutores).IsRequired();
            Property(c => c.TotalBorradoMenores).IsRequired();

            //relaciones
            //HasRequired(c => c.CorresponsabilidadMadre).WithMany(cm => cm.ControlesMadre).HasForeignKey(c => c.IdCorresponsabilidadMadre);
            //HasRequired(c => c.Medico).WithMany().HasForeignKey(c => c.IdMedico).WillCascadeOnDelete(false);
            //HasRequired(c => c.Madre).WithMany().HasForeignKey(c => c.IdMadre).WillCascadeOnDelete(false);
            //HasOptional(c => c.Tutor).WithMany().HasForeignKey(c => c.IdTutor).WillCascadeOnDelete(false);
            //HasRequired(c => c.EstablecimientoSalud).WithMany().HasForeignKey(c => c.IdEstablecimientoSalud).WillCascadeOnDelete(false);
        }
    }
}
