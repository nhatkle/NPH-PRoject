using Models.DAO;
using Models.EF;
using NPH_Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            return View(model);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var ecryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = ecryptedMd5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Create Successfully", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Account is sucessfully created");
                }
            }
            return View("Index");
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var ecryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = ecryptedMd5Pas;
                }

                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Edit Successfully", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Account is Update sucessfully ");
                }
            }
            return View("Index");
        }
        // POST: Admin/User/Delete/5
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
