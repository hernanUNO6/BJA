using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bja.Entidades;
using Bja.Modelo;

namespace Bja.Central.Web.Controllers
{
    public class ProvinciasController : Controller
    {
        private ModeloProvincia modProvincia = new ModeloProvincia();

        //
        // GET: /Provincias/

        //public ActionResult Index()
        //{
        //    return View(modProvincia.Listar());
        //}

        public ActionResult Index(string criterioBusqueda = null, int paginaActual = 1)
        {
            ViewBag.totalRegistros = modProvincia.TotalRegistros(criterioBusqueda);
            ViewBag.tamanioPagina = modProvincia.TamanioPagina();
            ViewBag.totalPaginas = (ViewBag.totalRegistros + ViewBag.tamanioPagina - 1) / ViewBag.tamanioPagina;

            int totalPaginas = (ViewBag.totalRegistros + ViewBag.tamanioPagina - 1) / ViewBag.tamanioPagina;

            if (totalPaginas > 0)
            {
                if (paginaActual <= 0)
                {
                    paginaActual = 1;
                    ViewBag.paginaActual = 1;
                }
                else if (paginaActual > totalPaginas)
                {
                    paginaActual = totalPaginas;
                    ViewBag.paginaActual = totalPaginas;
                }
                else
                {
                    ViewBag.paginaActual = paginaActual;
                }
            }

            ViewBag.criterioBusqueda = criterioBusqueda;
            var resp = modProvincia.Listar(criterioBusqueda, paginaActual);
            return View(resp);
        }

        //
        // GET: /Provincias/Details/5

        public ActionResult Details(long id = 0)
        {
            Provincia provincia = modProvincia.Buscar(id);

            ModeloDepartamento modDepto = new ModeloDepartamento();
            provincia.Departamento = modDepto.Buscar(provincia.IdDepartamento);

            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        //
        // GET: /Provincias/Create

        public ActionResult Create()
        {
            ModeloDepartamento modDepto = new ModeloDepartamento();
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion");
            return View();
        }

        //
        // POST: /Provincias/Create

        [HttpPost]
        public ActionResult Create(Provincia provincia)
        {
            provincia.Id = IdentifierGenerator.NewId();
            provincia.IdSesion = 1;
            provincia.FechaUltimaTransaccion = DateTime.Now;
            provincia.FechaRegistro = DateTime.Now;
            provincia.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            provincia.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            provincia.DescripcionEstadoSincronizacion = "";

            ModeloDepartamento modDepto = new ModeloDepartamento();

            if (ModelState.IsValid)
            {
                modProvincia.Crear(provincia);
                return RedirectToAction("Index");
            }
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion", provincia.IdDepartamento);
            return View(provincia);
        }

        //
        // GET: /Provincias/Edit/5

        public ActionResult Edit(long id = 0)
        {
            ModeloDepartamento modDepto = new ModeloDepartamento();

            Provincia provincia = modProvincia.Buscar(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion", provincia.IdDepartamento);
            return View(provincia);
        }

        //
        // POST: /Provincias/Edit/5

        [HttpPost]
        public ActionResult Edit(Provincia provincia)
        {
            ModeloDepartamento modDepto = new ModeloDepartamento();

            if (ModelState.IsValid)
            {
                provincia.IdSesion = 1;
                provincia.FechaUltimaTransaccion = DateTime.Now;
                provincia.FechaRegistro = DateTime.Now;
                provincia.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
                provincia.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                provincia.DescripcionEstadoSincronizacion = "";

                modProvincia.Editar(provincia);
                return RedirectToAction("Index");
            }
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion", provincia.IdDepartamento);
            return View(provincia);
        }

        //
        // GET: /Provincias/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Provincia provincia = modProvincia.Buscar(id);

            ModeloDepartamento modDepto = new ModeloDepartamento();
            provincia.Departamento = modDepto.Buscar(provincia.IdDepartamento);

            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        //
        // POST: /Provincias/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modProvincia.Eliminar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            
        }
    }
}