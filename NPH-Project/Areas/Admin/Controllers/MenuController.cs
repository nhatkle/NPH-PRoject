using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new MenuDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SeachString = searchString;
            return View(model);
        }
        public void SetViewBag(long? selectId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectId);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();
                long id = dao.Insert(menu);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Them menu khong thanh cong");
                }
            }
            return View("Index");
        }
        public ActionResult Details(int id)
        {
            var menu = new MenuDao().ViewDetail(id);
            return View(menu);
        }
        public ActionResult Edit(int id)
        {
            var menu = new MenuDao().ViewDetail(id);
            return View(menu);
        }
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();
                var result = dao.Update(menu);
                if (result)
                {
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat menu khong thanh cong");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new MenuDao().Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new MenuDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }

}
