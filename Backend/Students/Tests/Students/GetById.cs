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
    class GetById
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
            studentRepo.Setup(s => s.Read(It.IsAny<Guid>())).Returns(_studentsList[0]);
            _studentRepo = studentRepo;

            Mock<IStudentRepository> studentRepoEmpty = new Mock<IStudentRepository>() { CallBase = true };
            studentRepoEmpty.Setup(s => s.Read(It.IsAny<Guid>())).Returns(() => null);
            _studentRepoEmpty = studentRepoEmpty;
        }

        private void CreateController(Mock<IStudentRepository> studentRepo)
        {
            _studentController = new StudentController(studentRepo.Object);
        }

        [Test]
        public void GetByIdSuccessTest()
        {
            ActionResult result = null;
            CreateController(_studentRepo);
            Assert.DoesNotThrow(() => { result = _studentController.Get(new Guid("2ddeb3ad-cf0d-4893-9c77-3fefbd73c57f")); });

            var okResult = result as OkObjectResult;

            _studentRepo.Verify(s => s.Read(It.IsAny<Guid>()), Times.Once());
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetByIdNotFoundTestTest()
        {
            ActionResult result = null;
            CreateController(_studentRepoEmpty);

            //pass random guid
            Assert.DoesNotThrow(() => { result = _studentController.Get(new Guid("af76ee0f-3d82-42f7-a531-4bf80186a556")); });

            NotFoundResult notFountObjectResult = (NotFoundResult)result;

            _studentRepoEmpty.Verify(s => s.Read(It.IsAny<Guid>()), Times.Once());
            Assert.AreEqual(404, notFountObjectResult.StatusCode);
        }
    }
}
