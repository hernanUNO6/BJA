using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class GrupoFamiliarConfiguration : EntityTypeConfiguration<GrupoFamiliar>
    {
        public GrupoFamiliarConfiguration()
        {
            ToTable("GruposFamiliares");
            HasKey(m => m.Id);
            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(m => m.IdFamilia).IsRequired();
            Property(m => m.IdMenor).IsOptional();
            Property(m => m.IdMadre).IsOptional();
            Property(m => m.IdTutor).IsOptional();
            Property(m => m.IdTipoParentesco).IsOptional();
            Property(m => m.TipoGrupoFamiliar).IsRequired();
            Property(m => m.TitularPagoVigente).IsOptional();

            //Relaciones
            HasRequired(m => m.Familia).WithMany().HasForeignKey(m => m.IdFamilia).WillCascadeOnDelete(false);
        }
    }
}
