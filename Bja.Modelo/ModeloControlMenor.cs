using Bja.Entidades;
using Bja.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class ModeloControlMenor
    {
        BjaContext context = new BjaContext();

        public void Crear(ControlMenor controlmenor)
        {
            controlmenor.Id = IdentifierGenerator.NewId();
            controlmenor.IdSesion = SessionManager.getCurrentSession().Id;
            controlmenor.FechaUltimaTransaccion = DateTime.Now;
            controlmenor.FechaRegistro = DateTime.Now;
            controlmenor.EstadoRegistro = TipoEstadoRegistro.Vigente;

            context.ControlesMenor.Add(controlmenor);

            context.SaveChanges();
        }

        public void Editar(long Id, ControlMenor controlmenor)
        {
            ControlMenor _controlmenor = null;

            _controlmenor = (from nc in context.ControlesMenor
                             where nc.Id == Id
                             select nc).FirstOrDefault();

            _controlmenor.IdSesion = SessionManager.getCurrentSession().Id;
            _controlmenor.FechaUltimaTransaccion = DateTime.Now;
            _controlmenor.FechaRegistro = DateTime.Now;
            _controlmenor.EstadoRegistro = TipoEstadoRegistro.Vigente;

            _controlmenor.IdCorresponsabilidadMenor = controlmenor.IdCorresponsabilidadMenor;
            _controlmenor.IdMedico = controlmenor.IdMedico;
            _controlmenor.IdTutor = controlmenor.IdTutor;
            _controlmenor.IdMadre = controlmenor.IdMadre;
            _controlmenor.FechaProgramada = controlmenor.FechaProgramada;
            _controlmenor.FechaControl = controlmenor.FechaControl;
            _controlmenor.TallaCm = controlmenor.TallaCm;
            _controlmenor.PesoKg = controlmenor.PesoKg;
            _controlmenor.NumeroControl = controlmenor.NumeroControl;
            _controlmenor.Observaciones = controlmenor.Observaciones;
            _controlmenor.EstadoPago = controlmenor.EstadoPago;
            _controlmenor.TipoBeneficiario = controlmenor.TipoBeneficiario;

            context.SaveChanges();
        }

        public void Eliminar(long Id)
        {
            ControlMenor controlmenor = null;

            controlmenor = (from nc in context.ControlesMenor
                            where nc.Id == Id
                            select nc).FirstOrDefault();

            controlmenor.IdSesion = SessionManager.getCurrentSession().Id;
            controlmenor.FechaUltimaTransaccion = DateTime.Now;
            controlmenor.FechaRegistro = DateTime.Now;
            controlmenor.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

            context.SaveChanges();
        }

        public ControlMenor Recuperar(long Id)
        {
            ControlMenor controlmenor = null;

            controlmenor = (from nc in context.ControlesMenor
                            where nc.Id == Id
                            select nc).FirstOrDefault();

            return controlmenor;
        }

        public List<ControlMenor> ListarControlesDeCorresponsabilidadDeMenor(long IdCorresponsabilidadMenor)
        {
            List<ControlMenor> controlmenor = new List<ControlMenor>();

            controlmenor = (from cm in context.ControlesMenor
                            where cm.IdCorresponsabilidadMenor == IdCorresponsabilidadMenor
                            select cm).ToList<ControlMenor>();

            return controlmenor;
        }

    }
}
