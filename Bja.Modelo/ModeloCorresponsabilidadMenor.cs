﻿using Bja.Entidades;
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
            corresponsabilidadmenor.EstadoRegistro = TipoEstadoRegistro.Vigente;

            context.CorresponsabilidadesMenor.Add(corresponsabilidadmenor);

            context.SaveChanges();
        }

        public void Editar(int Id, CorresponsabilidadMenor corresponsabilidadmenor)
        {
            CorresponsabilidadMenor _corresponsabilidadmenor = null;

            _corresponsabilidadmenor = (from cn in context.CorresponsabilidadesMenor
                                        where cn.Id == Id
                                        select cn).FirstOrDefault();

            _corresponsabilidadmenor.IdSesion = SessionManager.getCurrentSession().Id;
            _corresponsabilidadmenor.FechaUltimaTransaccion = DateTime.Now;
            _corresponsabilidadmenor.FechaRegistro = DateTime.Now;
            _corresponsabilidadmenor.EstadoRegistro = TipoEstadoRegistro.Vigente;

            _corresponsabilidadmenor.IdEstablecimientoSalud = corresponsabilidadmenor.IdEstablecimientoSalud;
            _corresponsabilidadmenor.TipoInscripcionMenor = corresponsabilidadmenor.TipoInscripcionMenor;
            _corresponsabilidadmenor.FechaInscripcion = corresponsabilidadmenor.FechaInscripcion;
            _corresponsabilidadmenor.IdMenor = corresponsabilidadmenor.IdMenor;
            //_corresponsabilidadmenor.DireccionMenor = corresponsabilidadmenor.DireccionMenor;
            _corresponsabilidadmenor.IdMadre = corresponsabilidadmenor.IdMadre;
            //_corresponsabilidadmenor.DireccionMadre = corresponsabilidadmenor.DireccionMadre;
            _corresponsabilidadmenor.IdTutor = corresponsabilidadmenor.IdTutor;
            //_corresponsabilidadmenor.DireccionTutor = corresponsabilidadmenor.DireccionTutor;
            _corresponsabilidadmenor.CodigoFormulario = corresponsabilidadmenor.CodigoFormulario;
            _corresponsabilidadmenor.FechaSalidaPrograma = corresponsabilidadmenor.FechaSalidaPrograma;
            _corresponsabilidadmenor.TipoSalidaMenor = corresponsabilidadmenor.TipoSalidaMenor;
            _corresponsabilidadmenor.Observaciones = corresponsabilidadmenor.Observaciones;
            _corresponsabilidadmenor.AutorizadoPor = corresponsabilidadmenor.AutorizadoPor;
            _corresponsabilidadmenor.CargoAutorizador = corresponsabilidadmenor.CargoAutorizador;

            context.SaveChanges();
        }

        public void Eliminar(int Id)
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

        public List<CorresponsabilidadMenor> Listar()
        {
            return context.CorresponsabilidadesMenor.ToList();
        }

    }
}
