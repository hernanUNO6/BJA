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
    public class ModeloDepartamento
    {
        BjaContext db = new BjaContext();

        public List<Departamento> Listar()
        {
            return db.Departamentos.ToList();
        }

        public void Crear(Departamento depto)
        {
            db.Departamentos.Add(depto);
            db.SaveChanges();
        }

        public Departamento Buscar(long id = 0)
        {
            Departamento depto = db.Departamentos.Find(id);
            if (depto == null)
            {
                return null;
            }
            return depto;
        }

        public void Editar(Departamento depto)
        {
            db.Entry(depto).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            Departamento depto = this.Buscar(id);
            db.Departamentos.Remove(depto);
            db.SaveChanges();
        }

        public void EliminarTodo()
        {
            foreach (var item in db.Departamentos)
                db.Departamentos.Remove(item);
            db.SaveChanges();
        }
    }
}
