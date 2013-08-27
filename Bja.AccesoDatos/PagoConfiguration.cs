using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class PagoConfiguration : EntityTypeConfiguration<Pago>
    {
        public PagoConfiguration()
        {
            ToTable("Pagos");
            HasKey(m => m.Id);
            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();

            Property(m => m.IdTutor).IsRequired();
            Property(m => m.NombreCompletoTutor).IsRequired().HasMaxLength(500);
            Property(m => m.NombreCompletoMadre).IsRequired().HasMaxLength(500);
            Property(m => m.IdBeneficiario).IsRequired();
            Property(m => m.NombreCompletoBeneficiario).IsRequired().HasMaxLength(500);
            Property(m => m.SexoTitular).IsRequired().HasMaxLength(1);
            Property(m => m.FechaNacimientoTitular).IsRequired();
            Property(m => m.Monto).IsRequired();
            Property(m => m.Control).IsRequired().HasMaxLength(32);
        }
    }
}
