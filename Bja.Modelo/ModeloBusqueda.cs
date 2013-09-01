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
                          where (m.Nombres.Contains(CriterioNombres) || m.PrimerApellido.Contains(CriterioPaterno) || m.SegundoApellido.Contains(CriterioMaterno) || m.DocumentoIdentidad.Contains(CriterioDocIde)) && (m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                          select new
                          {
                              Id = m.Id,
                              DocumentoIdentidad = m.DocumentoIdentidad,
                              Paterno = m.PrimerApellido,
                              Materno = m.SegundoApellido,
                              Nombres = m.Nombres,
                              Familia = "Familia",
                              Tipo = "Madre"
                          }
                          ).ToList();

            var menores = (from m in context.Menores
                           where (m.Nombres.Contains(CriterioNombres) || m.PrimerApellido.Contains(CriterioPaterno) || m.SegundoApellido.Contains(CriterioMaterno) || m.DocumentoIdentidad.Contains(CriterioDocIde)) && (m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                           select new
                           {
                               Id = m.Id,
                               DocumentoIdentidad = m.DocumentoIdentidad,
                               Paterno = m.PrimerApellido,
                               Materno = m.SegundoApellido,
                               Nombres = m.Nombres,
                               Familia = "Familia",
                               Tipo = "Menor"
                           }
                          ).ToList();

            var tutores = (from m in context.Tutores
                           where (m.Nombres.Contains(CriterioNombres) || m.PrimerApellido.Contains(CriterioPaterno) || m.SegundoApellido.Contains(CriterioMaterno) || m.DocumentoIdentidad.Contains(CriterioDocIde)) && (m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                           select new
                           {
                               Id = m.Id,
                               DocumentoIdentidad = m.DocumentoIdentidad,
                               Paterno = m.PrimerApellido,
                               Materno = m.SegundoApellido,
                               Nombres = m.Nombres,
                               Familia = "Familia",
                               Tipo = "Tutor"
                           }
                          ).ToList();

            //resultado = (from m in madres 
            //             select m).Union(from n in menores 
            //                             select n).Union(from t in tutores 
            //                                             select t).ToList<RegistroBusqueda>();

            return resultado;
        }

    }
}
