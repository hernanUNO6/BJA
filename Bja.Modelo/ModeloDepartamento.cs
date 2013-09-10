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
            depto.Id = IdentifierGenerator.NewId();
            depto.IdSesion = SessionManager.getSessionIdentifier();
            depto.FechaUltimaTransaccion = DateTime.Now;
            depto.FechaRegistro = DateTime.Now;
            depto.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            depto.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            depto.DescripcionEstadoSincronizacion = "";

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
            depto.IdSesion = SessionManager.getSessionIdentifier();
            depto.FechaUltimaTransaccion = DateTime.Now;
            depto.FechaRegistro = DateTime.Now;
            depto.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
            depto.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            depto.DescripcionEstadoSincronizacion = "";

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
