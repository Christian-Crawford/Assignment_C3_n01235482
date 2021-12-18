using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_C1.Models;
using System.Diagnostics;

namespace Assignment_C1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        [HttpGet]
        public ActionResult List()
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teacher = Controller.ShowTeachers();
            return View(Teacher);
        }


        [HttpGet]
        // GET: Teacher/Show/{id}
        [Route("Teacher/Show/{id}")]
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        [HttpGet]
        [Route("Teacher/Update/{id}")]
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);

        }

        [HttpPost]
        [Route("Teacher/Update/{id}")]
        public ActionResult Update(int id, int teacher_id, string f_name, string l_name, string emp_num, string hire_date, decimal salary)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = new Teacher();
            SelectedTeacher.TeacherId = id;
            SelectedTeacher.TeacherFname = f_name;
            SelectedTeacher.TeacherLname = l_name;
            SelectedTeacher.TeacherEmpNum = emp_num;
            SelectedTeacher.TeacherHireDate = hire_date;
            SelectedTeacher.TeacherSal = salary;
            return RedirectToAction("Show/" + id);
        }
        // Get: Teacher/New
        [HttpGet]
        [Route("Teacher/New")]
        public ActionResult New()
        {
            return View();

        }

        //POST : Teacher/Create
        [HttpPost]
        [Route("Teacher/Create")]

        public ActionResult Create(int teacher_id, string f_name, string l_name, string emp_num, string hire_date, decimal salary)
        {
            Debug.WriteLine("You are entering a new Teacher with the Following Info: " + teacher_id + f_name + l_name + emp_num + hire_date + salary);

           
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherId = teacher_id;
            NewTeacher.TeacherFname = f_name;
            NewTeacher.TeacherLname = l_name;
            NewTeacher.TeacherEmpNum = emp_num;
            NewTeacher.TeacherHireDate = hire_date;
            NewTeacher.TeacherSal = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);


               return RedirectToAction("List");
        }

        //Get DeleteConfir,{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("list");
        }
    }
}