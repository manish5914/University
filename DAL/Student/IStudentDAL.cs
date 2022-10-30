﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public interface IStudentDAL
    {
        void Add(Student student);
        void AddResult(Student student);
        string GetStudent(string studentId);
        DataTable GetSubjects();
        DataTable GetGrades();

    }
}