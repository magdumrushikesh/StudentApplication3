using System;
using System.Diagnostics;
using System.Web.Mvc;
using StudentApplication3.BAL;
using StudentApplication3.Models;

namespace StudentApplication3.Controllers
{
    public class StudentController : Controller
    {
        private StudentBAL studentBAL = new StudentBAL();

        public ActionResult Index()
        {
            var students = studentBAL.GetAllStudents();
            return View(students);
        }

        public ActionResult Create()
        {
            ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                studentBAL.AddStudent(student);
                return RedirectToAction("Index");
            }
            ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
            return View(student);
        }

        public ActionResult Edit(int id)
        {
            var student = studentBAL.GetStudentById(id);
            if (student == null) return HttpNotFound();

            // Ensure date is in the correct format
            if (student.DOB != null)
            {
                ViewBag.FormattedDOB = student.DOB.Value.ToString("yyyy-MM-dd");
            }
            ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
            return View(student);
        }



        [HttpPost]
        public ActionResult Edit(Student model)
        {
            if (ModelState.IsValid)
            {
                studentBAL.UpdateStudent(model);
                return RedirectToAction("Index");
            }
            ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var student = studentBAL.GetStudentById(id);
            if (student == null) return HttpNotFound();
            return View(student);
        }

        [HttpPost]
        [ActionName("DeleteConfirmed")]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                studentBAL.DeleteStudent(id);
                return Json(new { success = true, message = "Student deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

    }
}
