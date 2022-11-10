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
using NLog;

namespace University.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBL _userBL;
        private static Logger logger = LogManager.GetLogger("myLoggerRule");
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
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            logger.Info("Enter Login");
            User authenticatedUser = _userBL.AuthenticateUser(user);
            if (authenticatedUser == null)
            {
                logger.Error("Authenticated User is null");
                return Json(new { error = "User not Found" });    
            }
            Session["CurrentUser"] = authenticatedUser.Email;
            Session["CurrentUserId"] = authenticatedUser.ID;
            Session["CurrentUserRole"] = authenticatedUser.RoleId;
            logger.Info("Authenticated User");
            return Json(new { url = Url.Action(_userBL.RedirectUser(authenticatedUser)) });
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public JsonResult GetCurrentUser()
        {
            logger.Info("Got Users");
            return Json(new {currentUser = Session["CurrentUser"], currentUserId = Session["CurrentUserId"], currentUserRole = Session["CurrentUserRole"] }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            if (ModelState.IsValid)
            {
                logger.Info("Model Is Valid");
                int result = _userBL.Add(user);
                if (result == UniversityConstants.rowAffectedZero)
                {
                    logger.Error("User not Registered");
                    return Json(new { error = "User not Registered" });
                }
                User registeredUser = _userBL.GetUsers().Where(z => z.Email == user.Email).FirstOrDefault();
                Session["CurrentUser"] = registeredUser.Email;
                Session["CurrentUserId"] = registeredUser.ID;
                Session["CurrentUserRole"] = registeredUser.RoleId;
                logger.Info("Registered User");
                return Json(new { url = _userBL.RedirectUser(registeredUser) });
            }
            logger.Error("User not Registered because Model is invalid");
            return Json(new { error = "User not Registered" });
        }
        public ActionResult Admin()
        {
            return View();
        }
        public JsonResult GetStudents()
        {
            logger.Info("Got Students");
            return Json(_userBL.GetStudents(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ApproveStudents(ApprovedStudents student)
        {
            if (Session["CurrentUser"] == null)
            {
                logger.Error("Not Logged in");
                return Json(new { error = "Not Logged in" });
            }
            if ((int)Session["CurrentUserRole"] != (int) Roles.Admin) {

                logger.Error("Not currently signed in as Admin");
                return Json(new { error = "Not currently signed in as Admin" });
            }
            if(_userBL.ApproveStudents(student.Students) == UniversityConstants.rowAffectedZero)
            {

                logger.Error("Zero Student Approved");
                return Json(new { error = "An Error Occured" });
            }
            logger.Info("Approved Student");
            return Json(new { success = "Approved Student" });
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return Json(new { url = Url.Action("") }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TestDojo()
        {
            return View();
        }
    }
}