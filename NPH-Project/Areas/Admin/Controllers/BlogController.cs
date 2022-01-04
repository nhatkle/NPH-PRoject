using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        // GET: Admin/Blog
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new BlogDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SeachString = searchString;
            return View(model);
        }

        // GET: Admin/Blog/Details/5
        [ValidateInput(false)]
        public ActionResult Details(long id)
        {
            var blog = new BlogDao().ViewDetail(id);
            return View(blog);
        }

        // GET: Admin/Blog/Create
        public ActionResult Create()
        {
            Blog blog = new Blog();
            return View(blog);
        }

        // POST: Admin/Blog/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                if (blog.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(blog.ImageUpload.FileName);
                    string extension = Path.GetExtension(blog.ImageUpload.FileName);
                    filename += extension;
                    blog.Image = "/Assets/Admin/img/blogs/" + filename;
                    blog.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Assets/Admin/img/blogs/"), filename));
                    var dao = new BlogDao();
                    long id = dao.Insert(blog);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Blog");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to upload");
                    }
                }
                else
                {
                    var dao = new BlogDao();
                    long id = dao.Insert(blog);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Blog");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to upload");
                    }
                }
            }
            return View();
        }

        // GET: Admin/Blog/Edit/5
        public ActionResult Edit(int id)
        {
            var blog = new BlogDao().ViewDetail(id);
            return View(blog);
        }

        // POST: Admin/Blog/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, Blog blog)
        {
            if (ModelState.IsValid)
            {
                var dao = new BlogDao();
                if (blog.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(blog.ImageUpload.FileName);
                    string extension = Path.GetExtension(blog.ImageUpload.FileName);
                    filename += extension;
                    blog.Image = "/Assets/Admin/img/blogs/" + filename;
                    blog.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Assets/Admin/img/blogs/"), filename));

                    var resultimage = dao.Updateimage(blog);
                    if (resultimage)
                    {
                        return RedirectToAction("Index", "Blog");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to upload");
                    }
                }
                else
                {

                    var result = dao.Update(blog);
                    if (result)
                    {
                        return RedirectToAction("Index", "Blog");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to upload");
                    }
                }


            }
            return View("Index");
        }
        // GET: Admin/Blog/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {

            // TODO: Add delete logic here
            new BlogDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
   

}