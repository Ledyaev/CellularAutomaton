using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using CellularAutomaton.Web.Filters;

namespace CellularAutomaton.Web.Controllers
{

    [Theme]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangeCulture(string theme)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> themes = new List<string>() { "dark", "light" };
            if (!themes.Contains(theme))
            {
                theme = "dark";
            }
            // Сохраняем выбранную культуру в куки
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
            return Redirect(returnUrl);
        }
    }
}