using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bja.AccesoDatos;
using Bja.Entidades;

namespace Bja.Modelo
{
    public class ModeloTipoParentesco
    {
        BjaContext context = new BjaContext();

        public List<TipoParentesco> Listar()
        {
            return context.TiposParentescos.ToList();
        }

    }
}
