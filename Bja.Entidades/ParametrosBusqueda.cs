using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    [Serializable]
    public class ParametrosBusqueda
    {
        public string Identificador { get; set; }
        public bool Nombres { get; set; }
        public bool PrimerApellido { get; set; }
        public bool SegundoApellido { get; set; }
        public bool TercerApellido { get; set; }
        public bool NombreCompleto { get; set; }
        public bool DocumentoIdentidad { get; set; }
        public bool FechaNacimiento { get; set; }
        public bool LocalidadNacimiento { get; set; }
    }
}
