﻿using System;
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
    public class MunicipiosController : Controller
    {
        private ModeloMunicipio modMunicipio = new ModeloMunicipio();
        private ModeloProvincia modProvincia = new ModeloProvincia();

        //
        // GET: /Municipios/

        //public ActionResult Index()
        //{
        //    return View(modMunicipio.Listar());
        //}

        public ActionResult Index(string criterioBusqueda = null, int paginaActual = 1)
        {
            ViewBag.totalRegistros = modMunicipio.TotalRegistros(criterioBusqueda);
            ViewBag.tamanioPagina = modMunicipio.TamanioPagina();
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
            var resp = modMunicipio.Listar(criterioBusqueda, paginaActual);
            return View(resp);
        }

        //
        // GET: /Municipios/Details/5

        public ActionResult Details(long id = 0)
        {
            Municipio municipio = modMunicipio.Buscar(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        //
        // GET: /Municipios/Create

        public ActionResult Create()
        {
            ModeloDepartamento modDepto = new ModeloDepartamento();
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion");

            ViewBag.IdProvincia = new SelectList(modProvincia.Listar(), "Id", "Descripcion");
            return View();
        }

        //
        // POST: /Municipios/Create

        [HttpPost]
        public ActionResult Create(Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                modMunicipio.Crear(municipio);
                return RedirectToAction("Index");
            }
            ViewBag.IdDepartamento = new SelectList(modProvincia.Listar(), "Id", "Descripcion", municipio.IdProvincia);
            return View(municipio);
        }

        //
        // GET: /Municipios/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Municipio municipio = modMunicipio.Buscar(id);

            ModeloProvincia modProvincia = new ModeloProvincia();
            municipio.Provincia = modProvincia.Buscar(municipio.IdProvincia);

            ModeloDepartamento modDepto = new ModeloDepartamento();
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion", municipio.Provincia.IdDepartamento);

            if (municipio == null)
            {
                return HttpNotFound();
            }
            ViewBag.cboIdProvincia = new SelectList(modProvincia.Listar().Where(p => p.IdDepartamento == municipio.Provincia.IdDepartamento), "Id", "Descripcion", municipio.IdProvincia);
            return View(municipio);
        }

        //
        // POST: /Municipios/Edit/5

        [HttpPost]
        public ActionResult Edit(Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                modMunicipio.Editar(municipio);
                return RedirectToAction("Index");
            }
            ViewBag.IdProvincia = new SelectList(modProvincia.Listar(), "Id", "Descripcion", municipio.IdProvincia);
            return View(municipio);
        }

        //
        // GET: /Municipios/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Municipio municipio = modMunicipio.Buscar(id);

            ModeloProvincia modProvincia = new ModeloProvincia();
            municipio.Provincia = modProvincia.Buscar(municipio.IdProvincia);

            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        //
        // POST: /Municipios/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modMunicipio.Eliminar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            
        }

        [ActionName("ProvinciasPorDepartamento")]
        public ActionResult GetProvinciasPorDepartamento(string id)
        {
            List<Provincia> Datos = modProvincia.GetProvinciasPorDepartamento(id);
            var myData = (from d in Datos select new { d.Id, d.Descripcion });            
            return Json(myData, JsonRequestBehavior.AllowGet);
        }
    }
}