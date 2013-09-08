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
    public class InicializacionBD
    {
        public static void inicializarBD()
        {
            Database.SetInitializer<BjaContext>(new BjaContextInitializer());

            //    new DropCreateDatabaseIfModelChanges<BjaContext>());
        }
    }
}
