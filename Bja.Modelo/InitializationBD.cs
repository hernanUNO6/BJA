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
