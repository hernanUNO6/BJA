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
        tutor.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
        tutor.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
        tutor.DescripcionEstadoSincronizacion = "";

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
        _tutor.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
        _tutor.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;

        _tutor.Nombres = tutor.Nombres;
        _tutor.NombreCompleto = tutor.NombreCompleto;
        _tutor.PrimerApellido = tutor.PrimerApellido;
        _tutor.SegundoApellido = tutor.SegundoApellido;
        _tutor.TercerApellido = tutor.TercerApellido;
        _tutor.DocumentoIdentidad = tutor.DocumentoIdentidad;
        _tutor.TipoDocumentoIdentidad = tutor.TipoDocumentoIdentidad;
        _tutor.FechaNacimiento = tutor.FechaNacimiento;
        _tutor.IdDepartamento = tutor.IdDepartamento;
        _tutor.IdProvincia = tutor.IdProvincia;
        _tutor.IdMunicipio = tutor.IdMunicipio;
        _tutor.LocalidadNacimiento = tutor.LocalidadNacimiento;
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

    public List<Tutor> RecuperarTutor(long Id)
    {
        List<Tutor> tutor = new List<Tutor>();
        tutor = (from t in context.Tutores
                 where t.Id == Id
                 select t).ToList<Tutor>();
        return tutor;
    }

    //public List<Tutor> Listar()
    //{
    //    return context.Tutores.ToList();
    //}

    public List<Tutor> ListarTutoresDeMadresPorCriterio(string Criterio)
    {
        List<Tutor> tutor = new List<Tutor>();

        tutor = (from t in context.Tutores
                 where (t.Nombres.Contains(Criterio) ||
                       t.PrimerApellido.Contains(Criterio) ||
                       t.SegundoApellido.Contains(Criterio) ||
                       t.DocumentoIdentidad.Contains(Criterio)) &&
                       (t.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                 from cm in context.CorresponsabilidadesMadre
                 where cm.IdTutor == t.Id &&
                       cm.EstadoRegistro != TipoEstadoRegistro.BorradoLogico
                 orderby t.PrimerApellido, t.SegundoApellido, t.Nombres
                 select t).Distinct().ToList<Tutor>();

        return tutor;
    }

    public List<Tutor> ListarTutoresDeMenoresPorCriterio(string Criterio)
    {
        List<Tutor> tutor = new List<Tutor>();

        tutor = (from t in context.Tutores
                 where (t.Nombres.Contains(Criterio) ||
                       t.PrimerApellido.Contains(Criterio) ||
                       t.SegundoApellido.Contains(Criterio) ||
                       t.DocumentoIdentidad.Contains(Criterio)) &&
                       (t.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                 from cn in context.CorresponsabilidadesMenor
                 where cn.IdTutor == t.Id
                 orderby t.PrimerApellido, t.SegundoApellido, t.Nombres
                 select t).Distinct().ToList<Tutor>();

        return tutor;
    }

    public List<Tutor> ListarTutoresDeUnaFamilia(long IdFamilia)
    {//ojo filtrar los no borrados
        List<Tutor> tutor = new List<Tutor>();

        tutor = (from t in context.Tutores 
                 where t.EstadoRegistro != TipoEstadoRegistro.BorradoLogico
                 from gf in context.GruposFamiliares
                 where (gf.IdFamilia == IdFamilia) &&
                       (gf.IdReferencial == t.Id) &&
                       (gf.TipoGrupoFamiliar == TipoGrupoFamiliar.Tutor) &&
                       (gf.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                 select t).Distinct().ToList<Tutor>();

            return tutor;
    }

    public ResultadoPaginacion listaPaginada(long saltarRegistros = 0, long tamañoPagina = 20, string criterioBusqueda = "")
    {
        //buscar lista de registros paginados en base al criterio de búsqueda
        //en linq usar skip y take para la paginación
        //ej:myDataSource.Skip(saltarRegistros).Take(tamañoPagina)

        Int64 totalRegistrosEncontrados = 0;
        Int64 totalRegistros = 0;

        var lista = (from t in context.Tutores
                     where (t.Nombres.Contains(criterioBusqueda) ||
                           t.PrimerApellido.Contains(criterioBusqueda) ||
                           t.SegundoApellido.Contains(criterioBusqueda)) &&
                           (t.EstadoRegistro != TipoEstadoRegistro.BorradoLogico)
                     select t).ToList();

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
