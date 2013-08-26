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
    public class RedesSaludController : Controller
    {
        //private BjaContext db = new BjaContext();
        private ModeloRedSalud modRedSalud = new ModeloRedSalud();
        private ModeloMunicipio modMunicipio = new ModeloMunicipio();

        //
        // GET: /RedesSalud/

        public ActionResult Index()
        {
            return View(modRedSalud.Listar());
        }

        //
        // GET: /RedesSalud/Details/5

        public ActionResult Details(long id = 0)
        {
            RedSalud redSalud = modRedSalud.Buscar(id);
            if (redSalud == null)
            {
                return HttpNotFound();
            }
            return View(redSalud);
        }

        //
        // GET: /RedesSalud/Create

        public ActionResult Create()
        {
            ModeloDepartamento modDepto = new ModeloDepartamento();
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion");

            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion");
            return View();
        }

        //
        // POST: /RedesSalud/Create

        [HttpPost]
        public ActionResult Create(RedSalud redsalud)
        {
            redsalud.IdSesion = 1;
            redsalud.FechaUltimaTransaccion = System.DateTime.Now;
            redsalud.FechaRegistro = System.DateTime.Now;

            if (ModelState.IsValid)
            {
                modRedSalud.Crear(redsalud);
                return RedirectToAction("Index");
            }

            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion", redsalud.IdMunicipio);
            return View(redsalud);
        }

        //
        // GET: /RedesSalud/Edit/5

        public ActionResult Edit(long id = 0)
        {
            RedSalud redsalud = modRedSalud.Buscar(id);

            redsalud.Municipio = modMunicipio.Buscar(redsalud.IdMunicipio);

            ModeloProvincia modProvincia = new ModeloProvincia();
            redsalud.Municipio.Provincia = modProvincia.Buscar(redsalud.Municipio.IdProvincia);

            ModeloDepartamento modDepto = new ModeloDepartamento();
            redsalud.Municipio.Provincia.Departamento = modDepto.Buscar(redsalud.Municipio.Provincia.IdDepartamento);

            if (redsalud == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion", redsalud.Municipio.Provincia.IdDepartamento);
            ViewBag.IdProvincia = new SelectList(modProvincia.Listar().Where(p => p.IdDepartamento == redsalud.Municipio.Provincia.IdDepartamento), "Id", "Descripcion", redsalud.Municipio.IdProvincia);
            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar().Where(p => p.IdProvincia == redsalud.Municipio.IdProvincia), "Id", "Descripcion", redsalud.IdMunicipio);
            return View(redsalud);
        }

        //
        // POST: /RedesSalud/Edit/5

        [HttpPost]
        public ActionResult Edit(RedSalud redsalud)
        {
            if (ModelState.IsValid)
            {
                redsalud.IdSesion = 1;
                redsalud.FechaUltimaTransaccion = System.DateTime.Now;
                redsalud.FechaRegistro = System.DateTime.Now;

                modRedSalud.Editar(redsalud);
                return RedirectToAction("Index");
            }
            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion", redsalud.IdMunicipio);
            return View(redsalud);
        }

        //
        // GET: /RedesSalud/Delete/5

        public ActionResult Delete(long id = 0)
        {
            RedSalud redSalud = modRedSalud.Buscar(id);
            if (redSalud == null)
            {
                return HttpNotFound();
            }
            return View(redSalud);
        }

        //
        // POST: /RedesSalud/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modRedSalud.Eliminar(id);
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