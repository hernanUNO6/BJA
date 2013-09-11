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

        public void Crear(EstablecimientoSalud establecimientoSalud)
        {
            establecimientoSalud.Id = IdentifierGenerator.NewId();
            establecimientoSalud.IdSesion = SessionManager.getSessionIdentifier();
            establecimientoSalud.FechaUltimaTransaccion = DateTime.Now;
            establecimientoSalud.FechaRegistro = DateTime.Now;
            establecimientoSalud.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            establecimientoSalud.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            establecimientoSalud.DescripcionEstadoSincronizacion = "";

            db.EstablecimientosSalud.Add(establecimientoSalud);
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

        public void Editar(EstablecimientoSalud establecimientoSalud)
        {
            establecimientoSalud.IdSesion = SessionManager.getSessionIdentifier();
            establecimientoSalud.FechaUltimaTransaccion = DateTime.Now;
            establecimientoSalud.FechaRegistro = DateTime.Now;
            establecimientoSalud.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
            establecimientoSalud.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            establecimientoSalud.DescripcionEstadoSincronizacion = "";

            db.Entry(establecimientoSalud).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            EstablecimientoSalud estabSalud = this.Buscar(id);
            db.EstablecimientosSalud.Remove(estabSalud);
            db.SaveChanges();
        }

        public List<EstablecimientoSalud> GetEstablecimientosSaludPorRedSalud(long idRedSalud)
        {
            List<EstablecimientoSalud> estabsSalud = (from es in db.EstablecimientosSalud
                                                      where es.IdRedSalud == idRedSalud
                                                      select es).ToList();
            return estabsSalud;
        }
    }
}
