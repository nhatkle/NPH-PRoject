using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductCategoryDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SeachString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                long id = dao.Insert(productcategory);
                if (id > 0)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to Add");
                }
            }
            return View("Index");
        }
        public ActionResult Details(int id)
        {
            var productcategory = new ProductCategoryDao().ViewDetail(id);
            return View(productcategory);
        }
        public ActionResult Edit(int id)
        {
            var productcategory = new ProductCategoryDao().ViewDetail(id);
            return View(productcategory);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var result = dao.Update(productcategory);
                if (result)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to Edit");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductCategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
