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


        //[ActionName("MedicosPorIdentificador")]
        //public ActionResult GetMedico(string identificadorBusqueda)
        //{
        //    ModeloMedico modMedico = new ModeloMedico();
        //    Medico myDataXX = modMedico.Buscar(Convert.ToInt64(identificadorBusqueda));
        //    return Json(myDataXX, JsonRequestBehavior.AllowGet);
        //}

        [ActionName("MedicosPorCriterioBusqueda")]
        public ActionResult GetMedicos(string criterioBusqueda)
        {
            ModeloMedico modMedico = new ModeloMedico();

            List<Medico> myData = modMedico.Listar(criterioBusqueda);
            return Json(myData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarMedico()
        {
            ModeloMedico med = new ModeloMedico();
            return PartialView("BusquedaMedico", med.Listar());
        }

        //[HttpPost]
        //public ActionResult ContactUs(string identificador)
        //{    /*Your other processing logic will go here*/
        //    return Json(new
        //    {
        //        Id = identificador
        //    }, JsonRequestBehavior.AllowGet);
        //}

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
            AsignacionMedico asignacionMedico = modAsignacionMedico.Buscar(id);

            ModeloEstablecimientoSalud modEstabSalud = new ModeloEstablecimientoSalud();
            asignacionMedico.EstablecimientoSalud = modEstableSalud.Buscar(asignacionMedico.IdEstablecimientoSalud);

            ModeloMedico modMedico = new ModeloMedico();
            asignacionMedico.Medico = modMedico.Buscar(asignacionMedico.IdMedico);

            if (asignacionMedico == null)
            {
                return HttpNotFound();
            }
            return View(asignacionMedico);
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
            asignacionMedico.Id = IdentifierGenerator.NewId();
            asignacionMedico.IdSesion = 1;
            asignacionMedico.FechaUltimaTransaccion = DateTime.Now;
            asignacionMedico.FechaRegistro = DateTime.Now;
            asignacionMedico.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
            asignacionMedico.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
            asignacionMedico.DescripcionEstadoSincronizacion = "";

            if (ModelState.IsValid)
            {
                modAsignacionMedico.Crear(asignacionMedico);
                return RedirectToAction("Index");
            }

            ModeloDepartamento modDepto = new ModeloDepartamento();
            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion");

            ViewBag.IdMedico = new SelectList(modMedico.Listar(), "Id", "Nombres", asignacionMedico.IdMedico);
            ViewBag.IdEstablecimientoSalud = new SelectList(modEstableSalud.Listar(), "Id", "Codigo", asignacionMedico.IdEstablecimientoSalud);
            return View(asignacionMedico);
        }

        //
        // GET: /AsignacionesMedico/Edit/5

        public ActionResult Edit(long id = 0)
        {
            AsignacionMedico asignacionMedico = modAsignacionMedico.Buscar(id);

            ModeloEstablecimientoSalud modEstableMedico = new ModeloEstablecimientoSalud();
            asignacionMedico.EstablecimientoSalud = modEstableMedico.Buscar(asignacionMedico.IdEstablecimientoSalud);

            ModeloRedSalud modRedSalud = new ModeloRedSalud();
            asignacionMedico.EstablecimientoSalud.RedSalud = modRedSalud.Buscar(asignacionMedico.EstablecimientoSalud.IdRedSalud);

            ModeloMunicipio modMunicipio = new ModeloMunicipio();
            asignacionMedico.EstablecimientoSalud.RedSalud.Municipio = modMunicipio.Buscar(asignacionMedico.EstablecimientoSalud.RedSalud.IdMunicipio);

            ModeloProvincia modProvincia = new ModeloProvincia();
            asignacionMedico.EstablecimientoSalud.RedSalud.Municipio.Provincia = modProvincia.Buscar(asignacionMedico.EstablecimientoSalud.RedSalud.Municipio.IdProvincia);

            ModeloDepartamento modDepto = new ModeloDepartamento();
            asignacionMedico.EstablecimientoSalud.RedSalud.Municipio.Provincia.Departamento = modDepto.Buscar(asignacionMedico.EstablecimientoSalud.RedSalud.Municipio.Provincia.IdDepartamento);

            if (asignacionMedico == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdDepartamento = new SelectList(modDepto.Listar(), "Id", "Descripcion", asignacionMedico.EstablecimientoSalud.RedSalud.Municipio.Provincia.IdDepartamento);
            ViewBag.IdProvincia = new SelectList(modProvincia.Listar().Where(p => p.IdDepartamento == asignacionMedico.EstablecimientoSalud.RedSalud.Municipio.Provincia.IdDepartamento), "Id", "Descripcion", asignacionMedico.EstablecimientoSalud.RedSalud.Municipio.IdProvincia);
            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar().Where(p => p.IdProvincia == asignacionMedico.EstablecimientoSalud.RedSalud.Municipio.IdProvincia), "Id", "Descripcion", asignacionMedico.EstablecimientoSalud.RedSalud.IdMunicipio);
            ViewBag.IdRedSalud = new SelectList(modRedSalud.Listar().Where(p => p.IdMunicipio == asignacionMedico.EstablecimientoSalud.RedSalud.IdMunicipio), "Id", "Nombre", asignacionMedico.EstablecimientoSalud.IdRedSalud);
            ViewBag.cboIdEstablecimientoSalud = new SelectList(modEstableSalud.Listar().Where(p => p.IdRedSalud == asignacionMedico.EstablecimientoSalud.IdRedSalud), "Id", "Nombre", asignacionMedico.IdEstablecimientoSalud);
            ViewBag.IdMedico = new SelectList(modMedico.Listar(), "Id", "Id", asignacionMedico.IdMedico);

            Medico med = modMedico.Buscar(asignacionMedico.IdMedico);
            string nombreCompleto = med.Nombres + " ";
            nombreCompleto += med.PrimerApellido != null ? med.PrimerApellido + " " : "";
            nombreCompleto += med.SegundoApellido != null ? med.SegundoApellido + " " : "";
            nombreCompleto += med.TercerApellido != null ? med.TercerApellido : "";
            ViewBag.NombreCompleto = nombreCompleto.Trim();

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
                asignacionMedico.FechaUltimaTransaccion = DateTime.Now;
                asignacionMedico.FechaRegistro = DateTime.Now;
                asignacionMedico.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
                asignacionMedico.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                asignacionMedico.DescripcionEstadoSincronizacion = "";

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

            ModeloEstablecimientoSalud modEstabSalud = new ModeloEstablecimientoSalud();
            asignacionMedico.EstablecimientoSalud = modEstableSalud.Buscar(asignacionMedico.IdEstablecimientoSalud);

            ModeloMedico modMedico = new ModeloMedico();
            asignacionMedico.Medico = modMedico.Buscar(asignacionMedico.IdMedico);

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

        [ActionName("RedesSaludPorMunicipio")]
        public ActionResult GetRedesSaludPorMunicipio(string id)
        {
            ModeloRedSalud modRedSalud = new ModeloRedSalud();

            List<RedSalud> Datos = modRedSalud.GetRedesSaludPorMunicipio(id);
            var myData = (from d in Datos select new { d.Id, Descripcion = d.Nombre });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }

        [ActionName("EstablecimientosSaludPorRedSalud")]
        public ActionResult GetEstablecimientosSaludPorRedSalud(string id)
        {
            List<EstablecimientoSalud> Datos = modEstableSalud.GetEstablecimientosSaludPorRedSalud(Convert.ToInt64(id));
            var myData = (from d in Datos select new { d.Id, Descripcion = d.Nombre });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }
        
    }
}