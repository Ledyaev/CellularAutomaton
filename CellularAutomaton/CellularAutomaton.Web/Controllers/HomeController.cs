using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using CellularAutomaton.Web.Filters;

namespace CellularAutomaton.Web.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(User);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CurrentTheme()
        {
            HttpCookie cookie = Request.Cookies["theme"];
            if (cookie == null)
            {
                cookie = new HttpCookie("theme");
                cookie.HttpOnly = false;
                cookie.Value = "light.css";
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Content(cookie.Value);
        }


        public ActionResult ChangeTheme(string theme)
        {
            HttpCookie cookie = Request.Cookies["theme"];
            if (cookie != null)
                cookie.Value = theme;   // если куки уже установлено, то обновляем значение
            else
            {
                cookie = new HttpCookie("theme");
                cookie.HttpOnly = false;
                cookie.Value = theme;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}