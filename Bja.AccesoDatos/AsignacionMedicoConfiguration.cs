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
    public class AsignacionMedicoConfiguration : EntityTypeConfiguration<AsignacionMedico>
    {
        public AsignacionMedicoConfiguration()
        {
            ToTable("AsignacionesMedico");
            HasKey(am => am.Id);
            Property(am => am.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(am => am.IdSesion).IsRequired();
            Property(am => am.FechaRegistro).IsRequired();
            Property(am => am.FechaUltimaTransaccion).IsRequired();

            Property(am => am.FechaInicio).IsRequired();
            Property(am => am.FechaFin).IsRequired();
            Property(am => am.Observaciones).HasMaxLength(300);

            Property(am => am.OperacionActual).IsOptional();

            //Relaciones
            HasRequired(am => am.Medico).WithMany().HasForeignKey(am => am.IdMedico);
            HasRequired(am => am.EstablecimientoSalud).WithMany().HasForeignKey(am => am.IdEstablecimientoSalud).WillCascadeOnDelete(false);
        }
    }
}
