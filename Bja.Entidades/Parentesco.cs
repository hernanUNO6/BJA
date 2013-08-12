using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public enum Parentesco
    {
        //Estas pueden estar relacionados al tutor tanto para la madre como para el menor
        Madre = 0,  //Se asume por defecto
        Padre,
        Abuelo,         //Abuela
        Tio,            //Tia
        Hermano,        //Hermana
        Primo,          //Prima
        Suegro,         //Suegra
        Cunado,         //Cuñada
        Hermanastro,    //Hermanastra
        Designado = 50, //Tutor
        Otro = 100      //Otro
    }
}
