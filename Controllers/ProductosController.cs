using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class ProductosController : Controller
    {
        private ProductosEntities db = new ProductosEntities();

        //Retorna la vista con todos los registros
        // GET: Productos
        public ActionResult Index()
        {
            if (Session["username"] == null)
                return Redirect("~/Error/Index");
            return View(db.tbl_productos.ToList());
        }

        //Retorna el formulario de registro
        // GET: Productos/Create
        public ActionResult Create()
        {
            if (Session["username"] == null)
                return Redirect("~/Error/Index");
            return View();
        }

        //Se reciben los datos del formulario, si los datos son correctos se guardan y se redirige a la vista principal
        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fld_idProducto,fld_nombreProducto,fld_precio,fld_cantidad")] tbl_productos tbl_productos)
        {
            if (Session["username"] == null)
                return Redirect("~/Error/Index");
            if (ModelState.IsValid)
            {
                db.tbl_productos.Add(tbl_productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_productos);
        }

        //Se recibe un id de un registro y  se carga un formulario cargado con datos pertenecientes a ese id
        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null)
                return Redirect("~/Error/Index");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_productos tbl_productos = db.tbl_productos.Find(id);
            if (tbl_productos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_productos);
        }

        //Se reciben los datos del formulario, si los datos son correctos se guardan y se redirige a la vista principal
        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fld_idProducto,fld_nombreProducto,fld_precio,fld_cantidad")] tbl_productos tbl_productos)
        {
            if (Session["username"] == null)
                return Redirect("~/Error/Index");
            if (ModelState.IsValid)
            {
                db.Entry(tbl_productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_productos);
        }

        //Se recibe un id de un registro y  se carga un formulario cargado con datos pertenecientes a ese id
        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["username"] == null)
                return Redirect("~/Error/Index");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_productos tbl_productos = db.tbl_productos.Find(id);
            if (tbl_productos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_productos);
        }

        //Esto es un mensaje de confirmación, si la respuesta es afirmativa se procede a eliminar el registro
        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["username"] == null)
                return Redirect("~/Error/Index");
            tbl_productos tbl_productos = db.tbl_productos.Find(id);
            db.tbl_productos.Remove(tbl_productos);
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
