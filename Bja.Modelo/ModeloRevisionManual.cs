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
    public class ModeloRevisionManual
    {
        BjaContext db = new BjaContext();

        public List<Madre> Listar()
        {
            return db.Madres.ToList();
        }

        public List<MadreTemporal> ListarTempo()
        {
            return db.MadresTemporal.ToList();
        }

        public void Crear(Madre madre)
        {
            db.Madres.Add(madre);
            db.SaveChanges();
        }

        public void Crear(MadreTemporal madreTempo)
        {
            db.MadresTemporal.Add(madreTempo);
            db.SaveChanges();
        }

        public Madre Buscar(long id = 0)
        {
            Madre madre = db.Madres.Find(id);
            if (madre == null)
            {
                return null;
            }
            return madre;
        }



        public void EliminarTodoMadre()
        {
            foreach (var item in db.Madres)
                db.Madres.Remove(item);
            db.SaveChanges();
        }

        public void EliminarTodoMadreTempo()
        {
            foreach (var item in db.MadresTemporal)
                db.MadresTemporal.Remove(item);
            db.SaveChanges();
        }
    }
}
