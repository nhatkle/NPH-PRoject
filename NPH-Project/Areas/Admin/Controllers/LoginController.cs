using Models.DAO;
using NPH_Project.Areas.Admin.Models;
using NPH_Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Username,Encryptor.MD5Hash(model.Password));
                if (result ==1)
                {
                    var user = dao.GetByID(model.Username);
                    var userSession = new UserLogin();
                    userSession.Username = user.Username;
                    userSession.UserID = user.ID; 
                    Session.Add(CommonConstants.USER_SESSION,userSession);
                    return RedirectToAction("Index", "Home");
                } else if (result == 0)
                {
                    ModelState.AddModelError("", "This Account does not exist");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "This Account is blocked");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Wrong Password");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to login");
                }
            }
            return View("Index");
        }

    }
}