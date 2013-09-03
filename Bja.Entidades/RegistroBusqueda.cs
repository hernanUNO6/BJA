using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class RegistroBusqueda
    {
        public long Id { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Familia { get; set; }
        public string Tipo { get; set; }
    }
}
