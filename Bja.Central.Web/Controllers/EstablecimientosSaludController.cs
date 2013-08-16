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
    public class EstablecimientosSaludController : Controller
    {
        private ModeloEstablecimientoSalud modEstableSalud = new ModeloEstablecimientoSalud();
        private ModeloMunicipio modMunicipio = new ModeloMunicipio();

        //
        // GET: /EstablecimientosSalud/

        public ActionResult Index()
        {
            return View(modEstableSalud.Listar());
        }

        //
        // GET: /EstablecimientosSalud/Details/5

        public ActionResult Details(long id = 0)
        {
            EstablecimientoSalud estableSalud = modEstableSalud.Buscar(id);
            if (estableSalud == null)
            {
                return HttpNotFound();
            }
            return View(estableSalud);
        }

        //
        // GET: /EstablecimientosSalud/Create

        public ActionResult Create()
        {
            ModeloDepartamento modDepto = new ModeloDepartamento();
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion");

            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion");
            return View();
        }

        //
        // POST: /EstablecimientosSalud/Create

        [HttpPost]
        public ActionResult Create(EstablecimientoSalud estableSalud)
        {
            estableSalud.IdSesion = 1;
            estableSalud.FechaUltimaTransaccion = System.DateTime.Now;
            estableSalud.FechaRegistro = System.DateTime.Now;

            if (ModelState.IsValid)
            {
                modEstableSalud.Crear(estableSalud);
                return RedirectToAction("Index");
            }

            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion", estableSalud.IdMunicipio);
            return View(estableSalud);
        }

        //
        // GET: /EstablecimientosSalud/Edit/5

        public ActionResult Edit(long id = 0)
        {
            EstablecimientoSalud estableSalud = modEstableSalud.Buscar(id);
            if (estableSalud == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion", estableSalud.IdMunicipio);
            return View(estableSalud);
        }

        //
        // POST: /EstablecimientosSalud/Edit/5

        [HttpPost]
        public ActionResult Edit(EstablecimientoSalud estableSalud)
        {
            if (ModelState.IsValid)
            {
                estableSalud.IdSesion = 1;
                estableSalud.FechaUltimaTransaccion = System.DateTime.Now;
                estableSalud.FechaRegistro = System.DateTime.Now;

                modEstableSalud.Editar(estableSalud);
                return RedirectToAction("Index");
            }
            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion", estableSalud.IdMunicipio);
            return View(estableSalud);
        }

        //
        // GET: /EstablecimientosSalud/Delete/5

        public ActionResult Delete(long id = 0)
        {
            EstablecimientoSalud estableSalud = modEstableSalud.Buscar(id);
            if (estableSalud == null)
            {
                return HttpNotFound();
            }
            return View(estableSalud);
        }

        //
        // POST: /EstablecimientosSalud/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modEstableSalud.Eliminar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            
        }

        [ActionName("ProvinciasPorDepartamento")]
        public ActionResult GetProvinciasPorDepartamento(string id)
        {
            ModeloProvincia modProvincia = new ModeloProvincia();

            List<Provincia> Datos = modProvincia.GetProvinciasPorDepartamento(id);
            var myData = (from d in Datos select new { d.Id, d.Descripcion });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }

        [ActionName("MunicipiosPorProvincia")]
        public ActionResult GetMunicipiosPorProvincias(string id)
        {
            List<Municipio> Datos = modMunicipio.GetMunicipiosPorProvincias(id);
            var myData = (from d in Datos select new { d.Id, d.Descripcion });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }
    }
}