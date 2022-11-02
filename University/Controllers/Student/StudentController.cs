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

namespace University.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentBL _studentBL;
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
            return Json(_studentBL.GetSubjects());
        }
        [HttpPost]
        public ActionResult GetGrades()
        {
            return (Json(_studentBL.GetGrades()));
        }
        [HttpPost]
        public ActionResult Register(Student student)
        {
            if (ModelState.IsValid)
            {
                student.UserId = (int) Session["CurrentUserId"];
                _studentBL.Add(student);
                return Json(new { url = Url.Action("../User/Welcome") });
            }else
            {
                return Json(new { error = "Student not registered" });
            }
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}
