using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;
using System.Data;
using System.Text;
using System.Data.Odbc;
using System.Diagnostics;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;
using BusinessLayer;
using System.Text.Json;
using System.Web.ModelBinding;
using ModelState = System.Web.Mvc.ModelState;
using ModelError = System.Web.Mvc.ModelError;
using NLog;

namespace University.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentBL _studentBL;
        private static Logger logger = LogManager.GetLogger("myLoggerRule");
        public StudentController(IStudentBL studentBL)
        {
            _studentBL = studentBL;
        }

        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSubjects()
        {
            logger.Info("Got Subjects");
            return Json(_studentBL.GetSubjects());
        }
        [HttpPost]
        public ActionResult GetGrades()
        {
            logger.Info("Got Grades");
            return (Json(_studentBL.GetGrades()));
        }
        [HttpPost]
        public ActionResult Register(Student student)
        {
           
            if (ModelState.IsValid)
            {
                if (Session["currentUserId"] == null)
                {
                    logger.Error("Not Currently Logged in..");
                    return Json(new { error = "Not Currently Logged in.." });
                }
                logger.Info("Register Student");
                student.UserId = (int) Session["CurrentUserId"];
                int rowAffected = _studentBL.Add(student);
                if(rowAffected > 0)
                {
                    logger.Info("redirecting User");
                    return Json(new { url = Url.Action("../User/Login") });
                }
                logger.Info("User no Registered");
                return Json(new { error = "Not Registered" });
                
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        logger.Error(error.ErrorMessage.ToString());
                    }
                }
                return Json(new { error = "Student not registered" });
            }
        }
        public ActionResult Detail()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetCurrentStudent()
        {
            Student student = new Student();
            if (Session["CurrentUserId"] == null)
            {
                logger.Error("Can not get Student because User is not logged in");
                return Json(new { error = "Can not get Student because User is not logged in" }, JsonRequestBehavior.AllowGet);
            }
            student.UserId = (int) Session["CurrentUserId"];
            student = _studentBL.GetStudent(student);
            if(student == null)
            {
                logger.Error("No Student Found");
                return Json(new { error = "No StudentFound" }, JsonRequestBehavior.AllowGet);
            }
            logger.Info("Got Student");
            return Json(student, JsonRequestBehavior.AllowGet);
        } 
    }
}
