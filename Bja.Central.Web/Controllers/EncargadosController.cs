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
    public class EncargadosController : Controller
    {
        private ModeloEncargado modEncargado = new ModeloEncargado();
        //
        // GET: /Encargados/

        public ActionResult Index()
        {
            ViewBag.TiposEncargados = TipoEncargado.GetNames(typeof(TipoEncargado));
            ViewBag.TiposDI = TipoDocumentoIdentidad.GetNames(typeof(TipoDocumentoIdentidad));
            //ViewBag.TiposEstadosRegistros = TipoEstadoRegistro.GetNames(typeof(TipoEstadoRegistro));
            return View(modEncargado.Listar());
        }

        //
        // GET: /Encargados/Details/5

        public ActionResult Details(long id = 0)
        {
            Encargado encargado = modEncargado.Buscar(id);
            if (encargado == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoEncargado = TipoEncargado.GetNames(typeof(TipoEncargado))[encargado.IdTipoEncargado];
            //ViewBag.TipoDI = TipoDocumentoIdentidad.GetNames(typeof(TipoDocumentoIdentidad))[encargado.IdTipoDocumentoIdentidad];
            //Ojo
            //ViewBag.TipoEstadoRegistro = TipoEstadoRegistro.GetNames(typeof(TipoEstadoRegistro))[encargado.IdTipoEstadoRegistro];
            return View(encargado);
        }

        //
        // GET: /Encargados/Create

        public ActionResult Create()
        {
            ViewBag.cboTipoEncargado = (from TipoEncargado e in Enum.GetValues(typeof(TipoEncargado))
                                        select new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() });
            ViewBag.cboTipoDI = (from TipoDocumentoIdentidad e in Enum.GetValues(typeof(TipoDocumentoIdentidad))
                                 select new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() });
            return View();
        }

        //
        // POST: /Encargados/Create

        [HttpPost]
        public ActionResult Create(Encargado encargado)
        {
            encargado.IdSesion = 1;
            encargado.FechaUltimaTransaccion = System.DateTime.Now;
            encargado.FechaRegistro = System.DateTime.Now;
            //encargado.IdTipoEstadoRegistro = (long)TipoEstadoRegistro.Vigente;

            if (ModelState.IsValid)
            {
                modEncargado.Crear(encargado);
                return RedirectToAction("Index");
            }
            return View(encargado);
        }

        //
        // GET: /Encargados/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Encargado encargado = modEncargado.Buscar(id);
            if (encargado == null)
            {
                return HttpNotFound();
            }
            ViewBag.cboTipoEncargado = (from TipoEncargado e in Enum.GetValues(typeof(TipoEncargado))
                                        select new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() });
            ViewBag.cboTipoDI = (from TipoDocumentoIdentidad e in Enum.GetValues(typeof(TipoDocumentoIdentidad))
                                 select new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() });
            return View(encargado);
        }

        //
        // POST: /Encargados/Edit/5

        [HttpPost]
        public ActionResult Edit(Encargado encargado)
        {
            if (ModelState.IsValid)
            {
                encargado.IdSesion = 1;
                encargado.FechaUltimaTransaccion = System.DateTime.Now;
                encargado.FechaRegistro = System.DateTime.Now;

                modEncargado.Editar(encargado);
                return RedirectToAction("Index");
            }
            return View(encargado);
        }

        //
        // GET: /Encargados/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Encargado encargado = modEncargado.Buscar(id);
            if (encargado == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoEncargado = TipoEncargado.GetNames(typeof(TipoEncargado))[encargado.IdTipoEncargado];
            //ViewBag.TipoDI = TipoDocumentoIdentidad.GetNames(typeof(TipoDocumentoIdentidad))[encargado.IdTipoDocumentoIdentidad];
            //ViewBag.TipoEstadoRegistro = TipoEstadoRegistro.GetNames(typeof(TipoEstadoRegistro))[encargado.IdTipoEstadoRegistro];
            //Ojo

            return View(encargado);
        }

        //
        // POST: /Encargados/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modEncargado.Eliminar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            
        }
    }
}