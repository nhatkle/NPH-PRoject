using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class StatisticController : BaseController
    {
        
        // GET: Admin/Statistical
        public ActionResult Index()
        {
                
          

            ViewBag.PageView = HttpContext.Application["PageView"].ToString();
            ViewBag.Online = HttpContext.Application["Online"].ToString();
            return View();
        }
        public ActionResult GetRevenueStatistics(string fromDate,string toDate)
        {
            var statistic = new StatisticDao().GetRevenueStatistic(fromDate, toDate);
            return Json(statistic, JsonRequestBehavior.AllowGet);
        }
    }
}