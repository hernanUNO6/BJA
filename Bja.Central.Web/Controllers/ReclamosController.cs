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
    public class ReclamosController : Controller
    {
        private ModeloReclamo modReclamo = new ModeloReclamo();

        //
        // GET: /Reclamos/

        public ActionResult Index()
        {
            ViewBag.TiposReclamos = TipoReclamo.GetNames(typeof(TipoReclamo));
            return View(modReclamo.Listar());
        }

        //
        // GET: /Reclamos/Details/5

        public ActionResult Details(long id = 0)
        {
            Reclamo reclamo = modReclamo.Buscar(id);
            if (reclamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoReclamo = TipoReclamo.GetNames(typeof(TipoReclamo))[reclamo.IdTipoReclamo];
            return View(reclamo);
        }

        //
        // GET: /Reclamos/Create

        public ActionResult Create()
        {
            ViewBag.cboTipoReclamo = (from TipoReclamo e in Enum.GetValues(typeof(TipoReclamo))
                                      select new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() });
            return View();
        }

        //
        // POST: /Reclamos/Create

        [HttpPost]
        public ActionResult Create(Reclamo reclamo)
        {
            reclamo.Id = IdentifierGenerator.NewId();
            reclamo.IdSesion = 1;
            reclamo.FechaUltimaTransaccion = DateTime.Now;
            reclamo.FechaRegistro = DateTime.Now;
            reclamo.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            reclamo.DescripcionEstadoSincronizacion = "";

            if (ModelState.IsValid)
            {
                modReclamo.Crear(reclamo);
                return RedirectToAction("Index");
            }
            return View(reclamo);
        }

        //
        // GET: /Reclamos/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Reclamo reclamo = modReclamo.Buscar(id);
            if (reclamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.cboTipoReclamo = (from TipoReclamo e in Enum.GetValues(typeof(TipoReclamo))
                                      select new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() });
            return View(reclamo);
        }

        //
        // POST: /Reclamos/Edit/5

        [HttpPost]
        public ActionResult Edit(Reclamo reclamo)
        {
            if (ModelState.IsValid)
            {
                reclamo.IdSesion = 1;
                reclamo.FechaUltimaTransaccion = DateTime.Now;
                reclamo.FechaRegistro = DateTime.Now;
                reclamo.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                reclamo.DescripcionEstadoSincronizacion = "";

                modReclamo.Editar(reclamo);
                return RedirectToAction("Index");
            }
            return View(reclamo);
        }

        //
        // GET: /Reclamos/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Reclamo reclamo = modReclamo.Buscar(id);
            if (reclamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoReclamo = TipoReclamo.GetNames(typeof(TipoReclamo))[reclamo.IdTipoReclamo];
            return View(reclamo);
        }

        //
        // POST: /Reclamos/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modReclamo.Eliminar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            
        }
    }
}