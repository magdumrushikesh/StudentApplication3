using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using StudentApplication3.BAL;
using StudentApplication3.Models;

namespace StudentApplication3.Controllers
{
    public class StudentController : Controller
    {
        private StudentBAL studentBAL = new StudentBAL();
        private readonly string errorLogFilePath = @"D:\Project_Images\ErrorLog.txt"; // Define the error log file path
        private readonly string validationLogFilePath = @"D:\Project_Images\ValidationLog.txt"; // Validation log file path

        // Log Error Method
        private void LogError(string errorMessage, Exception ex = null)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(errorLogFilePath, true)) // Append to the file
                {
                    writer.WriteLine($"{DateTime.Now}: {errorMessage}");
                    if (ex != null)
                    {
                        writer.WriteLine($"Exception: {ex.Message}");
                        writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    }
                    writer.WriteLine("---------------------------------------------------");
                }
            }
            catch (Exception logEx)
            {
                // If logging fails, output to debug console.
                Debug.WriteLine($"Error logging failed: {logEx.Message}");
            }
        }

        // Log Validation Errors
        private void LogValidationErrors(ModelStateDictionary modelState)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(validationLogFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: Validation Errors:");
                    foreach (var key in modelState.Keys)
                    {
                        var errors = modelState[key].Errors;
                        foreach (var error in errors)
                        {
                            writer.WriteLine($"Field: {key} - Error: {error.ErrorMessage}");
                        }
                    }
                    writer.WriteLine("---------------------------------------------------");
                }
            }
            catch (Exception logEx)
            {
                Debug.WriteLine($"Validation logging failed: {logEx.Message}");
            }
        }

        // Index Action (GET)
        public ActionResult Index()
        {
            try
            {
                var students = studentBAL.GetAllStudents();
                return View(students);
            }
            catch (Exception ex)
            {
                LogError("Error in Index action.", ex);
                return RedirectToAction("ErrorPage");
            }
        }

        // Create Action (GET)
        public ActionResult Create()
        {
            try
            {
                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View();
            }
            catch (Exception ex)
            {
                LogError("Error in Create (GET) action.", ex);
                return RedirectToAction("ErrorPage");
            }
        }

        // Create Action (POST)
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (studentBAL.EmailExists(student.Email))
                    {
                        ModelState.AddModelError("Email", "Email address already exists.");
                    }
                    else
                    {
                        studentBAL.AddStudent(student);
                        return RedirectToAction("Index");
                    }
                }

                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View(student);
            }
            catch (Exception ex)
            {
                LogError("Error in Create (POST) action.", ex);
                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View(student);
            }
        }

        // Edit Action (GET)
        public ActionResult Edit(int id)
        {
            try
            {
                var student = studentBAL.GetStudentById(id);
                if (student == null) return HttpNotFound();

                if (student.DOB != null)
                {
                    ViewBag.FormattedDOB = student.DOB?.ToString("yyyy-MM-dd");
                }
                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View(student);
            }
            catch (Exception ex)
            {
                LogError("Error in Edit (GET) action.", ex);
                return RedirectToAction("ErrorPage");
            }
        }

        // Edit Action (POST)
        [HttpPost]
        public ActionResult Edit(Student model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentBAL.UpdateStudent(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    LogValidationErrors(ModelState);
                }

                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View(model);
            }
            catch (Exception ex)
            {
                LogError("Error in Edit (POST) action.", ex);
                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View(model);
            }
        }

        // Delete Action (GET)
        public ActionResult Delete(int id)
        {
            try
            {
                var student = studentBAL.GetStudentById(id);
                if (student == null) return HttpNotFound();
                return View(student);
            }
            catch (Exception ex)
            {
                LogError("Error in Delete (GET) action.", ex);
                return RedirectToAction("ErrorPage");
            }
        }

        // Delete Action (POST)
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
                LogError("Error in DeleteConfirmed (POST) action.", ex);
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // Get Students Action for Pagination
        [HttpGet]
        public JsonResult GetStudents(int page = 1, int rows = 10)
        {
            try
            {
                var students = studentBAL.GetAllStudents()
                    .Select(s => new StudentDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Age = s.Age,
                        Email = s.Email,
                        DOB = s.DOB, // Format DOB as "dd/MM/yyyy"
                        Gender = s.Gender,
                        Hobbies = s.Hobbies,
                        CDAC_Center = s.CDAC_Center
                    }).ToList();


                int totalRecords = students.Count;
                int totalPages = (int)Math.Ceiling((double)totalRecords / rows);

                var paginatedStudents = students.Skip((page - 1) * rows).Take(rows);

                return Json(new
                {
                    data = paginatedStudents,
                    currentPage = page,
                    totalPages,
                    totalRecords
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError("Error in GetStudents action.", ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Error Page Action
        public ActionResult ErrorPage()
        {
            return View();
        }
    }

    // DTO to avoid circular references
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? Age { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }

        public string Gender { get; set; }

        public string Hobbies { get; set; }
        public string CDAC_Center { get; set; }
    }
}
