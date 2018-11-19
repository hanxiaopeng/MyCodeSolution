using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCodeSolution.PictureProcessing.Controllers
{
    public class ShowController : Controller
    {
        // GET: Show

        /// <summary>
        /// 以图片中心点截取，自适应屏幕
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoZoom()
        {
            return View();
        }
    }
}