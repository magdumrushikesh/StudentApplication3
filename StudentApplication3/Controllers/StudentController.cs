using System;
using System.Linq;
using System.Web.Mvc;
using StudentApplication3.Models;

namespace StudentApplication3.Controllers
{
    public class StudentController : Controller
    {
        private StudentDBEntities db = new StudentDBEntities();

        // GET: Student/Create
        public ActionResult Create()
        {
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
    }
}
