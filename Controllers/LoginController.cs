﻿using MVC.Classes;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private ProductosEntities db = new ProductosEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /*Este método verifica la existencia del usuario en la base de datos.
         * Ya que se usó SHA256 para encriptar el password no es posible desencriptarlo
         * por lo que se retoma lo que introduce el usuario y se compara con lo que reside en la base de datos
         */
        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(tbl_user usuario, string returnUrl)
        {
            string username = usuario.fld_username;
            string password = Helper.Encrypt(usuario.fld_password);
            string cookiesession = string.Empty;
            List<tbl_user> list = (from p in db.tbl_user where p.fld_username == username && p.fld_encryptedPassword == password select p).ToList();

            if (list.Count() > 0)
            {
                cookiesession = username + "|" + password;
                FormsAuthentication.SetAuthCookie(cookiesession, false);
                Session["username"] = list.First().fld_username;
                Session["idTypeUser"] = list.FirstOrDefault().fk_idTipo;

                return Redirect("Productos/Index");
            }
            else
            {
                TempData["mensaje"] = "El nombre de usuario o contraseña es incorrecto";
            }
            return View(usuario);
        }

        //Este método destruye la sesión
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}