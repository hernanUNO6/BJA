using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bja.AccesoDatos;
using Bja.Entidades;

namespace Bja.Modelo
{
    public class ModeloRedSalud
    {
        private BjaContext db = new BjaContext();

        public List<RedSalud> Listar()
        {
            var redSalud = db.RedesSalud.Include(m => m.Municipio);
            return redSalud.ToList();
        }

        public void Crear(RedSalud redSalud)
        {
            db.RedesSalud.Add(redSalud);
            db.SaveChanges();
        }

        public RedSalud Buscar(long id = 0)
        {
            RedSalud redSalud = db.RedesSalud.Find(id);
            if (redSalud == null)
            {
                return null;
            }
            return redSalud;
        }

        public void Editar(RedSalud redSalud)
        {
            db.Entry(redSalud).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            RedSalud redSalud = this.Buscar(id);
            db.RedesSalud.Remove(redSalud);
            db.SaveChanges();
        }

        public List<RedSalud> GetRedesSaludPorMunicipio(string idMunicipio)
        {
            Int64 Identificador = Convert.ToInt64(idMunicipio);

            List<RedSalud> redesSalud = (from rs in db.RedesSalud
                                         where rs.IdMunicipio == Identificador
                                         select rs).ToList();
            return redesSalud;
        }
    }
}
