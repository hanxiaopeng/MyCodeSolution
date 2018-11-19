using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCodeSolution.HTML5.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Menu1(string text = "")
        {
            ViewBag.SelectMenu = text;
            return View();
        }
    }
}