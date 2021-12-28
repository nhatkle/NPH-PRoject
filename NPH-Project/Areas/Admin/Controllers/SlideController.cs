using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using System.Data;
using System.IO;
using Models.DAO;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
       
        // GET: Admin/Slide
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var dao = new SlideDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SeachString = searchString;
            return View(model);
        }

        // GET: Admin/Slide/Details/5
        public ActionResult Details(int id)
        {
            var slide = new SlideDao().ViewDetail(id);
            return View(slide);
        }

        // GET: Admin/Slide/Create
        public ActionResult Create()
        {
            Slide slide = new Slide();
            return View(slide);
        }

        // POST: Admin/Slide/Create
        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                if (slide.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(slide.ImageUpload.FileName);
                    string extension = Path.GetExtension(slide.ImageUpload.FileName);
                    filename += extension;
                    slide.Image = "/Assets/Admin/img/slides/" + filename;
                    slide.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Assets/Admin/img/slides/"), filename));
                    var dao = new SlideDao();
                    long id = dao.Insert(slide);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Uable to upload");
                    }
                }
                else
                {
                    var dao = new SlideDao();
                    long id = dao.Insert(slide);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Uable to upload");
                    }
                }
            }
            return View();
        }

        // GET: Admin/Slide/Edit/5
        public ActionResult Edit(int id)
        {
            var slide = new SlideDao().ViewDetail(id);
            return View(slide);
        }

        // POST: Admin/Slide/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideDao();
                if (slide.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(slide.ImageUpload.FileName);
                    string extension = Path.GetExtension(slide.ImageUpload.FileName);
                    filename += extension;
                    slide.Image = "/Assets/Admin/img/slides/" + filename;
                    slide.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Assets/Admin/img/slides/"), filename));

                    var resultimage = dao.Updateimage(slide);
                    if (resultimage)
                    {
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Them slide khong thanh cong");
                    }
                }
                else
                {

                    var result = dao.Update(slide);
                    if (result)
                    {
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Them slide khong thanh cong");
                    }
                }


            }
            return View("Index");
        }



        // POST: Admin/Slide/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {

            // TODO: Add delete logic here
            new SlideDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}

