using Microsoft.AspNetCore.Mvc;
using Students.Domain;
using System;
using System.Collections.Generic;

namespace Students.DAL.StudentRepository
{
    public interface IStudentRepository
    {
        public void Create(Student student);
        public List<Student> Read();
        public Student Read(Guid studentId);
        public bool Update(Guid studentId, Student student);
        public bool Delete(Guid studentId);
    }
}
