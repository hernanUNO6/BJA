using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class TipoParentesco
    {
        public long Id { get; set; }
        public long IdSesion { get; set; }
        public DateTime FechaUltimaTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TipoEstadoRegistro EstadoRegistro { get; set; }

        public String  Descripcion { get; set; }
        /*
        Padre = 1,      //Padre
        Abuelo,         //Abuela
        Tio,            //Tia
        Hermano,        //Hermana
        Primo,          //Prima
        Padrastro,      //Madrastra
        Suegro,         //Suegra
        Cunado,         //Cuñada
        Hermanastro,    //Hermanastra
        Designado,
        Otro*/
    }
}
