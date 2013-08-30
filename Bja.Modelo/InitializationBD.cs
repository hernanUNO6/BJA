using Bja.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class InicializacionBD
    {
        public static void inicializarBD()
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<BjaContext>());

            var rbac = new Rbac();

            if (rbac.authenticate("admin", "admin")==null)
            {
                //inicializa base de datos con session 1
                long session = 1;
                rbac.insertUser("admin", "Admin", "admin", 1, session);
            }

            //adiciona registros de configuración
            AdministradorConfiguracion.Crear("idMedicoAsignado", "general", "123", Entidades.TipoDato.enterolargo);
            AdministradorConfiguracion.Crear("idEstablecimientoSalud", "general", "123", Entidades.TipoDato.enterolargo);

        }
    }
}
