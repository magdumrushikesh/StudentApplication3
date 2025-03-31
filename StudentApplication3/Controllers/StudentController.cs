using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using StudentApplication3.BAL;
using StudentApplication3.Models;

namespace StudentApplication3.Controllers
{
    public class StudentController : Controller
    {
        private StudentBAL studentBAL = new StudentBAL();
        private readonly string errorLogFilePath = @"D:\Project_Images\ErrorLog.txt"; // Define the error log file path
        private readonly string validationLogFilePath = @"D:\Project_Images\ValidationLog.txt"; //validation log file path

        private void LogError(string errorMessage)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(errorLogFilePath, true)) // Append to the file
                {
                    writer.WriteLine($"{DateTime.Now}: {errorMessage}");
                }
            }
            catch (Exception logEx)
            {
                // If logging fails, output to debug console.
                Debug.WriteLine($"Error logging failed: {logEx.Message}");
                Debug.WriteLine($"Original error: {errorMessage}");
            }
        }

        private void LogValidationErrors(ModelStateDictionary modelState)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(validationLogFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: Validation Errors:");
                    foreach (var modelError in modelState.Values)
                    {
                        foreach (var error in modelError.Errors)
                        {
                            writer.WriteLine($"- {error.ErrorMessage}"); // Removed extra space
                        }
                    }
                }
            }
            catch (Exception logEx)
            {
                Debug.WriteLine($"Validation logging failed: {logEx.Message}");
                Debug.WriteLine($"Original validation errors.");
            }
        }

        public ActionResult Index()
        {
            try
            {
                var students = studentBAL.GetAllStudents();
                return View(students);
            }
            catch (Exception ex)
            {
                LogError($"Error in Index action: {ex.Message}");
                return View("Error"); // Or redirect to an error page
            }
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View();
            }
            catch (Exception ex)
            {
                LogError($"Error in Create (GET) action: {ex.Message}");
                return View("Error");
            }
        }
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
                LogError($"Error in Create (POST) action: {ex.Message}");
                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View(student);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var student = studentBAL.GetStudentById(id);
                if (student == null) return HttpNotFound();

                if (student.DOB != null)
                {
                    ViewBag.FormattedDOB = student.DOB.Value.ToString("yyyy-MM-dd");
                }
                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View(student);
            }
            catch (Exception ex)
            {
                LogError($"Error in Edit (GET) action: {ex.Message}");
                return View("Error");
            }
        }

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
                LogError($"Error in Edit (POST) action: {ex.Message}");
                ViewBag.list = new SelectList(studentBAL.GetAllCenters(), "Name", "Name");
                return View(model);
            }
        }

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
                LogError($"Error in Delete (GET) action: {ex.Message}");
                return View("Error");
            }
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
                LogError($"Error in DeleteConfirmed (POST) action: {ex.Message}");
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


    }
}