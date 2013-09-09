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
    public class ModeloRedSalud
    {
        private BjaContext db = new BjaContext();
        const int TAMANIO_PAGINA = 10;

        public List<RedSalud> Listar()
        {
            var redSalud = db.RedesSalud.Include(m => m.Municipio);
            return redSalud.ToList();
        }

        public List<RedSalud> Listar(string criterio)
        {
            return (from rs in db.RedesSalud.Include(p => p.Municipio)
                    where rs.Codigo.Contains(criterio) ||
                    rs.Nombre.Contains(criterio) ||
                    rs.Municipio.Descripcion.Contains(criterio)
                    select rs).ToList();
        }

        public List<RedSalud> Listar(string criterio, int pagina)
        {
            if (criterio != null)
            {
                return (from rs in db.RedesSalud.Include(p => p.Municipio)
                        where rs.Codigo.Contains(criterio) ||
                        rs.Nombre.Contains(criterio) ||
                        rs.Municipio.Descripcion.Contains(criterio)
                        select rs).OrderBy(p => p.Municipio.Descripcion).Skip((pagina - 1) * TAMANIO_PAGINA).Take(TAMANIO_PAGINA).ToList();
            }
            else
            {
                return db.RedesSalud.Include(p => p.Municipio).OrderBy(p => p.Id).Skip((pagina - 1) * TAMANIO_PAGINA).Take(TAMANIO_PAGINA).ToList();
            }
        }

        public int TotalRegistros(string criterio)
        {
            if (criterio != null)
            {
                return (from rs in db.RedesSalud.Include(p => p.Municipio)
                        where rs.Codigo.Contains(criterio) ||
                        rs.Nombre.Contains(criterio) ||
                        rs.Municipio.Descripcion.Contains(criterio)
                        select rs).ToList().Count();
            }
            else
            {
                return db.RedesSalud.Count();
            }
        }

        public int TamanioPagina()
        {
            return TAMANIO_PAGINA;
        }

        public void Crear(RedSalud redSalud)
        {
            redSalud.Id = IdentifierGenerator.NewId();
            redSalud.IdSesion = SessionManager.getSessionIdentifier();
            redSalud.FechaUltimaTransaccion = DateTime.Now;
            redSalud.FechaRegistro = DateTime.Now;
            redSalud.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            redSalud.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            redSalud.DescripcionEstadoSincronizacion = "";

            db.RedesSalud.Add(redSalud);
            db.SaveChanges();
        }

        public RedSalud Buscar(long id = 0)
        {
            RedSalud redSalud = db.RedesSalud.Find(id);
            if (redSalud == null)
            {
                return null;
            }
            return redSalud;
        }

        public void Editar(RedSalud redSalud)
        {
            db.Entry(redSalud).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            RedSalud redSalud = this.Buscar(id);
            db.RedesSalud.Remove(redSalud);
            db.SaveChanges();
        }

        public List<RedSalud> GetRedesSaludPorMunicipio(string idMunicipio)
        {
            Int64 Identificador = Convert.ToInt64(idMunicipio);

            List<RedSalud> redesSalud = (from rs in db.RedesSalud
                                         where rs.IdMunicipio == Identificador
                                         select rs).ToList();
            return redesSalud;
        }
    }
}
