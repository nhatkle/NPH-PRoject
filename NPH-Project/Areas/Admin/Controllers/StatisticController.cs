using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Admin/Statistic
        public ActionResult Index()
        {
            ViewBag.PageView = HttpContext.Application["PageView"].ToString(); // View on website
            return View();
        }
    }
}