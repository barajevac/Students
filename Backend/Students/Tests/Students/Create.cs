using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Students.Controllers;
using Students.DAL.StudentRepository;
using Students.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Students
{
    class Create
    {
        private Mock<IStudentRepository> _studentRepo { get; set; }
        private List<Student> _studentsList { get; set; }
        private StudentController _studentController { get; set; }
        private Student _studentsToAdd { get; set; }


        [SetUp]
        public void Setup()
        {
            CreateTestData();
            CreateMocks();
        }

        private void CreateTestData()
        {
            _studentsList = new List<Student>()
            {
                new Student()
                {
                    Id = new Guid("2ddeb3ad-cf0d-4893-9c77-3fefbd73c57f"),
                    FirstName = "FirstName 1",
                    LastName="LastName 1",
                    Mail = "Mail 1",
                    Phone = "111"
                },
                 new Student()
                {
                    Id = new Guid("ecf86cb4-50b9-4b9a-b663-ef2a1d66728f"),
                    FirstName = "FirstName 22",
                    LastName="LastName 2",
                    Mail = "Mail 2",
                    Phone = "222"
                }
            };

            _studentsToAdd = new Student()
            {
                Id = Guid.NewGuid(),
                FirstName = "FirstName 33",
                LastName = "LastName 3",
                Mail = "Mail 3",
                Phone = "333"
            };
        }

        private void CreateMocks()
        {
            Mock<IStudentRepository> studentRepo = new Mock<IStudentRepository>() { CallBase = true };
            studentRepo.Setup(s => s.Create(It.IsAny<Student>())).Callback(() =>
            {
                _studentsList.Add(_studentsToAdd);
            });
            _studentRepo = studentRepo;
        }

        private void CreateController(Mock<IStudentRepository> studentRepo)
        {
            _studentController = new StudentController(studentRepo.Object);
        }

        [Test]
        public void CreateSuccessTest()
        {
            ActionResult result = null;
            CreateController(_studentRepo);

            Assert.DoesNotThrow(() => { result = _studentController.Post(_studentsToAdd); });
            OkObjectResult okResult = (OkObjectResult)result;

            _studentRepo.Verify(s => s.Create(It.IsAny<Student>()), Times.Once());
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(3, _studentsList.Count);
            Assert.AreNotEqual(_studentsToAdd.Id, _studentsList[0].Id);
            Assert.AreNotEqual(_studentsToAdd.Id, _studentsList[1].Id);

        }
    }
}
