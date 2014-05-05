using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CellularAutomaton.Web.Filters
{
    public class ThemeAttribute: FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string theme = null;
            // Получаем куки из контекста, которые могут содержать установленную культуру
            HttpCookie themeCookie = filterContext.HttpContext.Request.Cookies["theme"];
            if (themeCookie != null)
                theme = themeCookie.Value;
            else
                theme = "dark";

            // Список культур
            List<string> cultures = new List<string>() { "dark", "light" };
            if (!cultures.Contains(theme))
            {
                theme = "dark";
            }
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}