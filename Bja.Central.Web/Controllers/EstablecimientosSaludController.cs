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
        //private ModeloMunicipio modMunicipio = new ModeloMunicipio();
        private ModeloRedSalud modRedSalud = new ModeloRedSalud();

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

            ModeloMunicipio modMunicipio = new ModeloMunicipio();
            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion");

            ViewBag.IdRedSalud = new SelectList(modRedSalud.Listar(), "Id", "Nombre");
            return View();
        }

        //
        // POST: /EstablecimientosSalud/Create

        [HttpPost]
        public ActionResult Create(EstablecimientoSalud estableSalud)
        {
            estableSalud.Id = IdentifierGenerator.NewId();
            estableSalud.IdSesion = 1;
            estableSalud.FechaUltimaTransaccion = DateTime.Now;
            estableSalud.FechaRegistro = DateTime.Now;
            estableSalud.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;

            if (ModelState.IsValid)
            {
                modEstableSalud.Crear(estableSalud);
                return RedirectToAction("Index");
            }

            ViewBag.IdRedSalud = new SelectList(modRedSalud.Listar(), "Id", "Nombre", estableSalud.IdRedSalud);
            return View(estableSalud);
        }

        //
        // GET: /EstablecimientosSalud/Edit/5

        public ActionResult Edit(long id = 0)
        {
            EstablecimientoSalud estableSalud = modEstableSalud.Buscar(id);

            estableSalud.RedSalud = modRedSalud.Buscar(estableSalud.IdRedSalud);

            ModeloMunicipio modMunicipio = new ModeloMunicipio();
            estableSalud.RedSalud.Municipio = modMunicipio.Buscar(estableSalud.RedSalud.IdMunicipio);

            ModeloProvincia modProvincia = new ModeloProvincia();
            estableSalud.RedSalud.Municipio.Provincia = modProvincia.Buscar(estableSalud.RedSalud.Municipio.IdProvincia);

            ModeloDepartamento modDepto = new ModeloDepartamento();
            estableSalud.RedSalud.Municipio.Provincia.Departamento = modDepto.Buscar(estableSalud.RedSalud.Municipio.Provincia.IdDepartamento);

            if (estableSalud == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion", estableSalud.RedSalud.Municipio.Provincia.IdDepartamento);
            ViewBag.IdProvincia = new SelectList(modProvincia.Listar().Where(p => p.IdDepartamento == estableSalud.RedSalud.Municipio.Provincia.IdDepartamento), "Id", "Descripcion", estableSalud.RedSalud.Municipio.IdProvincia);
            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar().Where(p => p.IdProvincia == estableSalud.RedSalud.Municipio.IdProvincia), "Id", "Descripcion", estableSalud.RedSalud.IdMunicipio);
            ViewBag.cboIdRedSalud = new SelectList(modRedSalud.Listar().Where(p => p.IdMunicipio == estableSalud.RedSalud.IdMunicipio), "Id", "Nombre", estableSalud.IdRedSalud);
            return View(estableSalud);
        }

        ////
        //// POST: /EstablecimientosSalud/Edit/5

        //[HttpPost]
        //public ActionResult Edit(EstablecimientoSalud estableSalud)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        estableSalud.IdSesion = 1;
        //        estableSalud.FechaUltimaTransaccion = System.DateTime.Now;
        //        estableSalud.FechaRegistro = System.DateTime.Now;

        //        modEstableSalud.Editar(estableSalud);
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion", estableSalud.IdMunicipio);
        //    return View(estableSalud);
        //}

        ////
        //// GET: /EstablecimientosSalud/Delete/5

        //public ActionResult Delete(long id = 0)
        //{
        //    EstablecimientoSalud estableSalud = modEstableSalud.Buscar(id);
        //    if (estableSalud == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(estableSalud);
        //}

        ////
        //// POST: /EstablecimientosSalud/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    modEstableSalud.Eliminar(id);
        //    return RedirectToAction("Index");
        //}

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

        [ActionName("RedesSaludPorMunicipio")]
        public ActionResult GetRedesSaludPorMunicipio(string id)
        {
            List<RedSalud> Datos = modRedSalud.GetRedesSaludPorMunicipio(id);
            var myData = (from d in Datos select new { d.Id, Descripcion = d.Nombre });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }

    }
}