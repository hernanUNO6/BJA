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
            grupofamiliar.IdSesion = SessionManager.getSessionIdentifier();
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

            _grupofamiliar.IdSesion = SessionManager.getSessionIdentifier();
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
            _grupofamiliar.TitularPagoVigente = grupofamiliar.TitularPagoVigente;

            context.SaveChanges();
        }

        public void Eliminar(long Id)
        {
            GrupoFamiliar grupofamiliar = null;

            grupofamiliar = (from gf in context.GruposFamiliares
                       where gf.Id == Id
                       select gf).FirstOrDefault();

            grupofamiliar.IdSesion = SessionManager.getSessionIdentifier();
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

        public GrupoFamiliar RecuperarPorTutorDeFamilia(long IdFamilia, long IdTutor)
        {
            GrupoFamiliar grupofamiliar = null;

            grupofamiliar = (from gf in context.GruposFamiliares
                             where gf.IdFamilia == IdFamilia &&
                                   gf.IdTutor == IdTutor &&
                                   gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Tutor 
                             select gf).FirstOrDefault();

            return grupofamiliar;
        }

        public GrupoFamiliar RecuperarPorMenorDeFamilia(long IdFamilia, long IdMenor)
        {
            GrupoFamiliar grupofamiliar = null;

            grupofamiliar = (from gf in context.GruposFamiliares
                             where gf.IdFamilia == IdFamilia &&
                                   gf.IdMenor == IdMenor &&
                                   gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Menor
                             select gf).FirstOrDefault();

            return grupofamiliar;
        }

        public GrupoFamiliar RecuperarPorMadreDeFamilia(long IdFamilia, long IdMadre)
        {
            GrupoFamiliar grupofamiliar = null;

            grupofamiliar = (from gf in context.GruposFamiliares
                             where gf.IdFamilia == IdFamilia &&
                                   gf.IdMadre == IdMadre &&
                                   gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Madre
                             select gf).FirstOrDefault();

            return grupofamiliar;
        }

        public GrupoFamiliar RecuperarTitularHabilitado(long IdFamilia)
        {
            GrupoFamiliar grupofamiliar = null;

            grupofamiliar = (from gf in context.GruposFamiliares
                             where gf.IdFamilia == IdFamilia &&
                                   gf.TitularPagoVigente == true &&
                                   (gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Madre || gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Tutor)
                             select gf).FirstOrDefault();

            return grupofamiliar;
        }

        public GrupoFamiliar RecuperarMadreDeFamilia(long IdFamilia)
        {
            GrupoFamiliar grupofamiliar = null;

            grupofamiliar = (from gf in context.GruposFamiliares
                             where gf.IdFamilia == IdFamilia &&
                                   gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Madre
                             select gf).FirstOrDefault();

            return grupofamiliar;
        }

        public List<RegistroTitularPago> ListarMadresYTutoresDeUnaFamilia(long IdFamilia)
        {
            List<RegistroTitularPago> resultado = new List<RegistroTitularPago>();

            var madres = (from gm in context.GruposFamiliares
                          where gm.EstadoRegistro != TipoEstadoRegistro.BorradoLogico &&
                                gm.IdFamilia == IdFamilia &&
                                gm.TipoGrupoFamiliar == TipoGrupoFamiliar.Madre
                          select new RegistroTitularPago
                          { 
                              Id = gm.Madre.Id,
                              IdGrupoFamiliar = gm.Id,
                              Tipo = "Madre",
                              Titular = gm.TitularPagoVigente == true ? "SI" : "",
                              DocumentoIdentidad = gm.Madre.DocumentoIdentidad,
                              Paterno = gm.Madre.PrimerApellido,
                              Materno = gm.Madre.SegundoApellido,
                              Conyuge = gm.Madre.TercerApellido,
                              Nombres = gm.Madre.Nombres,
                              FechaNacimiento = gm.Madre.FechaNacimiento,
                              Sexo = "Femenino"
                          }).ToList();

            var tutores = (from gt in context.GruposFamiliares
                          where gt.EstadoRegistro != TipoEstadoRegistro.BorradoLogico &&
                                gt.IdFamilia == IdFamilia &&
                                gt.TipoGrupoFamiliar == TipoGrupoFamiliar.Tutor
                          select new RegistroTitularPago
                          {
                              Id = gt.Tutor.Id,
                              IdGrupoFamiliar = gt.Id,
                              Tipo = "Tutor",
                              Titular = gt.TitularPagoVigente == true ? "SI" : "",
                              DocumentoIdentidad = gt.Tutor.DocumentoIdentidad,
                              Paterno = gt.Tutor.PrimerApellido,
                              Materno = gt.Tutor.SegundoApellido,
                              Conyuge = gt.Tutor.TercerApellido,
                              Nombres = gt.Tutor.Nombres,
                              FechaNacimiento = gt.Tutor.FechaNacimiento,
                              Sexo = gt.Tutor.Sexo == "M" ? "Masculiano" : gt.Tutor.Sexo == "F" ? "Femenino" : ""
                          }).ToList();

            resultado = (from m in madres select m)
                        .Union(from t in tutores select t).
                        OrderBy(g => g.Paterno).
                        ThenBy(g => g.Materno).
                        ThenBy(g => g.Nombres).
                        ToList<RegistroTitularPago>();

            return resultado;
        }

        public void EstablecerTitularDePagoVigenteDeFamilia(long IdFamilia, long IdGrupoFamiliar)
        {
            GrupoFamiliar _grupofamiliar = null;

            var grupofamiliar = (from gf in context.GruposFamiliares
                                 where gf.IdFamilia == IdFamilia &&
                                      gf.TipoGrupoFamiliar != TipoGrupoFamiliar.Menor
                                 select gf).ToList();

            foreach (var g in grupofamiliar)
            {
                g.TitularPagoVigente = false;
            }

            context.SaveChanges();

            _grupofamiliar = (from gf in context.GruposFamiliares
                              where gf.IdFamilia == IdFamilia &&
                                    gf.Id == IdGrupoFamiliar
                                 select gf).FirstOrDefault();

            _grupofamiliar.IdSesion = SessionManager.getSessionIdentifier();
            _grupofamiliar.FechaUltimaTransaccion = DateTime.Now;
            _grupofamiliar.FechaRegistro = DateTime.Now;
            _grupofamiliar.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
            _grupofamiliar.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;

            _grupofamiliar.TitularPagoVigente = true;

            context.SaveChanges();
        }

    }
}
