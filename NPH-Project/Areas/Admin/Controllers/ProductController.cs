using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using Models.EF;
using Models.DAO;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SeachString = searchString;
            return View(model);
        }

        // GET: Admin/Product/Details/5
        [ValidateInput(false)]
        public ActionResult Details(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }
        
        public void SetViewBag(long? selectId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectId);
        }
        // GET: Admin/Product/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            SetViewBag();
            Product product = new Product();
            return View(product);
        }
      
        // POST: Admin/Product/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                if (product.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extension = Path.GetExtension(product.ImageUpload.FileName);
                    filename += extension;
                    product.Image = "/Assets/Admin/img/products/" + filename;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Assets/Admin/img/products/"), filename));
                    var dao = new ProductDao();
                    long id = dao.Insert(product);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to upload");
                    }
                }
                else
                {
                    var dao = new ProductDao();
                    long id = dao.Insert(product);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to upload");
                    }
                }
                SetViewBag();
            }
            return View();
        }

        // GET: Admin/Product/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            SetViewBag(product.CategoryID);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(long id, Product product)
        {

            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                if (product.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extension = Path.GetExtension(product.ImageUpload.FileName);
                    filename += extension;
                    product.Image = "/Assets/Admin/img/products/" + filename;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Assets/Admin/img/products/"), filename));

                    var resultimage = dao.Updateimage(product);
                    if (resultimage)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to upload");
                    }
                }
                else
                {

                    var result = dao.Update(product);
                    if (result)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to upload");
                    }
                }


            }
            SetViewBag(product.CategoryID);
            return View("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        // GET: Admin/Product/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }


  
        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
    }
}

