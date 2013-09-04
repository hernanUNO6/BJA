using Bja.Entidades;
using Bja.AccesoDatos;
using Bja.Soporte.Paginacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class ModeloGrupoFamiliar
    {
        BjaContext context = new BjaContext();

        public void Crear(GrupoFamiliar grupofamiliar)
        {
            grupofamiliar.Id = IdentifierGenerator.NewId();
            grupofamiliar.IdSesion = SessionManager.getCurrentSession().Id;
            grupofamiliar.FechaUltimaTransaccion = DateTime.Now;
            grupofamiliar.FechaRegistro = DateTime.Now;
            grupofamiliar.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            grupofamiliar.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            grupofamiliar.DescripcionEstadoSincronizacion = "";

            context.GruposFamiliares.Add(grupofamiliar);

            context.SaveChanges();
        }

        public void Editar(long Id, GrupoFamiliar grupofamiliar)
        {
            GrupoFamiliar _grupofamiliar = null;

            _grupofamiliar = (from gf in context.GruposFamiliares
                        where gf.Id == Id
                        select gf).FirstOrDefault();

            _grupofamiliar.IdSesion = SessionManager.getCurrentSession().Id;
            _grupofamiliar.FechaUltimaTransaccion = DateTime.Now;
            _grupofamiliar.FechaRegistro = DateTime.Now;
            _grupofamiliar.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
            _grupofamiliar.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;

            _grupofamiliar.IdFamilia = grupofamiliar.IdFamilia;
            _grupofamiliar.IdMadre = grupofamiliar.IdMadre;
            _grupofamiliar.IdMenor = grupofamiliar.IdMenor;
            _grupofamiliar.IdTutor = grupofamiliar.IdTutor;
            _grupofamiliar.IdTipoParentesco = grupofamiliar.IdTipoParentesco;
            _grupofamiliar.TipoGrupoFamiliar = grupofamiliar.TipoGrupoFamiliar;

            context.SaveChanges();
        }

        public void Eliminar(long Id)
        {
            GrupoFamiliar grupofamiliar = null;

            grupofamiliar = (from gf in context.GruposFamiliares
                       where gf.Id == Id
                       select gf).FirstOrDefault();

            grupofamiliar.IdSesion = SessionManager.getCurrentSession().Id;
            grupofamiliar.FechaUltimaTransaccion = DateTime.Now;
            grupofamiliar.FechaRegistro = DateTime.Now;
            grupofamiliar.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

            context.SaveChanges();
        }

        public GrupoFamiliar Recuperar(long Id)
        {
            GrupoFamiliar grupofamiliar = null;

            grupofamiliar = (from gf in context.GruposFamiliares 
                       where gf.Id == Id
                       select gf).FirstOrDefault();

            return grupofamiliar;
        }

    }
}
