using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.AccesoDatos
{
    public class BjaContext : DbContext
    {
        public BjaContext() : base()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Encargado> Encargados { get; set; }
        public DbSet<Familia> Familias { get; set; }
        public DbSet<GrupoFamiliar> GruposFamiliares { get; set; }
        public DbSet<Madre> Madres { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Menor> Menores { get; set; }
        public DbSet<CorresponsabilidadMadre> CorresponsabilidadesMadre { get; set; }
        public DbSet<CorresponsabilidadMenor> CorresponsabilidadesMenor { get; set; }
        public DbSet<ControlMadre> ControlesMadre { get; set; }
        public DbSet<ControlMenor> ControlesMenor { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Recepcion> Recepciones { get; set; }
        public DbSet<SolicitudPago> SolicitudesPago { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Reclamo> Reclamos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<RedSalud> RedesSalud { get; set; }
        public DbSet<EstablecimientoSalud> EstablecimientosSalud { get; set; }
        public DbSet<AsignacionMedico> AsignacionesMedico { get; set; }

        public DbSet<FamiliaTemporal> FamiliasTemporal { get; set; }
        public DbSet<GrupoFamiliarTemporal> GruposFamiliaresTemporal { get; set; }
        public DbSet<MadreTemporal> MadresTemporal { get; set; }
        public DbSet<ControlMadreTemporal> ControlMadresTemporal { get; set; }
        public DbSet<CorresponsabilidadMadreTemporal> CorresponsabilidadMadresTemporal { get; set; }
        public DbSet<ControlMenorTemporal> ControlMenoresTemporal { get; set; }
        public DbSet<CorresponsabilidadMenorTemporal> CorresponsabilidadMenoresTemporal { get; set; }
        public DbSet<MenorTemporal> MenoresTemporal { get; set; }
        public DbSet<TutorTemporal> TutoresTemporal { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PermissionConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new SessionConfiguration());
            modelBuilder.Configurations.Add(new ConfiguracionConfiguration());

            modelBuilder.Configurations.Add(new MedicoConfiguration());
            modelBuilder.Configurations.Add(new EncargadoConfiguration());
            modelBuilder.Configurations.Add(new FamiliaConfiguration());
            modelBuilder.Configurations.Add(new GrupoFamiliarConfiguration());
            modelBuilder.Configurations.Add(new MadreConfiguration());
            modelBuilder.Configurations.Add(new TutorConfiguration());
            modelBuilder.Configurations.Add(new MenorConfiguration());
            modelBuilder.Configurations.Add(new CorresponsabilidadMadreConfiguration());
            modelBuilder.Configurations.Add(new CorresponsabilidadMenorConfiguration());
            modelBuilder.Configurations.Add(new ControlMadreConfiguration());
            modelBuilder.Configurations.Add(new ControlMenorConfiguration());
            modelBuilder.Configurations.Add(new EnvioConfiguration());
            modelBuilder.Configurations.Add(new RecepcionConfiguration());
            modelBuilder.Configurations.Add(new PagoConfiguration());

            modelBuilder.Configurations.Add(new DepartamentoConfiguration());
            modelBuilder.Configurations.Add(new ProvinciaConfiguration());
            modelBuilder.Configurations.Add(new MunicipioConfiguration());
            modelBuilder.Configurations.Add(new RedSaludConfiguration());
            modelBuilder.Configurations.Add(new EstablecimientoSaludConfiguration());
            modelBuilder.Configurations.Add(new AsignacionMedicoConfiguration());

            modelBuilder.Configurations.Add(new FamiliaTemporalConfiguration());
            modelBuilder.Configurations.Add(new GrupoFamiliarTemporalConfiguration());
            modelBuilder.Configurations.Add(new MadreTemporalConfiguration());
            modelBuilder.Configurations.Add(new ControlMadreTemporalConfiguration());
            modelBuilder.Configurations.Add(new CorresponsabilidadMadreTemporalConfiguration());
            modelBuilder.Configurations.Add(new ControlMenorTemporalConfiguration());
            modelBuilder.Configurations.Add(new CorresponsabilidadMenorTemporalConfiguration());
            modelBuilder.Configurations.Add(new MenorTemporalConfiguration());
            modelBuilder.Configurations.Add(new TutorTemporalConfiguration());
        }
    }
}
