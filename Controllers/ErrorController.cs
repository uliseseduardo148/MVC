using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "ERROR 505";
                    ViewBag.Description = "Ocurrio un error inesperado, esperemos que no vuelva a pasar ...";
                    break;

                case 404:
                    ViewBag.Title = "ERROR 404";
                    ViewBag.Description = "Página no encontrada, la URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal :( ...";
                    break;
            }

            return View("~/Views/Shared/Error.cshtml");
        }
    }
}