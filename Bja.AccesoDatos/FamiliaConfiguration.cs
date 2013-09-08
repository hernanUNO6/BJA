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
    public class FamiliaConfiguration : EntityTypeConfiguration<Familia>
    {
        public FamiliaConfiguration()
        {
            ToTable("Familias");
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(m => m.IdSesion).IsRequired();
            Property(m => m.FechaRegistro).IsRequired();
            Property(m => m.FechaUltimaTransaccion).IsRequired();
            Property(m => m.EstadoSincronizacion).IsRequired();
            Property(m => m.DescripcionEstadoSincronizacion).IsRequired().HasMaxLength(512);

            Property(m => m.IdEstablecimientoSalud).IsRequired();
            Property(m => m.PrimerApellido).IsRequired().HasMaxLength(50);
            Property(m => m.SegundoApellido).IsRequired().HasMaxLength(50);
            Property(m => m.FechaInscripcion).IsRequired();
            Property(m => m.IdDepartamento).IsRequired();
            Property(m => m.IdProvincia).IsRequired();
            Property(m => m.IdMunicipio).IsRequired();
            Property(m => m.Localidad).IsRequired().HasMaxLength(250);
            Property(m => m.Observaciones).IsOptional().HasMaxLength(1024);

            //relaciones
            HasRequired(f => f.EstablecimientoSalud).WithMany().HasForeignKey(f => f.IdEstablecimientoSalud);
            HasRequired(f => f.Departamento).WithMany().HasForeignKey(f => f.IdDepartamento);
            HasRequired(f => f.Municipio).WithMany().HasForeignKey(f => f.IdMunicipio);
            HasRequired(f => f.Provincia).WithMany().HasForeignKey(f => f.IdProvincia);
            //Por duplicidad
            //HasRequired(f => f.Departamento).WithMany().HasForeignKey(f => f.IdDepartamento);
        }
    }
}
