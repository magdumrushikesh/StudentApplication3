﻿using System;
using System.Collections.Generic;
using System.Linq;
using StudentApplication3.Models;

namespace StudentApplication3.DAL
{
    public class StudentDAL
    {
        private StudentDBEntities3 db = new StudentDBEntities3();

        public List<Student> GetAllStudents() => db.Students.ToList();
        public Student GetStudentById(int id) => db.Students.FirstOrDefault(s => s.Id == id);
        public List<center> GetAllCenters() => db.centers.ToList();

        public void AddStudent(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
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