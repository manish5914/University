using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using DAL.DataAccessLayer;
using System.Data;
using System.Text;
using System.Data.Odbc;
using System.Diagnostics;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;
using BL;
using System.Text.Json;

namespace University.Controllers
{
    public class StudentController : Controller
    {  
        StudentBL studentBL = new StudentBL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSubjects()
        {
            Debug.WriteLine("In Subject");
            return Json(studentBL.GetSubjects());
        }
        [HttpPost]
        public ActionResult GetGrades()
        {
            return (Json(studentBL.GetGrades()));
        }
        [HttpPost]
        public ActionResult Register(Student student)
        {
            Debug.WriteLine("in post register");
            if (ModelState.IsValid)
            {
                studentBL.Add(student);
                return Json(new { url = Url.Action("../User/Welcome") });
            }else
            {
                return Json(new { error = "Student not registered" });
            }
        }
    }
}
