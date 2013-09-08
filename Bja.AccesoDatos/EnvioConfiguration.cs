using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(c => c.IdSesion).IsRequired();
            Property(c => c.FechaRegistro).IsRequired();
            Property(c => c.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(c => c.IdEstablecimientoSalud).IsRequired();
            Property(c => c.IdMedico).IsRequired();
            Property(c => c.FechaEnvio).IsRequired();
            Property(c => c.CodigoVerificacion).IsRequired().HasMaxLength(128);

            //relaciones
            HasRequired(e => e.Medico).WithMany().HasForeignKey(e => e.IdMedico);
            HasRequired(e => e.EstablecimientoSalud).WithMany().HasForeignKey(e => e.IdEstablecimientoSalud);
            HasMany(e=> e.Madres).WithMany(m => m.Envios);
            HasMany(e => e.Tutores).WithMany(m => m.Envios);
            HasMany(e => e.Menores).WithMany(m => m.Envios);

            HasMany(e => e.CorresponsabilidadMadres).WithMany(m => m.Envios);
            HasMany(e => e.ControlMadres).WithMany(m => m.Envios);
            HasMany(e => e.CorresponsabilidadMenores).WithMany(m => m.Envios);
            HasMany(e => e.ControlMenores).WithMany(m => m.Envios);

        }
    }
}
