using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCodeSolution.HTML5.Controllers
{
    public class DistrictController : Controller
    {
        // GET: District

        /// <summary>
        /// 中国省市区地址三级联动插件
        /// </summary>
        /// <returns></returns>
        public ActionResult Linkage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetChineseDistricts()
        {
            JsonResult result = new JsonResult();


            Dictionary<int, Dictionary<int, string>> allDis = new Dictionary<int, Dictionary<int, string>>();

            Dictionary<int, string> dis1 = new Dictionary<int, string>();
            dis1.Add(1, "北京");
            dis1.Add(2, "天津");
            dis1.Add(3, "上海");
            dis1.Add(4, "广州");
            dis1.Add(5, "深圳");
            allDis.Add(0,dis1);

            Dictionary<int, string> dis2 = new Dictionary<int, string>();
            dis2.Add(101, "朝阳");
            dis2.Add(102, "通州");
            dis2.Add(103, "海淀");
            dis2.Add(104, "丰台");
            dis2.Add(105, "昌平");
            allDis.Add(1, dis2);

            result.Data = new { statusCode = 200, message = "成功！", AllDis= allDis };

            return result;
        }
    }
}