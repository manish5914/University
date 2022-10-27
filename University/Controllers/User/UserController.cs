using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DataAccessLayer;
using DAL.Models;
using BL;

namespace University.Controllers
{
    public class UserController : Controller
    {
        UserBL userBL = new UserBL();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            Debug.WriteLine("UserLogin");
            return View();
        }
        [HttpPost]
        public ActionResult CheckUser(User user)
        {
            Debug.WriteLine("Check User");
            var searchUser = userBL.GetUsers().Where(u => user.Email == u.Email && user.Password == u.Password).FirstOrDefault();
            if (searchUser != null)
            {
                Session["CurrentUser"] = searchUser.Email;
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
            //var currentUser = Session["CurrentUser"];
            //if (currentUser != null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return View();
            //}
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
                return Json(new { url = Url.Action("Welcome") });
            }
            else
            {
                return Json(new { error = "User not Registered" });

            }

        }
    }
}