﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLayer;

namespace University.Controllers
{
    public class UserController : Controller
    {
        UserBL userBL = new UserBL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            Debug.WriteLine("UserLogin");
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            Debug.WriteLine("Login");
            User authenticatedUser = userBL.AuthenticateUser(user);
            if (authenticatedUser != null)
            {
                Session["CurrentUser"] = authenticatedUser.Email;
                Session["CurrentUserId"] = authenticatedUser.ID;
                return Json(new { url = Url.Action("Welcome") });
            }
            else
            {
                return Json(new { error = "User not Found" });
            }
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public JsonResult GetCurrentUser()
        {
            return Json(new {currentUser = Session["CurrentUser"] }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Register()
        {
            Debug.WriteLine("Register Page");
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            Debug.WriteLine("Entered Register User");

            int result = userBL.Add(user);
           
            if (result == 1)
            {
                Session["CurrentUser"] = user.Email;
                Session["CurrentUserId"] = userBL.GetUserIDByEmail(user);
                return Json(new { url = Url.Action("Welcome") });
            }
            else
            {
                return Json(new { error = "User not Registered" });
            }

        }
    }
}