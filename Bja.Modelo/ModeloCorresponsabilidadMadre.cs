using Bja.Entidades;
using Bja.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class ModeloCorresponsabilidadMadre
    {
      BjaContext context = new BjaContext();

      public void Crear(CorresponsabilidadMadre corresponsabilidadmadre)
      {
          corresponsabilidadmadre.Id = IdentifierGenerator.NewId();
          corresponsabilidadmadre.IdSesion = SessionManager.getCurrentSession().Id;
          corresponsabilidadmadre.FechaUltimaTransaccion = DateTime.Now;
          corresponsabilidadmadre.FechaRegistro = DateTime.Now;
          corresponsabilidadmadre.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
          corresponsabilidadmadre.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
          corresponsabilidadmadre.DescripcionEstadoSincronizacion = "";

          context.CorresponsabilidadesMadre.Add(corresponsabilidadmadre);

          context.SaveChanges();
      }

      public void Editar(long Id, CorresponsabilidadMadre corresponsabilidadmadre)
      {
          CorresponsabilidadMadre _corresponsabilidadmadre = null;

          _corresponsabilidadmadre = (from cm in context.CorresponsabilidadesMadre
                                      where cm.Id == Id
                                      select cm).FirstOrDefault();

          _corresponsabilidadmadre.IdSesion = SessionManager.getCurrentSession().Id;
          _corresponsabilidadmadre.FechaUltimaTransaccion = DateTime.Now;
          _corresponsabilidadmadre.FechaRegistro = DateTime.Now;
          _corresponsabilidadmadre.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
          _corresponsabilidadmadre.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;

          _corresponsabilidadmadre.IdEstablecimientoSalud = corresponsabilidadmadre.IdEstablecimientoSalud;
          _corresponsabilidadmadre.TipoInscripcionMadre = corresponsabilidadmadre.TipoInscripcionMadre;
          _corresponsabilidadmadre.FechaInscripcion = corresponsabilidadmadre.FechaInscripcion;
          _corresponsabilidadmadre.IdMadre = corresponsabilidadmadre.IdMadre;
          _corresponsabilidadmadre.IdTutor = corresponsabilidadmadre.IdTutor;
          _corresponsabilidadmadre.IdTipoParentesco = corresponsabilidadmadre.IdTipoParentesco;
          _corresponsabilidadmadre.CodigoFormulario = corresponsabilidadmadre.CodigoFormulario;
          _corresponsabilidadmadre.FechaUltimaMenstruacion = corresponsabilidadmadre.FechaUltimaMenstruacion;
          _corresponsabilidadmadre.FechaUltimoParto = corresponsabilidadmadre.FechaUltimoParto;
          _corresponsabilidadmadre.NumeroEmbarazo = corresponsabilidadmadre.NumeroEmbarazo;
          _corresponsabilidadmadre.ARO = corresponsabilidadmadre.ARO;
          _corresponsabilidadmadre.FechaSalidaPrograma = corresponsabilidadmadre.FechaSalidaPrograma;
          _corresponsabilidadmadre.TipoSalidaMadre = corresponsabilidadmadre.TipoSalidaMadre;
          _corresponsabilidadmadre.Observaciones = corresponsabilidadmadre.Observaciones;
          _corresponsabilidadmadre.AutorizadoPor = corresponsabilidadmadre.AutorizadoPor;
          _corresponsabilidadmadre.CargoAutorizador = corresponsabilidadmadre.CargoAutorizador;

          context.SaveChanges();
      }

      public void Eliminar(long Id)
      {
          CorresponsabilidadMadre corresponsabilidadmadre = null;

          corresponsabilidadmadre = (from cm in context.CorresponsabilidadesMadre
                                     where cm.Id == Id
                                     select cm).FirstOrDefault();

          corresponsabilidadmadre.IdSesion = SessionManager.getCurrentSession().Id;
          corresponsabilidadmadre.FechaUltimaTransaccion = DateTime.Now;
          corresponsabilidadmadre.FechaRegistro = DateTime.Now;
          corresponsabilidadmadre.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

          context.SaveChanges();
      }

      public CorresponsabilidadMadre Recuperar(long Id)
      {
          CorresponsabilidadMadre corresponsabilidadmadre = null;

          corresponsabilidadmadre = (from cm in context.CorresponsabilidadesMadre 
                                     where cm.Id == Id
                                     select cm).FirstOrDefault();

          return corresponsabilidadmadre;
      }

      public long RecuperarLaUltimaCorresponsabilidadValidaDeMadre(long IdMadre)
      {
          long Id;
          string s;

          Id = 0;
          
          var c = (from cm in context.CorresponsabilidadesMadre
                                     where cm.IdMadre == IdMadre && 
                                            cm.EstadoRegistro != TipoEstadoRegistro.BorradoLogico
                                     orderby cm.FechaRegistro descending
                                     select cm.Id).Take(1);
          foreach (var g in c)
          {
              s = g.ToString();
              Id = Convert.ToInt64(s);
          }

          return Id;
      }

      public List<CorresponsabilidadMadre> ListarCorresponsabilidadesDeMadre(long IdMadre)
      {
          List<CorresponsabilidadMadre> corresponsabilidadmadre = new List<CorresponsabilidadMadre>();

          corresponsabilidadmadre = (from cm in context.CorresponsabilidadesMadre
                                     where cm.IdMadre == IdMadre
                                     select cm).ToList<CorresponsabilidadMadre>();

          return corresponsabilidadmadre;
      }

    }
}
