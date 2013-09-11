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
    public class ModeloMadre : IGrillaPaginada
    {

      BjaContext context = new BjaContext();

      public void Crear(Madre madre)
      {
          madre.Id = IdentifierGenerator.NewId();
          madre.IdSesion = SessionManager.getSessionIdentifier();
          madre.FechaUltimaTransaccion = DateTime.Now;
          madre.FechaRegistro = DateTime.Now;
          madre.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
          madre.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
          madre.DescripcionEstadoSincronizacion = "";

          context.Madres.Add(madre);

          context.SaveChanges();
      }

      public void Editar(long Id, Madre madre)
      {
          Madre _madre = null;

          _madre = (from p in context.Madres
                    where p.Id == Id
                    select p).FirstOrDefault();

          _madre.IdSesion = SessionManager.getSessionIdentifier();
          _madre.FechaUltimaTransaccion = DateTime.Now;
          _madre.FechaRegistro = DateTime.Now;
          _madre.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
          _madre.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;

          _madre.Nombres = madre.Nombres;
          _madre.NombreCompleto = madre.NombreCompleto;
          _madre.PrimerApellido = madre.PrimerApellido;
          _madre.SegundoApellido = madre.SegundoApellido;
          _madre.TercerApellido = madre.TercerApellido;
          _madre.DocumentoIdentidad = madre.DocumentoIdentidad;
          _madre.TipoDocumentoIdentidad = madre.TipoDocumentoIdentidad;
          _madre.FechaNacimiento = madre.FechaNacimiento;
          _madre.IdDepartamento = madre.IdDepartamento;
          _madre.IdProvincia = madre.IdProvincia;
          _madre.IdMunicipio = madre.IdMunicipio;
          _madre.LocalidadNacimiento = madre.LocalidadNacimiento;
          _madre.Defuncion = madre.Defuncion;
          _madre.Observaciones = madre.Observaciones;

          context.SaveChanges();
      }

      public void Eliminar(long Id)
      {
          Madre madre = null;

          madre = (from m in context.Madres 
                    where m.Id == Id
                    select m).FirstOrDefault();

          madre.IdSesion = SessionManager.getSessionIdentifier();
          madre.FechaUltimaTransaccion = DateTime.Now;
          madre.FechaRegistro = DateTime.Now;
          madre.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

          context.SaveChanges();
      }

      public Madre Recuperar(long Id)
      {
          Madre madre = null;

          madre = (from m in context.Madres 
                   where m.Id == Id
                   select m).FirstOrDefault();

          return madre;
      }

      //public List<Madre> Listar()
      //{
      //    //ojo filtrar los no borrados
      //    return context.Madres.ToList();
      //}

      public List<Madre> ListarMadresPorCriterio(string Criterio)
      {//ojo filtrar los no borrados
          List<Madre> madre = new List<Madre>();

          madre = (from m in context.Madres
                   where (m.Nombres.Contains(Criterio) ||
                         m.PrimerApellido.Contains(Criterio) ||
                         m.SegundoApellido.Contains(Criterio) ||
                         m.DocumentoIdentidad.Contains(Criterio)) && 
                         (m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                   orderby m.PrimerApellido, m.SegundoApellido, m.Nombres
                   select m).ToList<Madre>();

          return madre;
      }

      public List<Madre> ListarMadresDeMenorATravesDeCorresponsabilidadDeMenor(long IdMenor)
      {//ojo filtrar los no borrados
          List<Madre> madre = new List<Madre>();

          madre = (from cn in context.CorresponsabilidadesMenor
                   where (cn.IdMenor == IdMenor) && 
                         (cn.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                   from m in context.Madres
                   where (m.Id == cn.IdMadre) && 
                         (m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                   select m).Distinct().ToList<Madre>();

          return madre;
      }

      public List<Madre> ListarMadresBajoTuicionDeTutor(long IdTutor)
      {//ojo filtrar los no borrados
          List<Madre> madre = new List<Madre>();

          madre = (from cm in context.CorresponsabilidadesMadre
                   where (cm.IdTutor == IdTutor) &&
                         (cm.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                   from m in context.Madres
                   where (m.Id == cm.IdMadre) &&
                         (m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                   select m).Distinct().ToList<Madre>();

          return madre;
      }

      public List<Madre> ListarMadresDeUnaFamilia(long IdFamilia)
      {//ojo filtrar los no borrados
          List<Madre> madre = new List<Madre>();

          madre = (from m in context.Madres
                   where m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico
                   from gf in context.GruposFamiliares
                   where (gf.IdFamilia == IdFamilia) &&
                         (gf.IdMadre == m.Id) &&
                         (gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Madre) &&
                         (gf.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                   select m).Distinct().ToList<Madre>();

          return madre;
      }

      public List<RegistroParaCombo> ListarMadresDeUnaFamiliaParaCombo(long IdFamilia)
      {//ojo filtrar los no borrados
          List<RegistroParaCombo> tutor = new List<RegistroParaCombo>();

          tutor = (from m in context.Madres
                   where m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico
                   from gf in context.GruposFamiliares
                   where (gf.IdFamilia == IdFamilia) &&
                         (gf.IdMadre == m.Id) &&
                         (gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Madre) &&
                         (gf.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                   select new RegistroParaCombo
                   {
                       Id = m.Id,
                       Descripcion = m.NombreCompleto
                   }).ToList();

          return tutor;
      }

      public ResultadoPaginacion listaPaginada(long saltarRegistros = 0, long tamañoPagina = 20, string criterioBusqueda = "")
      {
          //buscar lista de registros paginados en base al criterio de búsqueda
          //en linq usar skip y take para la paginación
          //ej:myDataSource.Skip(saltarRegistros).Take(tamañoPagina)

          Int64 totalRegistrosEncontrados = 0;
          Int64 totalRegistros = 0;

          var lista = (from m in context.Madres
                           where (m.Nombres.Contains(criterioBusqueda) || 
                                 m.PrimerApellido.Contains(criterioBusqueda) || 
                                 m.SegundoApellido.Contains(criterioBusqueda)) &&
                                 (m.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                           select m).ToList();

          //var lista = BuscarConveniosMantenimientoPaginada(ref totalRegistrosEncontrados, ref totalRegistros, saltarRegistros, tamañoPagina, criterioBusqueda);
          //crear la lista de objetos de tipo RegistroGrid
          
          var listaRegistroGrid = (from il in lista
                                    select new RegistroGrid(il.Id, il.Nombres + " " + il.PrimerApellido, il)).ToList();
            
          //crear resultado paginación
          var result = new ResultadoPaginacion();
          result.resultados = listaRegistroGrid;
          result.totalEncontrados = totalRegistrosEncontrados;
          result.totalRegistros = totalRegistros;
          return result;
            
      }

      public long totalRegistros()
      {
          //retorna el total de registros en la tabla madre
          return context.Madres.Count(); 
      }

      // Adicionado por Hernán
      public Madre Buscar(long id = 0)
      {
          Madre madre = context.Madres.Find(id);
          if (madre == null)
          {
              return null;
          }
          return madre;
      }

    }
}
