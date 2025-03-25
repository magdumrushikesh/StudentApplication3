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
            var list = db.centers.ToList();
            ViewBag.list = new SelectList(list, "Id", "Name"); // Correctly populate the SelectList
            return View();
        }


        // POST: Student/Create using AJAX
        [HttpPost]
        public JsonResult Create(Student student)
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
                return Json(new { success = true, message = "Student created successfully!" });
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

        public ActionResult Edit(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View("Create",student);
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
