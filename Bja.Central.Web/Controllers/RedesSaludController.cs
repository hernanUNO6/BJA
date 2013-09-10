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

        //public ActionResult Index()
        //{
        //    return View(modRedSalud.Listar());
        //}

        public ActionResult Index(string criterioBusqueda = null, int paginaActual = 1)
        {
            ViewBag.totalRegistros = modRedSalud.TotalRegistros(criterioBusqueda);
            ViewBag.tamanioPagina = modRedSalud.TamanioPagina();
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
            var resp = modRedSalud.Listar(criterioBusqueda, paginaActual);
            return View(resp);
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
        public ActionResult Create(RedSalud redSalud)
        {
            if (ModelState.IsValid)
            {
                modRedSalud.Crear(redSalud);
                return RedirectToAction("Index");
            }

            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion", redSalud.IdMunicipio);
            return View(redSalud);
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
            ViewBag.cboIdMunicipio = new SelectList(modMunicipio.Listar().Where(p => p.IdProvincia == redsalud.Municipio.IdProvincia), "Id", "Descripcion", redsalud.IdMunicipio);
            return View(redsalud);
        }

        //
        // POST: /RedesSalud/Edit/5

        [HttpPost]
        public ActionResult Edit(RedSalud redSalud)
        {
            if (ModelState.IsValid)
            {
                redSalud.IdSesion = 1;
                redSalud.FechaUltimaTransaccion = DateTime.Now;
                redSalud.FechaRegistro = DateTime.Now;
                redSalud.EstadoRegistro = TipoEstadoRegistro.VigenteRegistroModificado;
                redSalud.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                redSalud.DescripcionEstadoSincronizacion = "";

                modRedSalud.Editar(redSalud);
                return RedirectToAction("Index");
            }
            ViewBag.IdMunicipio = new SelectList(modMunicipio.Listar(), "Id", "Descripcion", redSalud.IdMunicipio);
            return View(redSalud);
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