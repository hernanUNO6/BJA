using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration() 
        {
            ToTable("Roles");
            HasKey(r => r.Id);
            Property(r => r.IdSession).IsRequired();
            Property(r => r.Name).IsRequired().HasMaxLength(250);
            Property(r => r.Description).HasMaxLength(1000);
        }
    }
}
