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
        const int TAMANIO_PAGINA = 10;

        public List<Municipio> Listar()
        {
            var municipio = db.Municipios.Include(m => m.Provincia);
            return municipio.ToList();
        }

        public List<Municipio> Listar(string criterio)
        {
            return (from m in db.Municipios.Include(p => p.Provincia)
                    where m.Codigo.Contains(criterio) ||
                    m.Descripcion.Contains(criterio) ||
                    m.Provincia.Descripcion.Contains(criterio)
                    select m).ToList();
        }

        public List<Municipio> Listar(string criterio, int pagina)
        {
            if (criterio != null)
            {
                return (from m in db.Municipios.Include(p => p.Provincia)
                    where m.Codigo.Contains(criterio) ||
                    m.Descripcion.Contains(criterio) ||
                    m.Provincia.Descripcion.Contains(criterio)
                    select m).OrderBy(p => p.Provincia.Descripcion).Skip((pagina - 1) * TAMANIO_PAGINA).Take(TAMANIO_PAGINA).ToList();
            }
            else
            {
                return db.Municipios.Include(p => p.Provincia).OrderBy(p => p.Id).Skip((pagina - 1) * TAMANIO_PAGINA).Take(TAMANIO_PAGINA).ToList();
            }
        }

        public int TotalRegistros(string criterio)
        {
            if (criterio != null)
            {
                return (from m in db.Municipios.Include(p => p.Provincia)
                        where m.Codigo.Contains(criterio) ||
                        m.Descripcion.Contains(criterio) ||
                        m.Provincia.Descripcion.Contains(criterio)
                        select m).ToList().Count();
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

        public void Crear(Municipio municipio)
        {
            municipio.Id = IdentifierGenerator.NewId();
            municipio.IdSesion = SessionManager.getSessionIdentifier();
            municipio.FechaUltimaTransaccion = DateTime.Now;
            municipio.FechaRegistro = DateTime.Now;
            municipio.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            municipio.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            municipio.DescripcionEstadoSincronizacion = "";

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

        public void EliminarTodo()
        {
            foreach (var item in db.Municipios)
                db.Municipios.Remove(item);
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
