﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class EncargadoLog : Encargado
    {
        public long IdLog { get; set; }
        public bool Enviado { get; set; }
    }
}