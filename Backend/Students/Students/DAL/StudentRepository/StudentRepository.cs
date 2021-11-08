using Students.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Students.DAL.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        public void Create(Student student)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"..\..\Data.txt", true))
            {
                Write(student, streamWriter, true);
            }
        }

        public bool Delete(Guid studentId)
        {
            var listOfStudents = Read();
            var index = listOfStudents.FindIndex(s => s.Id.Equals(studentId));

            if (index > -1)
            {
                listOfStudents.RemoveAt(index);
                File.WriteAllText(@"..\..\Data.txt", String.Empty);
                foreach (var student in listOfStudents)
                {
                    using (StreamWriter streamWriter = new StreamWriter(@"..\..\Data.txt", true))
                    {
                        Write(student, streamWriter);
                    }
                }
                return true;
            }
            return false;
        }

        public List<Student> Read()
        {
            var dataRows = File.ReadAllLines(@"..\..\Data.txt");

            List<Student> students = new List<Student>();
            if (dataRows.Length > 0)
            {
                foreach (var row in dataRows)
                {
                    var rowData = row.Split(' ');
                    Student student = new Student()
                    {
                        Id = new Guid(rowData[0]),
                        FirstName = rowData[1],
                        LastName = rowData[2],
                        Mail = rowData[3],
                        Phone = rowData[4]
                    };
                    students.Add(student);
                }
            }
            return students;
        }

        public Student Read(Guid studentId)
        {
            var dataRows = File.ReadAllLines(@"..\..\Data.txt");

            if (dataRows.Length > 0)
            {
                foreach (var row in dataRows)
                {
                    var rowData = row.Split(' ');
                    if (rowData[0].Equals(studentId.ToString()))
                    {
                        return new Student()
                        {
                            Id = new Guid(rowData[0]),
                            FirstName = rowData[1],
                            LastName = rowData[2],
                            Mail = rowData[3],
                            Phone = rowData[4]
                        };
                    }
                }
            }
            return null;
        }

        public bool Update(Guid studentId, Student st)
        {
            var listOfStudents = Read();
            var index = listOfStudents.FindIndex(s => s.Id.Equals(studentId));

            if (index > -1)
            {
                listOfStudents[index] = st;
                listOfStudents[index].Id = studentId;

                File.WriteAllText(@"..\..\Data.txt", String.Empty);
                foreach (var student in listOfStudents)
                {
                    using (StreamWriter streamWriter = new StreamWriter(@"..\..\Data.txt", true))
                    {
                        Write(student, streamWriter);
                    }
                }
                return true;
            }
            return false;
        }

        private static void Write(Student student, StreamWriter streamWriter, bool isCreate = false)
        {
            if (isCreate)
                student.Id = Guid.NewGuid();

            var input = $"{student.Id} {student.FirstName} {student.LastName} {student.Mail} {student.Phone}";
            streamWriter.WriteLine(input);
            streamWriter.Close();
        }
    }
}
