using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class StatisticController : BaseController
    {
        NPHDbContext db = new NPHDbContext();
        // GET: Admin/Statistical
        public ActionResult Index(string fromDate, string toDate)
        {
            var dao = new StatisticDao();
            ViewBag.ListDateStatistic = dao.GetRevenueStatistic(fromDate, toDate);
            ViewBag.PageView = HttpContext.Application["PageView"].ToString();
            ViewBag.Products = db.Products.Count();
            ViewBag.Order = db.Orders.Count();
            ViewBag.Blog = db.Blogs.Count();
            return View();
        }
        public ActionResult GetRevenueStatistics(string fromDate,string toDate)
        {
            var statistic = new StatisticDao().GetRevenueStatistic(fromDate, toDate);
            return Json(statistic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMonthRevenueStatistic(string fromDate, string toDate)
        {
            var mstatistic = new StatisticDao().GetMonthRevenueStatistic(fromDate, toDate);
            return Json(mstatistic, JsonRequestBehavior.AllowGet);
        }
    }
}