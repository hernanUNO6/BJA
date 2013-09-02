using Bja.Entidades;
using Bja.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class ModeloCorresponsabilidadMenor
    {
        BjaContext context = new BjaContext();

        public void Crear(CorresponsabilidadMenor corresponsabilidadmenor)
        {
            corresponsabilidadmenor.Id = IdentifierGenerator.NewId();
            corresponsabilidadmenor.IdSesion = SessionManager.getCurrentSession().Id;
            corresponsabilidadmenor.FechaUltimaTransaccion = DateTime.Now;
            corresponsabilidadmenor.FechaRegistro = DateTime.Now;
            corresponsabilidadmenor.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            corresponsabilidadmenor.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            corresponsabilidadmenor.DescripcionEstadoSincronizacion = "";

            context.CorresponsabilidadesMenor.Add(corresponsabilidadmenor);

            context.SaveChanges();
        }

        public void Editar(long Id, CorresponsabilidadMenor corresponsabilidadmenor)
        {
            CorresponsabilidadMenor _corresponsabilidadmenor = null;

            _corresponsabilidadmenor = (from cn in context.CorresponsabilidadesMenor
                                        where cn.Id == Id
                                        select cn).FirstOrDefault();

            _corresponsabilidadmenor.IdSesion = SessionManager.getCurrentSession().Id;
            _corresponsabilidadmenor.FechaUltimaTransaccion = DateTime.Now;
            _corresponsabilidadmenor.FechaRegistro = DateTime.Now;
            _corresponsabilidadmenor.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
            _corresponsabilidadmenor.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;

            _corresponsabilidadmenor.IdEstablecimientoSalud = corresponsabilidadmenor.IdEstablecimientoSalud;
            _corresponsabilidadmenor.TipoInscripcionMenor = corresponsabilidadmenor.TipoInscripcionMenor;
            _corresponsabilidadmenor.FechaInscripcion = corresponsabilidadmenor.FechaInscripcion;
            _corresponsabilidadmenor.IdMenor = corresponsabilidadmenor.IdMenor;
            _corresponsabilidadmenor.IdMadre = corresponsabilidadmenor.IdMadre;
            _corresponsabilidadmenor.IdTutor = corresponsabilidadmenor.IdTutor;
            _corresponsabilidadmenor.IdTipoParentesco = corresponsabilidadmenor.IdTipoParentesco;
            _corresponsabilidadmenor.CodigoFormulario = corresponsabilidadmenor.CodigoFormulario;
            _corresponsabilidadmenor.FechaSalidaPrograma = corresponsabilidadmenor.FechaSalidaPrograma;
            _corresponsabilidadmenor.TipoSalidaMenor = corresponsabilidadmenor.TipoSalidaMenor;
            _corresponsabilidadmenor.Observaciones = corresponsabilidadmenor.Observaciones;
            _corresponsabilidadmenor.AutorizadoPor = corresponsabilidadmenor.AutorizadoPor;
            _corresponsabilidadmenor.CargoAutorizador = corresponsabilidadmenor.CargoAutorizador;

            context.SaveChanges();
        }

        public void Eliminar(long Id)
        {
            CorresponsabilidadMenor corresponsabilidadmenor = null;

            corresponsabilidadmenor = (from cn in context.CorresponsabilidadesMenor
                                       where cn.Id == Id
                                       select cn).FirstOrDefault();

            corresponsabilidadmenor.IdSesion = SessionManager.getCurrentSession().Id;
            corresponsabilidadmenor.FechaUltimaTransaccion = DateTime.Now;
            corresponsabilidadmenor.FechaRegistro = DateTime.Now;
            corresponsabilidadmenor.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

            context.SaveChanges();
        }

        public CorresponsabilidadMenor Recuperar(long Id)
        {
            CorresponsabilidadMenor corresponsabilidadmenor = null;

            corresponsabilidadmenor = (from cn in context.CorresponsabilidadesMenor
                                       where cn.Id == Id
                                       select cn).FirstOrDefault();

            return corresponsabilidadmenor;
        }

        public List<CorresponsabilidadMenor> ListarCorresponsabilidadesDeMenor(long IdMenor)
        {
            List<CorresponsabilidadMenor> corresponsabilidadmenor = new List<CorresponsabilidadMenor>();

            corresponsabilidadmenor = (from cm in context.CorresponsabilidadesMenor
                                       where cm.IdMenor == IdMenor
                                       select cm).ToList<CorresponsabilidadMenor>();

            return corresponsabilidadmenor;
        }

        public long RecuperarLaUltimaCorresponsabilidadValidaDeMenor(long IdMenor)
        {
            long Id;
            string s;

            Id = 0;

            var c = (from cn in context.CorresponsabilidadesMenor 
                     where cn.IdMenor == IdMenor &&
                            cn.EstadoRegistro != TipoEstadoRegistro.BorradoLogico
                     orderby cn.FechaRegistro descending
                     select cn.Id).Take(1);
            foreach (var g in c)
            {
                s = g.ToString();
                Id = Convert.ToInt64(s);
            }

            return Id;
        }

    }
}
