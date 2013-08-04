﻿using Bja.Entidades;
using Bja.AccesoDatos;
using Bja.Soporte.Paginacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class ModeloMenor : IGrillaPaginada
  {
  
    BjaContext context = new BjaContext();

    public void Crear(Menor menor)
    {
      menor.Id = IdentifierGenerator.NewId();
      menor.IdSesion = SessionManager.getCurrentSession().Id;
      menor.FechaUltimaTransaccion = DateTime.Now;
      menor.FechaRegistro = DateTime.Now;
      menor.EstadoRegistro = TipoEstadoRegistro.Vigente;

      context.Menores.Add(menor);

      context.SaveChanges();
    }

    public void Editar(int Id, Menor menor)
    {
      Menor _menor = null;

      _menor = (from n in context.Menores
                where n.Id == Id
                select n).FirstOrDefault();

      _menor.IdSesion = SessionManager.getCurrentSession().Id;
      _menor.FechaUltimaTransaccion = DateTime.Now;
      _menor.FechaRegistro = DateTime.Now;
      _menor.EstadoRegistro = TipoEstadoRegistro.Vigente;

      _menor.Nombres = menor.Nombres;
      _menor.PrimerApellido = menor.PrimerApellido;
      _menor.SegundoApellido = menor.SegundoApellido;
      _menor.DocumentoIdentidad = menor.DocumentoIdentidad;
      _menor.IdTipoDocumentoIdentidad = menor.IdTipoDocumentoIdentidad;
      _menor.TipoDocumentoIdentidad = menor.TipoDocumentoIdentidad;
      _menor.IdLocalidadNacimiento = menor.IdLocalidadNacimiento;
      _menor.FechaNacimiento = menor.FechaNacimiento;
      _menor.Sexo = menor.Sexo;
      _menor.Defuncion = menor.Defuncion;
      _menor.Observaciones = menor.Observaciones;

      context.SaveChanges();
    }

    public void Eliminar(int Id)
    {
        Menor menor = null;

        menor = (from n in context.Menores
                  where n.Id == Id
                  select n).FirstOrDefault();

        menor.IdSesion = SessionManager.getCurrentSession().Id;
        menor.FechaUltimaTransaccion = DateTime.Now;
        menor.FechaRegistro = DateTime.Now;
        menor.EstadoRegistro = TipoEstadoRegistro.BorradoLogico;

        context.SaveChanges();
    }

    public Menor Recuperar(long Id)
    {
        Menor menor = null;

        menor = (from n in context.Menores
                where n.Id == Id
                select n).FirstOrDefault();

        return menor;
    }

    public List<Menor> Listar()
    {
        return context.Menores.ToList();
    }
    
    public ResultadoPaginacion listaPaginada(long saltarRegistros = 0, long tamañoPagina = 20, string criterioBusqueda = "")
    {
        //buscar lista de registros paginados en base al criterio de búsqueda
        //en linq usar skip y take para la paginación
        //ej:myDataSource.Skip(saltarRegistros).Take(tamañoPagina)

        Int64 totalRegistrosEncontrados = 0;
        Int64 totalRegistros = 0;
        var lista = (from n in context.Menores
                     where n.Nombres.Contains(criterioBusqueda) ||
                     n.PrimerApellido.Contains(criterioBusqueda) ||
                     n.SegundoApellido.Contains(criterioBusqueda)
                     select n).ToList();
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
        //retorna el total de registros en la tabla menor
        return context.Menores.Count();
    }

  }
}
