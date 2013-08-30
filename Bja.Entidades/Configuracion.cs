using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class Configuracion
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }

        public String Nombre { get; set; }
        public String Clase { get; set; }
        public String Valor { get; set; }
        public TipoDato TipoDatoValor { get; set; }

    }
}
