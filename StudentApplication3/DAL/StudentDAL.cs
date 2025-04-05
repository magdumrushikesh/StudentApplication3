using System;
using System.Collections.Generic;
using System.Linq;
using StudentApplication3.Models;

namespace StudentApplication3.DAL
{
    public class StudentDAL
    {
        private StudentDBEntities7 db = new StudentDBEntities7();

        public List<Student> GetAllStudents() => db.Students.ToList();
        public Student GetStudentById(int id) => db.Students.FirstOrDefault(s => s.Id == id);
        public List<center> GetAllCenters() => db.centers.ToList();

       

        public void AddStudent(Student student)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 
                    throw new Exception("Error while adding student: " + ex.Message);
                }
            }
        }


        public void UpdateStudent(Student student)
        {
            var existingStudent = db.Students.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.DOB = student.DOB;
                existingStudent.Age = student.Age;
                existingStudent.Gender = student.Gender;
                existingStudent.Hobbies = student.Hobbies;
                existingStudent.CDAC_Center = student.CDAC_Center;
                db.SaveChanges();
            }
        }

        public void DeleteStudent(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }
    }
}