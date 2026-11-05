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
    public class vehiculoController : Controller
    {
        private MatriculacionFinalBD_Context db = new MatriculacionFinalBD_Context();

        // GET: vehiculo
        public ActionResult Index()
        {
            var vehiculo = db.vehiculo.Include(v => v.color1).Include(v => v.marca1);
            return View(vehiculo.ToList());

        }

        // GET: vehiculo/Details/5
        [Authorize(Roles = "Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehiculo vehiculo = db.vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: vehiculo/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            ViewBag.color = new SelectList(db.color, "id", "descripcion");
            ViewBag.marca = new SelectList(db.marca, "id", "descripcion");
            return View();
        }

        // POST: vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "id,placa,marca,motor,chasis,combustible,anio,color,foto,avaluo")] vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.vehiculo.Add(vehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.color = new SelectList(db.color, "id", "descripcion", vehiculo.color);
            ViewBag.marca = new SelectList(db.marca, "id", "descripcion", vehiculo.marca);
            return View(vehiculo);
        }

        // GET: vehiculo/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehiculo vehiculo = db.vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.color = new SelectList(db.color, "id", "descripcion", vehiculo.color);
            ViewBag.marca = new SelectList(db.marca, "id", "descripcion", vehiculo.marca);
            return View(vehiculo);
        }

        // POST: vehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "id,placa,marca,motor,chasis,combustible,anio,color,foto,avaluo")] vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.color = new SelectList(db.color, "id", "descripcion", vehiculo.color);
            ViewBag.marca = new SelectList(db.marca, "id", "descripcion", vehiculo.marca);
            return View(vehiculo);
        }

        // GET: vehiculo/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehiculo vehiculo = db.vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            vehiculo vehiculo = db.vehiculo.Find(id);
            db.vehiculo.Remove(vehiculo);
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
