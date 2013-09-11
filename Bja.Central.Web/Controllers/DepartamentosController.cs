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
    public class DepartamentosController : Controller
    {
        private ModeloDepartamento modDepto = new ModeloDepartamento();

        //
        // GET: /Departamentos/

        public ActionResult Index()
        {
            return View(modDepto.Listar());
        }

        //
        // GET: /Departamentos/Details/5

        public ActionResult Details(long id = 0)
        {
            Departamento depto = modDepto.Buscar(id);
            if (depto == null)
            {
                return HttpNotFound();
            }
            return View(depto);
        }

        //
        // GET: /Departamentos/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Departamentos/Create

        [HttpPost]
        public ActionResult Create(Departamento depto)
        {
            if (ModelState.IsValid)
            {
                modDepto.Crear(depto);
                return RedirectToAction("Index");
            }
            return View(depto);
        }

        //
        // GET: /Departamentos/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Departamento depto = modDepto.Buscar(id);
            if (depto == null)
            {
                return HttpNotFound();
            }
            
            return View(depto);
        }

        //
        // POST: /Departamentos/Edit/5

        [HttpPost]
        public ActionResult Edit(Departamento depto)
        {
            if (ModelState.IsValid)
            {
                modDepto.Editar(depto);
                return RedirectToAction("Index");
            }
            return View(depto);
        }

        //
        // GET: /Departamentos/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Departamento depto = modDepto.Buscar(id);
            if (depto == null)
            {
                return HttpNotFound();
            }
            return View(depto);
        }

        //
        // POST: /Departamentos/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modDepto.Eliminar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            
        }
    }
}