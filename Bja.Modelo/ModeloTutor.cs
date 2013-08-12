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
    public class ModeloTutor : IGrillaPaginada
  {
    BjaContext context = new BjaContext();

    public void Crear(Tutor tutor)
    {
        tutor.Id = IdentifierGenerator.NewId();
        tutor.IdSesion = SessionManager.getCurrentSession().Id;
        tutor.FechaUltimaTransaccion = DateTime.Now;
        tutor.FechaRegistro = DateTime.Now;
        tutor.EstadoRegistro = TipoEstadoRegistro.Vigente;

        context.Tutores.Add(tutor);

        context.SaveChanges();
    }

    public void Editar(long Id, Tutor tutor)
    {
        Tutor _tutor = null;

        _tutor = (from t in context.Tutores
                  where t.Id == Id
                  select t).FirstOrDefault();

        _tutor.IdSesion = SessionManager.getCurrentSession().Id;
        _tutor.FechaUltimaTransaccion = DateTime.Now;
        _tutor.FechaRegistro = DateTime.Now;
        _tutor.EstadoRegistro = TipoEstadoRegistro.Vigente;
        _tutor.Nombres = tutor.Nombres;
        _tutor.PrimerApellido = tutor.PrimerApellido;
        _tutor.SegundoApellido = tutor.SegundoApellido;
        _tutor.TercerApellido = tutor.TercerApellido;
        _tutor.DocumentoIdentidad = tutor.DocumentoIdentidad;
        _tutor.IdTipoDocumentoIdentidad = tutor.IdTipoDocumentoIdentidad;
        _tutor.TipoDocumentoIdentidad = tutor.TipoDocumentoIdentidad;
        _tutor.FechaNacimiento = tutor.FechaNacimiento;
        _tutor.IdLocalidadNacimiento = tutor.IdLocalidadNacimiento;
        _tutor.Defuncion = tutor.Defuncion;
        _tutor.Observaciones = tutor.Observaciones;
        _tutor.Sexo = tutor.Sexo;

        context.SaveChanges();
    }

    public void Eliminar(long Id)
    {
        Tutor _tutor = null;

        _tutor = (from m in context.Tutores
                  where m.Id == Id
                  select m).FirstOrDefault();

        _tutor.IdSesion = SessionManager.getCurrentSession().Id;
        _tutor.FechaUltimaTransaccion = DateTime.Now;
        _tutor.FechaRegistro = DateTime.Now;
        _tutor.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

        context.SaveChanges();
    }

    public Tutor Recuperar(long Id)
    {
        Tutor tutor = null;

        tutor = (from m in context.Tutores
                 where m.Id == Id
                 select m).FirstOrDefault();

        return tutor;
    }

    public List<Tutor> Listar()
    {
        return context.Tutores.ToList();
    }

    public List<Madre> ListarMadresDependientes(long IdTutor)
    {
        List<Madre> madre = new List<Madre>();

        //...

        return madre;
    }

    public List<Menor> ListarMenoresDependientes(long IdTutor)
    {
        List<Menor> menor = new List<Menor>();

        //...

        return menor;
    }

    public ResultadoPaginacion listaPaginada(long saltarRegistros = 0, long tamañoPagina = 20, string criterioBusqueda = "")
    {
        //buscar lista de registros paginados en base al criterio de búsqueda
        //en linq usar skip y take para la paginación
        //ej:myDataSource.Skip(saltarRegistros).Take(tamañoPagina)

        Int64 totalRegistrosEncontrados = 0;
        Int64 totalRegistros = 0;

        var lista = (from m in context.Tutores
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
        return context.Tutores.Count();
    }

  }
}
