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
        const int TAMANIO_PAGINA = 10;

        public List<Provincia> Listar()
        {
            var provincias = db.Provincias.Include(p => p.Departamento);
            return provincias.ToList();   
        }

        public List<Provincia> Listar(string criterio)
        {
            return (from p in db.Provincias.Include(p => p.Departamento)
                    where p.Codigo.Contains(criterio) ||
                    p.Descripcion.Contains(criterio) ||
                    p.Departamento.Descripcion.Contains(criterio)
                    select p).ToList();
        }

        public List<Provincia> Listar(string criterio, int pagina)
        {
            if (criterio != null)
            {
                return (from p in db.Provincias.Include(p => p.Departamento)
                        where p.Codigo.Contains(criterio) ||
                        p.Descripcion.Contains(criterio) ||
                        p.Departamento.Descripcion.Contains(criterio)
                        select p).OrderBy(p => p.Departamento.Descripcion).Skip((pagina - 1) * TAMANIO_PAGINA).Take(TAMANIO_PAGINA).ToList();
            }
            else
            {
                return db.Provincias.Include(p => p.Departamento).OrderBy(p => p.Id).Skip((pagina - 1) * TAMANIO_PAGINA).Take(TAMANIO_PAGINA).ToList();
            }
        }

        public int TotalRegistros(string criterio)
        {
            if (criterio != null)
            {
                return (from p in db.Provincias.Include(p => p.Departamento)
                    where p.Codigo.Contains(criterio) ||
                    p.Descripcion.Contains(criterio) ||
                    p.Departamento.Descripcion.Contains(criterio)
                    select p).ToList().Count();
            }
            else
            {
                return db.Provincias.Count();
            }
        }

        public int TamanioPagina()
        {
            return TAMANIO_PAGINA;
        }

        public void Crear(Provincia provincia)
        {
            provincia.Id = IdentifierGenerator.NewId();
            provincia.IdSesion = SessionManager.getSessionIdentifier();
            provincia.FechaUltimaTransaccion = DateTime.Now;
            provincia.FechaRegistro = DateTime.Now;
            provincia.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            provincia.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            provincia.DescripcionEstadoSincronizacion = "";

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

        public void EliminarTodo()
        {
            foreach (var item in db.Provincias)
                db.Provincias.Remove(item);
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
