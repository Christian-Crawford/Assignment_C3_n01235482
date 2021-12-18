using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment_C1.Models;
using MySql.Data.MySqlClient;


namespace Assignment_C1.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Connecting with the teacher database
        private SchoolDBContext School = new SchoolDBContext();
       
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET/api/TeacherData/ShowTeachers</example> </example>
        /// <returns>List of all Teachers and attached information</returns>
        [HttpGet]

        public IEnumerable<Teacher> ShowTeachers()
        {

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teacher = new List<Teacher> {};

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherEmpNum = ResultSet["employeenumber"].ToString();
                string TeacherHireDate = ResultSet["hiredate"].ToString();
                decimal TeacherSal = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherEmpNum = TeacherEmpNum;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSal = TeacherSal;

                Teacher.Add(NewTeacher);



            }

            Conn.Close();

            return Teacher;


        }
        /// <summary>
        /// Finds a particular Teacher based on Id
        /// </summary>
        /// <param name="id">The id associated with each teacher</param>
        /// <example>GET/api/TeacherData.FindTeacher/3 </example>
        /// <returns>Linda Chan (and rest of file)</returns>
       [HttpGet]
        public Teacher FindTeacher (int id)
        {
           Teacher NewTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select* from teachers where teacherid = " + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();
        
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherEmpNum = ResultSet["employeenumber"].ToString();
                string TeacherHireDate = ResultSet["hiredate"].ToString();
                decimal TeacherSal = (decimal)ResultSet["salary"];


                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherEmpNum = TeacherEmpNum;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSal = TeacherSal;
            }

            return NewTeacher;

        }
        /// <summary>
        /// Add a New Teacher into the list
        /// </summary>
        /// <param name="NewTeacher">Teacher Object</param>
        /// 
        [HttpPost]
        
        public void AddTeacher(Teacher NewTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            string query= "insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (@teacherid, @teacherfname, @teacherlname, @teacherempnum, @hiredate, @salary)";
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@teacherid", NewTeacher.TeacherId);
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@teacherempnum", NewTeacher.TeacherEmpNum);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.TeacherHireDate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.TeacherSal);
            

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        /// <summary>
        /// Updates Teacher given infomrmation
        /// </summary>
        /// <param name="SelectedTeacher">Selected Teacher including all informaiton</param>
        public void UpdateTeacher(Teacher SelectedTeacher)

        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            string query = "update teachers set teacherid=@teacherid, teacherfname=@teacherfname, teacherlname=@teacherlname, employeenumber=@teacherempnum, hiredate=@hiredate, salary=@salary where teacherid=@teacherid";
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@teacherid", SelectedTeacher.TeacherId);
            cmd.Parameters.AddWithValue("@teacherfname", SelectedTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", SelectedTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@teacherempnum", SelectedTeacher.TeacherEmpNum);
            cmd.Parameters.AddWithValue("@hiredate", SelectedTeacher.TeacherHireDate);
            cmd.Parameters.AddWithValue("@salary", SelectedTeacher.TeacherSal);


            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        /// <summary>
        /// Deletes a teacher from the database based on ID
        /// </summary>
        /// <param name="id"> The ID of the Teacher</param>
        [HttpPost]
        public void DeleteTeacher (int id)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Delete from teachers where teacherid =@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
