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
    public class ModeloMunicipio
    {
        private BjaContext db = new BjaContext();

        public List<Municipio> Listar()
        {
            var municipio = db.Municipios.Include(m => m.Provincia);
            return municipio.ToList();
        }

        public void Crear(Municipio municipio)
        {
            db.Municipios.Add(municipio);
            db.SaveChanges();
        }

        public Municipio Buscar(long id = 0)
        {
            Municipio municipio = db.Municipios.Find(id);
            if (municipio == null)
            {
                return null;
            }
            return municipio;
        }

        public void Editar(Municipio municipio)
        {
            db.Entry(municipio).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            Municipio municipio = this.Buscar(id);
            db.Municipios.Remove(municipio);
            db.SaveChanges();
        }

        public List<Provincia> GetProvinciasPorDepartamento(string idDepto)
        {
            Int64 Identificador = Convert.ToInt64(idDepto);

            List<Provincia> provs = (from p in db.Provincias
                                    where p.IdDepartamento == Identificador
                                    select p).ToList();
            return provs;
        }
    }
}
