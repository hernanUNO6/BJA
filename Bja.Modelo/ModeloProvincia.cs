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
    public class ModeloProvincia
    {
        private BjaContext db = new BjaContext();

        public List<Provincia> Listar()
        {
            var provincias = db.Provincias.Include(p => p.Departamento);
            return provincias.ToList();   
        }

        public void Crear(Provincia provincia)
        {
            db.Provincias.Add(provincia);
            db.SaveChanges();
        }

        public Provincia Buscar(long id = 0)
        {
            Provincia provincia = db.Provincias.Find(id);
            if (provincia == null)
            {
                return null;
            }
            return provincia;
        }

        public void Editar(Provincia provincia)
        {
            db.Entry(provincia).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            Provincia provincia = this.Buscar(id);
            db.Provincias.Remove(provincia);
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
