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
    public class AsignacionesMedicoController : Controller
    {        
        private ModeloEstablecimientoSalud modEstableSalud = new ModeloEstablecimientoSalud();
        private ModeloMedico modMedico = new ModeloMedico();
        private ModeloAsignacionMedico modAsignacionMedico = new ModeloAsignacionMedico();

        //
        // GET: /AsignacionesMedico/

        public ActionResult Index()
        {
            return View(modAsignacionMedico.Listar());
        }

        //
        // GET: /AsignacionesMedico/Details/5

        public ActionResult Details(long id = 0)
        {
            AsignacionMedico asignacionmedico = modAsignacionMedico.Buscar(id);
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
            ModeloDepartamento modDepto = new ModeloDepartamento();
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion");
            
            ViewBag.IdEstablecimientoSalud = new SelectList(modEstableSalud.Listar(), "Id", "Codigo");
            ViewBag.IdMedico = new SelectList(modMedico.Listar(), "Id", "Nombres");
            return View();
        }

        //
        // POST: /AsignacionesMedico/Create

        [HttpPost]
        public ActionResult Create(AsignacionMedico asignacionMedico)
        {
            asignacionMedico.IdSesion = 1;
            asignacionMedico.FechaUltimaTransaccion = System.DateTime.Now;
            asignacionMedico.FechaRegistro = System.DateTime.Now;

            if (ModelState.IsValid)
            {
                modAsignacionMedico.Crear(asignacionMedico);
                return RedirectToAction("Index");
            }

            ViewBag.IdMedico = new SelectList(modMedico.Listar(), "Id", "Nombres", asignacionMedico.IdMedico);
            ViewBag.IdEstablecimientoSalud = new SelectList(modEstableSalud.Listar(), "Id", "Codigo", asignacionMedico.IdEstablecimientoSalud);
            return View(asignacionMedico);
        }

        //
        // GET: /AsignacionesMedico/Edit/5

        public ActionResult Edit(long id = 0)
        {
            AsignacionMedico asignacionMedico = modAsignacionMedico.Buscar(id);
            if (asignacionMedico == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMedico = new SelectList(modMedico.Listar(), "Id", "Nombres", asignacionMedico.IdMedico);
            ViewBag.IdEstablecimientoSalud = new SelectList(modEstableSalud.Listar(), "Id", "Codigo", asignacionMedico.IdEstablecimientoSalud);
            return View(asignacionMedico);
        }

        //
        // POST: /AsignacionesMedico/Edit/5

        [HttpPost]
        public ActionResult Edit(AsignacionMedico asignacionMedico)
        {
            if (ModelState.IsValid)
            {
                asignacionMedico.IdSesion = 1;
                asignacionMedico.FechaUltimaTransaccion = System.DateTime.Now;
                asignacionMedico.FechaRegistro = System.DateTime.Now;

                modAsignacionMedico.Editar(asignacionMedico);
                return RedirectToAction("Index");
            }
            ViewBag.IdMedico = new SelectList(modMedico.Listar(), "Id", "Nombres", asignacionMedico.IdMedico);
            ViewBag.IdEstablecimientoSalud = new SelectList(modEstableSalud.Listar(), "Id", "Codigo", asignacionMedico.IdEstablecimientoSalud);
            return View(asignacionMedico);
        }

        //
        // GET: /AsignacionesMedico/Delete/5

        public ActionResult Delete(long id = 0)
        {
            AsignacionMedico asignacionMedico = modAsignacionMedico.Buscar(id);
            if (asignacionMedico == null)
            {
                return HttpNotFound();
            }
            return View(asignacionMedico);
        }

        //
        // POST: /AsignacionesMedico/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modAsignacionMedico.Eliminar(id);
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
            ModeloMunicipio modMunicipio = new ModeloMunicipio();

            List<Municipio> Datos = modMunicipio.GetMunicipiosPorProvincias(id);
            var myData = (from d in Datos select new { d.Id, d.Descripcion });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }

        [ActionName("EstablecimientosSaludPorMunicipio")]
        public ActionResult GetEstablecimientosSaludPorMunicipio(string id)
        {
            List<EstablecimientoSalud> Datos = modEstableSalud.GetEstablecimientosSaludPorMunicipio(id);
            var myData = (from d in Datos select new { d.Id, d.Nombre });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }
        
    }
}