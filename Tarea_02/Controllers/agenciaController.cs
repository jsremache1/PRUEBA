using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tarea_02.Models;

namespace Tarea_02.Controllers
{
    [Authorize]
    public class agenciaController : Controller
    {
        private MatriculacionFinalBD_Context db = new MatriculacionFinalBD_Context();

        // GET: agencia
        public ActionResult Index()
        {
            return View(db.agencia.ToList());
        }

        // GET: agencia/Details/5
        [Authorize(Roles = "Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agencia agencia = db.agencia.Find(id);
            if (agencia == null)
            {
                return HttpNotFound();
            }
            return View(agencia);
        }

        // GET: agencia/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: agencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "id,descripcion,direccion,telefono,horainicio,horafin,foto")] agencia agencia)
        {
            if (ModelState.IsValid)
            {
                db.agencia.Add(agencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agencia);
        }

        // GET: agencia/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agencia agencia = db.agencia.Find(id);
            if (agencia == null)
            {
                return HttpNotFound();
            }
            return View(agencia);
        }

        // POST: agencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "id,descripcion,direccion,telefono,horainicio,horafin,foto")] agencia agencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agencia);
        }

        // GET: agencia/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agencia agencia = db.agencia.Find(id);
            if (agencia == null)
            {
                return HttpNotFound();
            }
            return View(agencia);
        }

        // POST: agencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            agencia agencia = db.agencia.Find(id);
            db.agencia.Remove(agencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
