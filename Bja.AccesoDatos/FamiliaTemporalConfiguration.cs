using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class FamiliaTemporalConfiguration : EntityTypeConfiguration<FamiliaTemporal>
    {
        public FamiliaTemporalConfiguration()
        {
            ToTable("FamiliasTemporal");
            HasKey(m => m.IdTemporal);
            Property(m => m.UltimoRegistro).IsRequired();

            HasKey(m => m.Id);
            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(m => m.IdEstablecimientoSalud).IsRequired();
            Property(m => m.PrimerApellido).IsRequired().HasMaxLength(100);
            Property(m => m.SegundoApellido).IsRequired().HasMaxLength(100);
            Property(m => m.FechaInscripcion).IsRequired();
            Property(m => m.IdDepartamento).IsRequired();
            Property(m => m.IdProvincia).IsRequired();
            Property(m => m.IdMunicipio).IsRequired();
            Property(m => m.Localidad).IsRequired().HasMaxLength(100);
            Property(m => m.Observaciones).IsOptional().HasMaxLength(1024);
        }
    }
}
