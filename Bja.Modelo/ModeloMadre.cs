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
          madre.IdSesion = SessionManager.getCurrentSession().Id;
          madre.FechaUltimaTransaccion = DateTime.Now;
          madre.FechaRegistro = DateTime.Now;
          madre.EstadoRegistro = TipoEstadoRegistro.Vigente;

          context.Madres.Add(madre);

          context.SaveChanges();

          Logger.log(madre);
      }

      public void Editar(long Id, Madre madre)
      {
          Madre _madre = null;

          _madre = (from p in context.Madres
                    where p.Id == Id
                    select p).FirstOrDefault();

          _madre.IdSesion = SessionManager.getCurrentSession().Id;
          _madre.FechaUltimaTransaccion = DateTime.Now;
          _madre.FechaRegistro = DateTime.Now;
          _madre.EstadoRegistro = TipoEstadoRegistro.Vigente;

          _madre.Nombres = madre.Nombres;
          _madre.PrimerApellido = madre.PrimerApellido;
          _madre.SegundoApellido = madre.SegundoApellido;
          _madre.TercerApellido = madre.TercerApellido;
          _madre.IdTipoDocumentoIdentidad = madre.IdTipoDocumentoIdentidad;
          _madre.TipoDocumentoIdentidad = madre.TipoDocumentoIdentidad;
          _madre.FechaNacimiento = madre.FechaNacimiento;
          _madre.IdLocalidadNacimiento = madre.IdLocalidadNacimiento;
          _madre.Defuncion = madre.Defuncion;
          _madre.Observaciones = madre.Observaciones;

          context.SaveChanges();

          Logger.log(_madre);
      }

      public void Eliminar(long Id)
      {
          Madre madre = null;

          madre = (from m in context.Madres 
                    where m.Id == Id
                    select m).FirstOrDefault();

          madre.IdSesion = SessionManager.getCurrentSession().Id;
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

      public List<Madre> Listar()
      {
          return context.Madres.ToList();
      }

      //public List<Menor> ListarMenoresDependientes(long IdTutor)
      //{
      //    List<Menor> menor = new List<Menor>();

      //    //...

      //    return menor;
      //}

      public ResultadoPaginacion listaPaginada(long saltarRegistros = 0, long tamañoPagina = 20, string criterioBusqueda = "")
      {
          //buscar lista de registros paginados en base al criterio de búsqueda
          //en linq usar skip y take para la paginación
          //ej:myDataSource.Skip(saltarRegistros).Take(tamañoPagina)

          Int64 totalRegistrosEncontrados = 0;
          Int64 totalRegistros = 0;

          var lista = (from m in context.Madres
                           where m.Nombres.Contains(criterioBusqueda) || 
                           m.PrimerApellido.Contains(criterioBusqueda) || 
                           m.SegundoApellido.Contains(criterioBusqueda)
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

    }
}
