using System.Collections.Generic;
using StudentApplication3.Models;
using StudentApplication3.DAL;

namespace StudentApplication3.BAL
{
    public class StudentBAL
    {
        private StudentDAL studentDAL = new StudentDAL();

        public List<Student> GetAllStudents() => studentDAL.GetAllStudents();
        public Student GetStudentById(int id) => studentDAL.GetStudentById(id);
        public List<center> GetAllCenters() => studentDAL.GetAllCenters();

        public void AddStudent(Student student) => studentDAL.AddStudent(student);
        public void UpdateStudent(Student student) => studentDAL.UpdateStudent(student);
        public void DeleteStudent(int id) => studentDAL.DeleteStudent(id);
    }
}
