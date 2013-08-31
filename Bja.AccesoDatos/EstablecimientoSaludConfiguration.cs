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
    public class EstablecimientoSaludConfiguration : EntityTypeConfiguration<EstablecimientoSalud>
    {
        public EstablecimientoSaludConfiguration()
        {
            ToTable("EstablecimientosSalud");

            HasKey(em => em.Id);
            Property(em => em.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(em => em.IdSesion).IsRequired();
            Property(em => em.FechaRegistro).IsRequired();
            Property(em => em.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(em => em.Codigo).IsRequired().HasMaxLength(10);
            Property(em => em.Nombre).IsRequired().HasMaxLength(100);
            Property(em => em.Direccion).IsRequired().HasMaxLength(30);
            Property(em => em.Telefono).HasMaxLength(10);
            Property(em => em.IdRedSalud).IsRequired();

            HasRequired(em => em.RedSalud).WithMany(em => em.EstablecimientosSalud).HasForeignKey(em => em.IdRedSalud);
        }
    }
}
