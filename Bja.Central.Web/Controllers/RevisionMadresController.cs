using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bja.Entidades;
using Bja.Modelo;

using Bja.AccesoDatos;

namespace Bja.Central.Web.Controllers
{
    public class RevisionMadresController : Controller
    {
        private ModeloRevisionManual modReclamo = new ModeloRevisionManual();

        private BjaContext db = new BjaContext();

        [ActionName("BuscarSemejantes")]
        public ActionResult GetBuscarSemejantes(ParametrosBusqueda parBusqueda)
        {
            ModeloMadreTemporal modMadreTemp = new ModeloMadreTemporal();

            List<Madre> Datos = modMadreTemp.BuscarSemejantes(parBusqueda);
            var myData = (from d in Datos select new { llavePrimaria = d.Id, d.Nombres, d.PrimerApellido, d.SegundoApellido, d.TercerApellido, d.NombreCompleto, d.DocumentoIdentidad, d.FechaNacimiento, d.LocalidadNacimiento });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }

        //[ActionName("BuscarSemejantes")]
        //public ActionResult GetBuscarSemejantes(string id)
        //{
        //    ModeloMadreTemporal modMadreTemp = new ModeloMadreTemporal();

        //    List<Madre> Datos = modMadreTemp.BuscarSemejantes(id);
        //    var myData = (from d in Datos select new { d.Nombres, d.PrimerApellido, d.NombreCompleto });
        //    return Json(myData, JsonRequestBehavior.AllowGet);
        //}

        public string Prueba()
        {
            return "Hay algo";
        }

        //
        // GET: /RevisionMadres/

        public ActionResult Index()
        {
            return View(modReclamo.Listar());
        }

        public ActionResult Temporal()
        {
            return View(modReclamo.ListarTempo());
        }

        //
        // GET: /RevisionMadres/Details/5

        public ActionResult Details(long id = 0)
        {
            Madre madre = modReclamo.Buscar(id);
            if (madre == null)
            {
                return HttpNotFound();
            }
            return View(madre);
        }

        //
        // GET: /RevisionMadres/Create

        public ActionResult Create()
        {
            ViewBag.IdDepartamento = new SelectList(db.Departamentos, "Id", "Descripcion");
            ViewBag.IdProvincia = new SelectList(db.Provincias, "Id", "Descripcion");
            ViewBag.IdMunicipio = new SelectList(db.Municipios, "Id", "Descripcion");
            return View();
        }

        //
        // POST: /RevisionMadres/Create

        [HttpPost]
        public ActionResult Create(Madre madre)
        {
            if (ModelState.IsValid)
            {
                db.Madres.Add(madre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDepartamento = new SelectList(db.Departamentos, "Id", "DescripcionEstadoSincronizacion", madre.IdDepartamento);
            ViewBag.IdProvincia = new SelectList(db.Provincias, "Id", "DescripcionEstadoSincronizacion", madre.IdProvincia);
            ViewBag.IdMunicipio = new SelectList(db.Municipios, "Id", "DescripcionEstadoSincronizacion", madre.IdMunicipio);
            return View(madre);
        }

        //
        // GET: /RevisionMadres/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Madre madre = db.Madres.Find(id);
            if (madre == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepartamento = new SelectList(db.Departamentos, "Id", "DescripcionEstadoSincronizacion", madre.IdDepartamento);
            ViewBag.IdProvincia = new SelectList(db.Provincias, "Id", "DescripcionEstadoSincronizacion", madre.IdProvincia);
            ViewBag.IdMunicipio = new SelectList(db.Municipios, "Id", "DescripcionEstadoSincronizacion", madre.IdMunicipio);
            return View(madre);
        }

        //
        // POST: /RevisionMadres/Edit/5

        [HttpPost]
        public ActionResult Edit(Madre madre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(madre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDepartamento = new SelectList(db.Departamentos, "Id", "DescripcionEstadoSincronizacion", madre.IdDepartamento);
            ViewBag.IdProvincia = new SelectList(db.Provincias, "Id", "DescripcionEstadoSincronizacion", madre.IdProvincia);
            ViewBag.IdMunicipio = new SelectList(db.Municipios, "Id", "DescripcionEstadoSincronizacion", madre.IdMunicipio);
            return View(madre);
        }

        //
        // GET: /RevisionMadres/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Madre madre = db.Madres.Find(id);
            if (madre == null)
            {
                return HttpNotFound();
            }
            return View(madre);
        }

        //
        // POST: /RevisionMadres/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Madre madre = db.Madres.Find(id);
            db.Madres.Remove(madre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}