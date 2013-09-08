using Bja.AccesoDatos;
using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    class BjaContextInitializer : DropCreateDatabaseIfModelChanges<BjaContext> 
    {
        protected override void Seed(BjaContext context)
        {
            base.Seed(context);

            //añadir registro de medico como usuario
            Medico medico = new Medico()
            {
                Nombres = "Herbert",
                PrimerApellido = "Saal",
                SegundoApellido = "Gutierrez",
                DocumentoIdentidad = "43123333",
                TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad,
                CorreoElectronico = "herbert.saal@gmail.com",
                EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro,
                FechaNacimiento = DateTime.Parse("05/04/1976"),
                LocalidadNacimiento = "La Paz",
                MatriculaColegioMedico = "1234567890"
            };

            ModeloMedico modelo = new ModeloMedico();
            modelo.Crear(medico);

            Rbac rbac = new Rbac();
            rbac.insertUser("admin", medico.Nombres + " " + medico.PrimerApellido + " " + medico.SegundoApellido, "admin",
                medico.Id);

            //departamento
            Departamento departamento = new Departamento() { Codigo = "LPZ", Descripcion = "La Paz" };
            ModeloDepartamento modeloDepartamento = new ModeloDepartamento();
            modeloDepartamento.Crear(departamento);

            Provincia provincia = new Provincia() { Codigo = "MUR", Descripcion = "Murillo", IdDepartamento = departamento.Id };
            ModeloProvincia modeloProvincia = new ModeloProvincia();
            modeloProvincia.Crear(provincia);

            Municipio municipio = new Municipio() { Codigo = "LP", Descripcion = "La Paz", IdProvincia = provincia.Id };
            ModeloMunicipio modeloMunicipio = new ModeloMunicipio();
            modeloMunicipio.Crear(municipio);

            RedSalud redSalud = new RedSalud() { Codigo = "MI", Nombre = "Red Salud miraflores", IdMunicipio = municipio.Id };
            ModeloRedSalud modeloRedSalud = new ModeloRedSalud();
            modeloRedSalud.Crear(redSalud);

            EstablecimientoSalud establecimientoSalud = new EstablecimientoSalud()
            {
                Codigo = "HO",
                Nombre = "Hospital Obrero",
                Direccion = "Miraflores",
                IdRedSalud = redSalud.Id
            };
            ModeloEstablecimientoSalud modeloEstablecimientoSalud = new ModeloEstablecimientoSalud();
            modeloEstablecimientoSalud.Crear(establecimientoSalud);

            //asignación medico
            AsignacionMedico asignacionMedico = new AsignacionMedico()
            {
                IdEstablecimientoSalud = establecimientoSalud.Id,
                IdMedico = medico.Id,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Today
            };
            ModeloAsignacionMedico modeloAsignacionMedico = new ModeloAsignacionMedico();
            modeloAsignacionMedico.Crear(asignacionMedico);


            //adiciona registros de configuración 
            //Lo siguiente en lo futuro hay que quitar
            // Se asumen que estos vendrán establecidos por la central
            AdministradorConfiguracion.Crear("ControlesDeMadrePreParto", "Madre", "4", Entidades.TipoDato.enterolargo);
            AdministradorConfiguracion.Crear("ControlesDeMadreParto", "Madre", "1", Entidades.TipoDato.enterolargo);
            AdministradorConfiguracion.Crear("ControlesDeMadrePostParto", "Madre", "1", Entidades.TipoDato.enterolargo);
            AdministradorConfiguracion.Crear("ControlesDeMenor", "Menor", "12", Entidades.TipoDato.enterolargo);
        }
    }
}
