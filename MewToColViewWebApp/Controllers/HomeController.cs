using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MewToColViewWebApp.Models;
using MewToColViewWebApp;

namespace MewToColViewWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<ex1table> table;
            using(var ent = new fp7dataEntities1())
            {
                table = ent.ex1table.ToArray();
            }
            return View(table);
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
    }
}