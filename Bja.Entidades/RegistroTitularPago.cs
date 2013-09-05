using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class RegistroTitularPago
    {
        public long Id { get; set; } //Id de Madre o de Tutor
        public long IdGrupoFamiliar { get; set; }
        public string Tipo { get; set; }
        public string Titular { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Conyuge { get; set; }
        public string Nombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
    }
}
