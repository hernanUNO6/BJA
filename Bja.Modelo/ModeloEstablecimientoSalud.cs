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
            var establecimientosSalud = db.EstablecimientosSalud.Include(e => e.RedSalud);
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

/*
        public List<EstablecimientoSalud> GetEstablecimientosSaludPorMunicipio(string idMunicipio)
        {
            Int64 Identificador = Convert.ToInt64(idMunicipio);

            List<EstablecimientoSalud> estSalud = (from es in db.EstablecimientosSalud
                                                   where es.IdMunicipio == Identificador
                                                   select es).ToList();
            return estSalud;
        }*/

        public List<EstablecimientoSalud> GetEstablecimientosSaludPorRedSalud(long idRedSalud)
        {
            List<EstablecimientoSalud> estabsSalud = (from es in db.EstablecimientosSalud
                                                      where es.IdRedSalud == idRedSalud
                                                      select es).ToList();
            return estabsSalud;
        }
    }
}
