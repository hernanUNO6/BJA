using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bja.Entidades;
using Bja.AccesoDatos;

namespace Bja.Central.Web.Controllers
{
    public class AsignacionesMedicoController : Controller
    {
        private BjaContext db = new BjaContext();

        //
        // GET: /AsignacionesMedico/

        public ActionResult Index()
        {
            var asignacionesmedico = db.AsignacionesMedico.Include(a => a.Medico).Include(a => a.EstablecimientoSalud);
            return View(asignacionesmedico.ToList());
        }

        //
        // GET: /AsignacionesMedico/Details/5

        public ActionResult Details(long id = 0)
        {
            AsignacionMedico asignacionmedico = db.AsignacionesMedico.Find(id);
            if (asignacionmedico == null)
            {
                return HttpNotFound();
            }
            return View(asignacionmedico);
        }

        //
        // GET: /AsignacionesMedico/Create

        public ActionResult Create()
        {
            ViewBag.IdMedico = new SelectList(db.Medicos, "Id", "Nombres");
            ViewBag.IdEstablecimientoSalud = new SelectList(db.EstablecimientosSalud, "Id", "Codigo");
            return View();
        }

        //
        // POST: /AsignacionesMedico/Create

        [HttpPost]
        public ActionResult Create(AsignacionMedico asignacionmedico)
        {
            if (ModelState.IsValid)
            {
                db.AsignacionesMedico.Add(asignacionmedico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMedico = new SelectList(db.Medicos, "Id", "Nombres", asignacionmedico.IdMedico);
            ViewBag.IdEstablecimientoSalud = new SelectList(db.EstablecimientosSalud, "Id", "Codigo", asignacionmedico.IdEstablecimientoSalud);
            return View(asignacionmedico);
        }

        //
        // GET: /AsignacionesMedico/Edit/5

        public ActionResult Edit(long id = 0)
        {
            AsignacionMedico asignacionmedico = db.AsignacionesMedico.Find(id);
            if (asignacionmedico == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMedico = new SelectList(db.Medicos, "Id", "Nombres", asignacionmedico.IdMedico);
            ViewBag.IdEstablecimientoSalud = new SelectList(db.EstablecimientosSalud, "Id", "Codigo", asignacionmedico.IdEstablecimientoSalud);
            return View(asignacionmedico);
        }

        //
        // POST: /AsignacionesMedico/Edit/5

        [HttpPost]
        public ActionResult Edit(AsignacionMedico asignacionmedico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asignacionmedico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMedico = new SelectList(db.Medicos, "Id", "Nombres", asignacionmedico.IdMedico);
            ViewBag.IdEstablecimientoSalud = new SelectList(db.EstablecimientosSalud, "Id", "Codigo", asignacionmedico.IdEstablecimientoSalud);
            return View(asignacionmedico);
        }

        //
        // GET: /AsignacionesMedico/Delete/5

        public ActionResult Delete(long id = 0)
        {
            AsignacionMedico asignacionmedico = db.AsignacionesMedico.Find(id);
            if (asignacionmedico == null)
            {
                return HttpNotFound();
            }
            return View(asignacionmedico);
        }

        //
        // POST: /AsignacionesMedico/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            AsignacionMedico asignacionmedico = db.AsignacionesMedico.Find(id);
            db.AsignacionesMedico.Remove(asignacionmedico);
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