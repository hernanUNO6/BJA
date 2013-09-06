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
    public class ModeloMadreTemporal
    {
        BjaContext db = new BjaContext();

        public List<MadreTemporal> Listar()
        {
            return db.MadresTemporal.ToList();
        }

        public void Crear(MadreTemporal madreTemp)
        {
            db.MadresTemporal.Add(madreTemp);
            db.SaveChanges();
        }

        public MadreTemporal Buscar(long id = 0)
        {
            MadreTemporal madreTemp = db.MadresTemporal.Find(id);
            if (madreTemp == null)
            {
                return null;
            }
            return madreTemp;
        }

        public List<MadreTemporal> Espejo(string id)
        {
            return Listar().Where(m => m.Id == Convert.ToInt64(id)).ToList();
        }

        public List<Madre> BuscarSemejantes(string id)
        {
            long identificador = Convert.ToInt64(id);
            MadreTemporal madreTemp = new MadreTemporal();
            madreTemp = Listar().Where(m => m.Id == identificador).FirstOrDefault();

            ModeloMadre madre = new ModeloMadre();
            List<Madre> madresCandidatas = new List<Madre>();

            Madre peorSemejanza = new Madre();

            int distanciaCalculada;
            int peorDistancia = -1;

            //foreach (Madre ma in madre.Listar())
            foreach (Madre ma in db.Madres.ToList())
            {
                if (madresCandidatas.Count <= 10)
                {
                    //madresCandidatas.Add(ma);
                    distanciaCalculada = Distancia.Levenshtein(madreTemp.NombreCompleto, ma.NombreCompleto);
                    if (distanciaCalculada > peorDistancia)
                    {
                        peorDistancia = distanciaCalculada;
                        peorSemejanza = ma;
                    }
                }
                else
                {
                    break;
                }
            }

            madresCandidatas.Add(peorSemejanza);

            return madresCandidatas;
        }
    }
}
