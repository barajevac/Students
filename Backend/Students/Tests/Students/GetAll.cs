using NUnit.Framework;
using Students.DAL.StudentRepository;
using Students.Domain;
using System.Collections.Generic;
using System;
using Moq;
using Students.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace Tests.Students
{
    public class GetAll
    {
        private Mock<IStudentRepository> _studentRepo { get; set; }
        private Mock<IStudentRepository> _studentRepoEmpty { get; set; }
        private List<Student> _studentsList { get; set; }
        private StudentController _studentController { get; set; }


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
        }

        private void CreateMocks()
        {
            Mock<IStudentRepository> studentRepo = new Mock<IStudentRepository>() { CallBase = true };
            studentRepo.Setup(s => s.Read()).Returns(_studentsList);
            _studentRepo = studentRepo;

            Mock<IStudentRepository> studentRepoEmpty = new Mock<IStudentRepository>() { CallBase = true };
            studentRepoEmpty.Setup(s => s.Read()).Returns(new List<Student>());
            _studentRepoEmpty = studentRepoEmpty;
        }

        private void CreateController(Mock<IStudentRepository> studentRepo)
        {
            _studentController = new StudentController(studentRepo.Object);
        }


        [Test]
        public void GetAllSuccessTest()
        {
            ActionResult result = null;
            CreateController(_studentRepo);
            Assert.DoesNotThrow(() => { result = _studentController.Get(); });

            var okResult = result as OkObjectResult;
            List<Student> StudentList = (List<Student>)okResult.Value;

            _studentRepo.Verify(s => s.Read(), Times.Once());
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, _studentsList);
            Assert.AreEqual(2, StudentList.Count);
        }

        [Test]
        public void GetAllSuccessTestWhenThereIsNoStudents()
        {
            ActionResult result = null;
            CreateController(_studentRepoEmpty);
            Assert.DoesNotThrow(() => { result = _studentController.Get(); });

            var okResult = result as OkObjectResult;
            List<Student> StudentList = (List<Student>)okResult.Value;

            _studentRepoEmpty.Verify(s => s.Read(), Times.Once());
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, new List<Student>());
            Assert.AreEqual(0, StudentList.Count);
        }

    }
}
