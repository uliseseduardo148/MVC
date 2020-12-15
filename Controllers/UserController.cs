using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC.Classes;
using MVC.Models;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private ProductosEntities db = new ProductosEntities();

        // GET: User
        public ActionResult Index()
        {
            if (Session["username"] == null || Session["idTypeUser"].Equals(2))
                return Redirect("~/Error/Index");

            var tbl_user = db.tbl_user.Include(t => t.tbl_typeUser);
            return View(tbl_user.ToList());
        }

        // GET: tbl_user/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fld_idUser,fld_username,fld_encryptedPassword,fld_password,fk_idTipo")] tbl_user tbl_user)
        {
            if (ModelState.IsValid && tbl_user.fld_encryptedPassword.Equals(tbl_user.fld_password))
            {
                try
                {
                    tbl_user.fk_idTipo = 2;
                    Helper.ValidatePassword(tbl_user.fld_password);
                    Helper.ValidatePassword(tbl_user.fld_encryptedPassword);
                    tbl_user.fld_encryptedPassword = Helper.Encrypt(tbl_user.fld_password);
                    db.tbl_user.Add(tbl_user);
                    db.SaveChanges();
                    TempData["mensaje"] = "Se ha registrado de forma exitosa. Ahora puede iniciar sesión";
                    TempData["estado"] = "ok";
                }
                catch (ValidatorException ex) {
                    TempData["mensaje"] = ex.Message;
                    TempData["estado"] = "fail";
                }
            }
            else {
                TempData["mensaje"] = "Las contraseñas no coinciden";
                TempData["estado"] = "fail";
            }
            return RedirectToAction("Create", "User");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null || Session["idTypeUser"].Equals(2))
                return Redirect("~/Error/Index");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_user tbl_user = db.tbl_user.Find(id);
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            return View(tbl_user);
        }

        // POST: User/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fld_idUser,fld_username,fld_encryptedPassword,fld_password,fk_idTipo")] tbl_user tbl_user)
        {
            if (Session["username"] == null || Session["idTypeUser"].Equals(2))
                return Redirect("~/Error/Index");

            if (ModelState.IsValid && tbl_user.fld_encryptedPassword.Equals(tbl_user.fld_password))
            {
                tbl_user.fk_idTipo = 2;
                Helper.ValidatePassword(tbl_user.fld_password);
                Helper.ValidatePassword(tbl_user.fld_encryptedPassword);
                tbl_user.fld_encryptedPassword = Helper.Encrypt(tbl_user.fld_password);
                db.Entry(tbl_user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["mensaje"] = "Se han actualizado los datos";
                TempData["estado"] = "ok";
                return RedirectToAction("Index");
            }
            else {
                TempData["mensaje"] = "Las contraseñas no coinciden";
                TempData["estado"] = "fail";
            }
            return View(tbl_user);
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
