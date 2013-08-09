﻿using Bja.Entidades;
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
          corresponsabilidadmadre.EstadoRegistro = TipoEstadoRegistro.Vigente;

          context.CorresponsabilidadesMadre.Add(corresponsabilidadmadre);

          context.SaveChanges();
      }

      public void Editar(int Id, CorresponsabilidadMadre corresponsabilidadmadre)
      {
          CorresponsabilidadMadre _corresponsabilidadmadre = null;

          _corresponsabilidadmadre = (from cm in context.CorresponsabilidadesMadre
                                      where cm.Id == Id
                                      select cm).FirstOrDefault();

          _corresponsabilidadmadre.IdSesion = SessionManager.getCurrentSession().Id;
          _corresponsabilidadmadre.FechaUltimaTransaccion = DateTime.Now;
          _corresponsabilidadmadre.FechaRegistro = DateTime.Now;
          _corresponsabilidadmadre.EstadoRegistro = TipoEstadoRegistro.Vigente;

          _corresponsabilidadmadre.IdEstablecimientoSalud = corresponsabilidadmadre.IdEstablecimientoSalud;
          _corresponsabilidadmadre.TipoInscripcionMadre = corresponsabilidadmadre.TipoInscripcionMadre;
          _corresponsabilidadmadre.FechaInscripcion = corresponsabilidadmadre.FechaInscripcion;
          _corresponsabilidadmadre.IdMadre = corresponsabilidadmadre.IdMadre;
          _corresponsabilidadmadre.DireccionMadre = corresponsabilidadmadre.DireccionMadre;
          _corresponsabilidadmadre.IdTutor = corresponsabilidadmadre.IdTutor;
          _corresponsabilidadmadre.DireccionTutor = corresponsabilidadmadre.DireccionTutor;
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

      public void Eliminar(int Id)
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

      public List<CorresponsabilidadMadre> Listar()
      {
          return context.CorresponsabilidadesMadre.ToList();
      }

    }
}
