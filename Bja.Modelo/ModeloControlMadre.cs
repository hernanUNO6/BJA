using Bja.Entidades;
using Bja.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class ModeloControlMadre
    {
        BjaContext context = new BjaContext();

        public void Crear(ControlMadre controlmadre)
        {
            controlmadre.Id = IdentifierGenerator.NewId();
            controlmadre.IdSesion = SessionManager.getCurrentSession().Id;
            controlmadre.FechaUltimaTransaccion = DateTime.Now;
            controlmadre.FechaRegistro = DateTime.Now;
            controlmadre.EstadoRegistro = TipoEstadoRegistro.Vigente;

            context.ControlesMadre.Add(controlmadre);

            context.SaveChanges();
        }

        public void Editar(long Id, ControlMadre controlmadre)
        {
            ControlMadre _controlmadre = null;

            _controlmadre = (from mc in context.ControlesMadre
                             where mc.Id == Id
                             select mc).FirstOrDefault();

            _controlmadre.IdSesion = SessionManager.getCurrentSession().Id;
            _controlmadre.FechaUltimaTransaccion = DateTime.Now;
            _controlmadre.FechaRegistro = DateTime.Now;
            _controlmadre.EstadoRegistro = TipoEstadoRegistro.Vigente;

            _controlmadre.IdCorresponsabilidadMadre = controlmadre.IdCorresponsabilidadMadre;
            _controlmadre.IdMedico = controlmadre.IdMedico;
            _controlmadre.IdMadre = controlmadre.IdMadre;
            _controlmadre.IdTutor = controlmadre.IdTutor;
            _controlmadre.TipoParentesco = controlmadre.TipoParentesco;
            _controlmadre.FechaProgramada = controlmadre.FechaProgramada;
            _controlmadre.FechaControl = controlmadre.FechaControl;
            _controlmadre.TallaCm = controlmadre.TallaCm;
            _controlmadre.PesoKg = controlmadre.PesoKg;
            _controlmadre.TipoControlMadre = controlmadre.TipoControlMadre;
            _controlmadre.NumeroControl = controlmadre.NumeroControl;
            _controlmadre.Observaciones = controlmadre.Observaciones;
            _controlmadre.EstadoPago = controlmadre.EstadoPago;
            _controlmadre.TipoBeneficiario = controlmadre.TipoBeneficiario;

            context.SaveChanges();
        }

        public void Eliminar(long Id)
        {
            ControlMadre controlmadre = null;

            controlmadre = (from mc in context.ControlesMadre
                            where mc.Id == Id
                            select mc).FirstOrDefault();

            controlmadre.IdSesion = SessionManager.getCurrentSession().Id;
            controlmadre.FechaUltimaTransaccion = DateTime.Now;
            controlmadre.FechaRegistro = DateTime.Now;
            controlmadre.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

            context.SaveChanges();
        }

        public ControlMadre Recuperar(long Id)
        {
            ControlMadre controlmadre = null;

            controlmadre = (from mc in context.ControlesMadre
                            where mc.Id == Id
                            select mc).FirstOrDefault();

            return controlmadre;
        }

        public List<ControlMadre> ListarControlesDeCorresponsabilidadDeMadre(long IdCorresponsabilidadMadre)
        {
            List<ControlMadre> controlmadre = new List<ControlMadre>();

            controlmadre = (from cm in context.ControlesMadre
                            where cm.IdCorresponsabilidadMadre == IdCorresponsabilidadMadre
                            orderby cm.NumeroControl 
                            select cm).ToList<ControlMadre>();

            return controlmadre;
        }

    }
}
