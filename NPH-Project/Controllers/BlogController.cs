using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            var blog = new BlogDao();
            ViewBag.Blogs = blog.ListAll();
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(5);
            return View();
        }
        public ActionResult Detail(long id)
        {
            var blog = new BlogDao();
            ViewBag.Blogs = blog.ListAll();
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(5);
            var blog1 = new BlogDao().ViewDetail(id);
            return View(blog1);
        }
    }
}