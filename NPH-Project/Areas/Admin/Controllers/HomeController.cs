using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        NPHDbContext db = new NPHDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {

            ViewBag.PageView = HttpContext.Application["PageView"].ToString();
            ViewBag.Products = db.Products.Count();
            ViewBag.Order = db.Orders.Count();
            ViewBag.Blog = db.Blogs.Count();

            return View();
        }
    }
}