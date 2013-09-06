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
    public class ModeloFamilia : IGrillaPaginada
    {
        BjaContext context = new BjaContext();

        public void Crear(Familia familia)
        {
            familia.Id = IdentifierGenerator.NewId();
            familia.IdSesion = SessionManager.getSessionIdentifier();
            familia.FechaUltimaTransaccion = DateTime.Now;
            familia.FechaRegistro = DateTime.Now;
            familia.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            familia.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            familia.DescripcionEstadoSincronizacion = "";

            context.Familias.Add(familia);

            context.SaveChanges();
        }

        public void Editar(long Id, Familia familia)
        {
            Familia _familia = null;

            _familia = (from f in context.Familias
                        where f.Id == Id
                        select f).FirstOrDefault();

            _familia.IdSesion = SessionManager.getSessionIdentifier();
            _familia.FechaUltimaTransaccion = DateTime.Now;
            _familia.FechaRegistro = DateTime.Now;
            _familia.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
            _familia.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;

            _familia.IdEstablecimientoSalud = familia.IdEstablecimientoSalud;
            _familia.FechaInscripcion = familia.FechaInscripcion;
            _familia.PrimerApellido = familia.PrimerApellido;
            _familia.SegundoApellido = familia.SegundoApellido;
            _familia.IdDepartamento = familia.IdDepartamento;
            _familia.IdProvincia = familia.IdProvincia;
            _familia.IdMunicipio = familia.IdMunicipio;
            _familia.Localidad = familia.Localidad;
            _familia.Observaciones = familia.Observaciones;

            context.SaveChanges();
        }

        public void Eliminar(long Id)
        {
            Familia familia = null;

            familia = (from f in context.Familias
                       where f.Id == Id
                       select f).FirstOrDefault();

            familia.IdSesion = SessionManager.getSessionIdentifier();
            familia.FechaUltimaTransaccion = DateTime.Now;
            familia.FechaRegistro = DateTime.Now;
            familia.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

            context.SaveChanges();
        }

        public Familia Recuperar(long Id)
        {
            Familia familia = null;

            familia = (from f in context.Familias
                       where f.Id == Id
                       select f).FirstOrDefault();

            return familia;
        }

        public List<Familia> Listar()
        {
            //ojo filtrar los no borrados
            return context.Familias.ToList();
        }

        public List<Familia> ListarMadresPorCriterio(string Criterio)
        {
            List<Familia> familia = new List<Familia>();

            familia = (from f in context.Familias
                       where (f.PrimerApellido.Contains(Criterio) ||
                       f.SegundoApellido.Contains(Criterio)) && ((f.EstadoRegistro == TipoEstadoRegistro.VigenteNuevoRegistro) || (f.EstadoRegistro == TipoEstadoRegistro.VigenteRegistroModificado))
                       orderby f.PrimerApellido, f.SegundoApellido
                       select f).ToList<Familia>();

            return familia;
        }

        public ResultadoPaginacion listaPaginada(long saltarRegistros = 0, long tamañoPagina = 20, string criterioBusqueda = "")
        {
            //buscar lista de registros paginados en base al criterio de búsqueda
            //en linq usar skip y take para la paginación
            //ej:myDataSource.Skip(saltarRegistros).Take(tamañoPagina)

            Int64 totalRegistrosEncontrados = 0;
            Int64 totalRegistros = 0;

            var lista = (from f in context.Familias
                         where f.PrimerApellido.Contains(criterioBusqueda) ||
                         f.SegundoApellido.Contains(criterioBusqueda)
                         select f).ToList();

            //var lista = BuscarConveniosMantenimientoPaginada(ref totalRegistrosEncontrados, ref totalRegistros, saltarRegistros, tamañoPagina, criterioBusqueda);
            //crear la lista de objetos de tipo RegistroGrid

            var listaRegistroGrid = (from il in lista
                                     select new RegistroGrid(il.Id, il.PrimerApellido + " " + il.SegundoApellido, il)).ToList();

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
            return context.Familias.Count();
        }

    }
}
