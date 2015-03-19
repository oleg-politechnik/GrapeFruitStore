using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrapeFruitStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["AtHome"] = "selected";
            return View();
        }

        public ActionResult Error(int? code)
        {
            if (code.HasValue && (code.Value == 404))
            {
                TempData["ErrorMessage"] = "404 - Страница не найдена.";
            }
            else
            {
                TempData["ErrorMessage"] = "Неизвестная ошибка.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewData["AtAbout"] = "selected";
            return View();
        }
    }
}
