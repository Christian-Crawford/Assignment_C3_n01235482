using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Assignment_C1.Models
{
    public class Teacher
    {
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string TeacherEmpNum;
        public string TeacherHireDate;
        public decimal TeacherSal;
    }
}