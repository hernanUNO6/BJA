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
    public class ModeloAsignacionMedico
    {
        private BjaContext db = new BjaContext();

        public List<AsignacionMedico> Listar()
        {
            var asignacionesmedico = db.AsignacionesMedico.Include(a => a.Medico).Include(a => a.EstablecimientoSalud);
            return asignacionesmedico.ToList();
        }

        public void Crear(AsignacionMedico asignacionMedico)
        {
            asignacionMedico.Id = IdentifierGenerator.NewId();
            asignacionMedico.IdSesion = SessionManager.getSessionIdentifier();
            asignacionMedico.FechaUltimaTransaccion = DateTime.Now;
            asignacionMedico.FechaRegistro = DateTime.Now;
            asignacionMedico.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            asignacionMedico.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            asignacionMedico.DescripcionEstadoSincronizacion = "";

            db.AsignacionesMedico.Add(asignacionMedico);
            db.SaveChanges();
        }

        public AsignacionMedico Buscar(long id = 0)
        {
            AsignacionMedico asignacionMedico = db.AsignacionesMedico.Find(id);
            if (asignacionMedico == null)
            {
                return null;
            }
            return asignacionMedico;
        }

        public void Editar(AsignacionMedico asignacionMedico)
        {
            asignacionMedico.IdSesion = SessionManager.getSessionIdentifier();
            asignacionMedico.FechaUltimaTransaccion = DateTime.Now;
            asignacionMedico.FechaRegistro = DateTime.Now;
            asignacionMedico.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
            asignacionMedico.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            asignacionMedico.DescripcionEstadoSincronizacion = "";

            db.Entry(asignacionMedico).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(long id)
        {
            AsignacionMedico asignacionMedico = this.Buscar(id);
            db.AsignacionesMedico.Remove(asignacionMedico);
            db.SaveChanges();
        }

        //public List<Municipio> GetMunicipiosPorProvincias(string idProvincia)
        //{
        //    Int64 Identificador = Convert.ToInt64(idProvincia);

        //    List<Municipio> provs = (from m in db.Municipios
        //                             where m.IdProvincia == Identificador
        //                             select m).ToList();
        //    return provs;
        //}

        public AsignacionMedico EstablecimientoDeSaludHabilitado(long IdMedico)
        {
            AsignacionMedico asignacionmedico = null;

            asignacionmedico = (from am in db.AsignacionesMedico
                                where (am.IdMedico == IdMedico) && 
                                      (am.OperacionActual == true)
                                select am).FirstOrDefault();

            return asignacionmedico;
        }

        public List<RegistroParaCombo> ListarEstablecimientoDeSaludHabilitado(long IdMedico)
        {
            List<RegistroParaCombo> asignacionmedico = new List<RegistroParaCombo>();

            asignacionmedico = (from am in db.AsignacionesMedico
                                where (am.IdMedico == IdMedico) &&
                                      (am.OperacionActual == true)
                                select new RegistroParaCombo
                                 {
                                     Id = am.IdEstablecimientoSalud,
                                     Descripcion = am.EstablecimientoSalud.Nombre
                                 }).ToList();

            return asignacionmedico;
        }

        public List<RegistroParaCombo> ListarEstablecimientoDeSaludConfigurado(long IdMedico)
        {
            List<RegistroParaCombo> asignacionmedico = new List<RegistroParaCombo>();

            asignacionmedico = (from am in db.AsignacionesMedico
                                where am.IdMedico == IdMedico
                                select new RegistroParaCombo
                                {
                                    Id = am.IdEstablecimientoSalud,
                                    Descripcion = am.EstablecimientoSalud.Nombre
                                }).ToList();

            return asignacionmedico;
        }

        public void EstablecerEstablecimientoDeSaludComoVigenteParaUnMedico(long IdMedico, long IdEstablecimientoSalud)
        {
            AsignacionMedico _asignacionmedico = null;

            var asignacionmedico = (from am in db.AsignacionesMedico
                          where am.IdMedico == IdMedico &&
                                am.IdEstablecimientoSalud != IdEstablecimientoSalud
                          select am).ToList();

            foreach (var g in asignacionmedico)
            {
                g.OperacionActual = false;
            }

            db.SaveChanges();

            _asignacionmedico = (from am in db.AsignacionesMedico
                                 where (am.IdMedico == IdMedico) &&
                                       (am.IdEstablecimientoSalud == IdEstablecimientoSalud)
                                 select am).FirstOrDefault();

            _asignacionmedico.IdSesion = SessionManager.getSessionIdentifier();
            _asignacionmedico.FechaUltimaTransaccion = DateTime.Now;
            _asignacionmedico.FechaRegistro = DateTime.Now;
            _asignacionmedico.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
            _asignacionmedico.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;

            _asignacionmedico.OperacionActual = true;

            db.SaveChanges();
        }

    }
}
