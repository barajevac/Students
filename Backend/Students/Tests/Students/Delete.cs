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
    class Delete
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
            studentRepo.Setup(s => s.Delete(It.IsAny<Guid>())).Callback(() =>
            {
                _studentsList.RemoveAt(0);
            }).Returns(true);
            _studentRepo = studentRepo;

            Mock<IStudentRepository> studentRepoEmpty = new Mock<IStudentRepository>() { CallBase = true };
            studentRepoEmpty.Setup(s => s.Delete(It.IsAny<Guid>())).Returns(false);
            _studentRepoEmpty = studentRepoEmpty;
        }

        private void CreateController(Mock<IStudentRepository> studentRepo)
        {
            _studentController = new StudentController(studentRepo.Object);
        }

        [Test]
        public void DeleteSuccessTest()
        {
            ActionResult result = null;
            CreateController(_studentRepo);
            Guid id = new Guid("2ddeb3ad-cf0d-4893-9c77-3fefbd73c57f");

            Assert.DoesNotThrow(() => { result = _studentController.Delete(id); });
            OkResult okResult = result as OkResult;
            _studentRepo.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(1, _studentsList.Count);
            Assert.AreNotEqual(id, _studentsList[0].Id);
        }

        [Test]
        public void DeleteUnsuccessTest()
        {
            ActionResult result = null;
            CreateController(_studentRepoEmpty);

            //random guid
            Guid id = new Guid("6612ff81-1296-4ca4-ac2b-f0c95bae1bc0");

            Assert.DoesNotThrow(() => { result = _studentController.Delete(id); });
            NotFoundResult notFoundResult = (NotFoundResult)result;
            _studentRepoEmpty.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(2, _studentsList.Count);
            Assert.AreNotEqual(id, _studentsList[0].Id);
            Assert.AreNotEqual(id, _studentsList[1].Id);

        }

    }
}
