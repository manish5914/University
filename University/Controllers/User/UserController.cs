using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLayer;
using System.Security.Permissions;
using System.Web.Helpers;

namespace University.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBL _userBL;
        public UserController(IUserBL userBL)
        {
                _userBL = userBL;
        }
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
            User authenticatedUser = _userBL.AuthenticateUser(user);
            if (authenticatedUser == null)
            {
                return Json(new { error = "User not Found" });    
            }
            Session["CurrentUser"] = authenticatedUser.Email;
            Session["CurrentUserId"] = authenticatedUser.ID;
            Session["CurrentUserRole"] = authenticatedUser.RoleId;
            return Json(new { url = Url.Action(_userBL.RedirectUser(authenticatedUser)) });
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public JsonResult GetCurrentUser()
        {
            return Json(new {currentUser = Session["CurrentUser"], currentUserId = Session["CurrentUserId"], currentUserRole = Session["CurrentUserRole"] }, JsonRequestBehavior.AllowGet);
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
            int result = _userBL.Add(user);
            if (result != 1)
            {
                return Json(new { error = "User not Registered" });
            }
            User registeredUser = _userBL.GetUsers().Where(z => z.Email == user.Email).FirstOrDefault();
            Session["CurrentUser"] = registeredUser.Email;
            Session["CurrentUserId"] = registeredUser.ID;
            Session["CurrentUserRole"] = registeredUser.RoleId;
            return Json(new { url = Url.Action("Welcome") });
        }
        public ActionResult Admin()
        {
            return View();
        }
        public JsonResult GetStudents()
        {
            return Json(_userBL.GetStudents(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ApproveStudents(string jsonResult)
        {
            return Json(new { success = "Approved Student" });
        }
    }
}