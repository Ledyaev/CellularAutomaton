using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CellularAutomaton.Web.Controllers
{
    public class AutomatonController : Controller
    {
        [HttpPost]
        public ActionResult Save(string area, string rules, string description)
        {
            return View("ShowAutomatons");
        }

        public ActionResult ShowAutomatons()
        {
            return View();
        }
    }
}