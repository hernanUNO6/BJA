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
    public class ModeloBusqueda
    {
        BjaContext context = new BjaContext();

        public List<RegistroBusqueda> ListarBusquedaDeRegistrosPorCriterio(string CriterioDocIde, string CriterioPaterno, string CriterioMaterno, string CriterioNombres)
        {
            List<RegistroBusqueda> resultado = new List<RegistroBusqueda>();

            var madres = (from m in context.Madres
                          where (m.Nombres.Contains(CriterioNombres) ||
                                 m.PrimerApellido.Contains(CriterioPaterno) ||
                                 m.SegundoApellido.Contains(CriterioMaterno) ||
                                 m.DocumentoIdentidad.Contains(CriterioDocIde)) &&
                                 (m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                          from gf in context.GruposFamiliares
                          where gf.EstadoRegistro != TipoEstadoRegistro.BorradoLogico &&
                                m.Id == gf.IdMadre && gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Madre
                          from fa in context.Familias
                          where fa.EstadoRegistro != TipoEstadoRegistro.BorradoLogico &&
                               fa.Id == gf.IdFamilia
                          select new RegistroBusqueda
                                {
                                    Id = fa.Id,
                                    DocumentoIdentidad = m.DocumentoIdentidad,
                                    Paterno = m.PrimerApellido,
                                    Materno = m.SegundoApellido,
                                    Nombres = m.Nombres,
                                    FechaNacimiento = m.FechaNacimiento,
                                    Sexo = "Femenino",
                                    Familia = fa.PrimerApellido + " " + fa.SegundoApellido,
                                    Tipo = "Madre"
                                }
                              ).ToList();

            var menores = (from n in context.Menores
                           where (n.Nombres.Contains(CriterioNombres) ||
                                  n.PrimerApellido.Contains(CriterioPaterno) ||
                                  n.SegundoApellido.Contains(CriterioMaterno) ||
                                  n.DocumentoIdentidad.Contains(CriterioDocIde)) &&
                                  (n.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                           from gf in context.GruposFamiliares
                           where gf.EstadoRegistro != TipoEstadoRegistro.BorradoLogico &&
                                 n.Id == gf.IdMenor && gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Menor
                           from fa in context.Familias
                           where fa.EstadoRegistro != TipoEstadoRegistro.BorradoLogico &&
                                fa.Id == gf.IdFamilia
                           select new RegistroBusqueda
                                 {
                                     Id = fa.Id,
                                     DocumentoIdentidad = n.DocumentoIdentidad,
                                     Paterno = n.PrimerApellido,
                                     Materno = n.SegundoApellido,
                                     Nombres = n.Nombres,
                                     FechaNacimiento = n.FechaNacimiento,
                                     Sexo = n.Sexo == "M" ? "Masculiano" : n.Sexo == "F" ? "Femenino" : "",
                                     Familia = fa.PrimerApellido + " " + fa.SegundoApellido,
                                     Tipo = n.Sexo == "M" ? "Niño" : n.Sexo == "F" ? "Niña" : ""
                                 }
                              ).ToList();

            var tutores = (from t in context.Tutores
                           where (t.Nombres.Contains(CriterioNombres) ||
                                  t.PrimerApellido.Contains(CriterioPaterno) ||
                                  t.SegundoApellido.Contains(CriterioMaterno) ||
                                  t.DocumentoIdentidad.Contains(CriterioDocIde)) &&
                                  (t.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                           from gf in context.GruposFamiliares
                           where gf.EstadoRegistro != TipoEstadoRegistro.BorradoLogico &&
                                 t.Id == gf.IdTutor && gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Tutor
                           from fa in context.Familias
                           where fa.EstadoRegistro != TipoEstadoRegistro.BorradoLogico &&
                                fa.Id == gf.IdFamilia
                           select new RegistroBusqueda
                                 {
                                     Id = fa.Id,
                                     DocumentoIdentidad = t.DocumentoIdentidad,
                                     Paterno = t.PrimerApellido,
                                     Materno = t.SegundoApellido,
                                     Nombres = t.Nombres,
                                     FechaNacimiento = t.FechaNacimiento,
                                     Sexo = t.Sexo == "M" ? "Masculiano" : t.Sexo == "F" ? "Femenino" : "",
                                     Familia = fa.PrimerApellido + " " + fa.SegundoApellido,
                                     Tipo = "Titular de Pago"
                                 }
                              ).ToList();

            resultado = (from m in madres select m)
                        .Union(from n in menores select n)
                        .Union(from t in tutores select t).
                        OrderBy(g => g.Paterno).
                        ThenBy(g => g.Materno).
                        ThenBy(g => g.Nombres).
                        ToList<RegistroBusqueda>();

            return resultado;
        }

    }
}
