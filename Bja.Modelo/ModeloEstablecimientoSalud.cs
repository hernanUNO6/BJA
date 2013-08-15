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
    public class ModeloEstablecimientoSalud
    {
        private BjaContext db = new BjaContext();

        public List<EstablecimientoSalud> Listar()
        {
            var establecimientosSalud = db.EstablecimientosSalud.Include(e => e.Municipio);
            return establecimientosSalud.ToList();
        }

        public void Crear(EstablecimientoSalud estabSalud)
        {
            db.EstablecimientosSalud.Add(estabSalud);
            db.SaveChanges();
        }

        public EstablecimientoSalud Buscar(long id = 0)
        {
            EstablecimientoSalud estabSalud = db.EstablecimientosSalud.Find(id);
            if (estabSalud == null)
            {
                return null;
            }
            return estabSalud;
        }

        public void Editar(EstablecimientoSalud estabSalud)
        {
            db.Entry(estabSalud).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            EstablecimientoSalud estabSalud = this.Buscar(id);
            db.EstablecimientosSalud.Remove(estabSalud);
            db.SaveChanges();
        }

        public List<Municipio> GetMunicipiosPorProvincias(string idProvincia)
        {
            Int64 Identificador = Convert.ToInt64(idProvincia);

            List<Municipio> provs = (from m in db.Municipios
                                     where m.IdProvincia == Identificador
                                     select m).ToList();
            return provs;
        }
    }
}
