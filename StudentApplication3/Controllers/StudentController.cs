using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StudentApplication3.Models;

namespace StudentApplication3.Controllers
{
    public class StudentController : Controller
    {
        private StudentDBEntities3 db = new StudentDBEntities3();


        // GET: Student/Create
        public ActionResult Create()
        {
            var list = db.centers.Select(c => new { c.Id, c.Name }).ToList(); // Use Name for both value and display
            ViewBag.list = new SelectList(list, "Name", "Name");
            return View();
        }



        // POST: Student/Create using AJAX
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (student == null)
            {
                return Json(new { success = false, message = "Invalid data." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return Json(new { success = false, message = string.Join(" ", errors) });
            }

            try
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        // GET: Student/Index
        public ActionResult Index()
        {
            var details = db.Students.ToList();
           
            return View(details);
        }

        //show details of one Student
        public ActionResult Details(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var list = db.centers.Select(c => new { c.Id, c.Name }).ToList(); // Use Name for both value and display
            ViewBag.list = new SelectList(list, "Name", "Name");

            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (student == null)
            {
                return Json(new { success = false, message = "Invalid student data." });
            }

            var existingStudent = db.Students.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent == null)
            {
                return Json(new { success = false, message = "Student not found." });
            }

            if (ModelState.IsValid)
            {
                // Update student details
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.DOB = student.DOB;
                existingStudent.Age = student.Age;
                existingStudent.Gender = student.Gender;
                existingStudent.Hobbies = student.Hobbies;
                existingStudent.CDAC_Center = student.CDAC_Center;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Json(new { success = false, message = "Validation failed. Please check your input." });
            }
        }

        public ActionResult Delete(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ActionName("DeleteConfirmed")]
        public JsonResult DeleteConfirmed(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return Json(new { success = false, message = "Student not found." });
            }

            try
            {
                db.Students.Remove(student);
                db.SaveChanges();
                return Json(new { success = true, message = "Student deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }




        private ActionResult NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
