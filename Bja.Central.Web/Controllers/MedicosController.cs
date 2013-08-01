﻿using System;
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
    public class MedicosController : Controller
    {
        private ModeloMedico modMedico = new ModeloMedico();
        //
        // GET: /Medicos/

        public ActionResult Index()
        {
            return View(modMedico.Listar());
        }

        //
        // GET: /Medicos/Details/5

        public ActionResult Details(long id = 0)
        {
            Medico medico = modMedico.Buscar(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        //
        // GET: /Medicos/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Medicos/Create

        [HttpPost]
        public ActionResult Create(Medico medico)
        {
            if (ModelState.IsValid)
            {
                modMedico.Crear(medico);
                return RedirectToAction("Index");
            }
            return View(medico);
        }

        //
        // GET: /Medicos/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Medico medico = modMedico.Buscar(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        //
        // POST: /Medicos/Edit/5

        [HttpPost]
        public ActionResult Edit(Medico medico)
        {
            if (ModelState.IsValid)
            {
                modMedico.Editar(medico);
                return RedirectToAction("Index");
            }
            return View(medico);
        }

        //
        // GET: /Medicos/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Medico medico = modMedico.Buscar(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        //
        // POST: /Medicos/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            modMedico.Eliminar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //base.Dispose(disposing);
        }
    }
}