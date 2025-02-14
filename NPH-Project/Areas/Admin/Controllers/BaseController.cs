﻿using NPH_Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NPH_Project.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                  RouteValueDictionary(new
                  {
                      controller = "Login",
                      action = "Index",
                      Area = "Admin"
                  }));
            }
            base.OnActionExecuting(filterContext);
        }
        // GET: Admin/Base
        protected void SetAlert(string message,string type)
        {
            TempData["AlertMessage"] = message;
            if(type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if(type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "errror")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

    }
}