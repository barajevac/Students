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
    class Update
    {
        private Mock<IStudentRepository> _studentRepo { get; set; }
        private Mock<IStudentRepository> _studentRepoEmpty { get; set; }

        private List<Student> _studentsList { get; set; }
        private Student _studentsToUpdate { get; set; }

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

            _studentsToUpdate = new Student()
            {
                Id = new Guid("2ddeb3ad-cf0d-4893-9c77-3fefbd73c57f"),
                FirstName = "FirstName updated",
                LastName = "LastName updated",
                Mail = "Mail updated",
                Phone = "phone updated"
            };

        }

        private void CreateMocks()
        {
            Mock<IStudentRepository> studentRepo = new Mock<IStudentRepository>() { CallBase = false };
            studentRepo.Setup(s => s.Read()).Returns(_studentsList);

            studentRepo.Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<Student>())).Callback(() =>
            {
                _studentsList[0] = _studentsToUpdate;
            }).Returns(true);
            _studentRepo = studentRepo;


            Mock<IStudentRepository> studentRepoEmpty = new Mock<IStudentRepository>() { CallBase = false };
            studentRepoEmpty.Setup(s => s.Read()).Returns(_studentsList);

            studentRepoEmpty.Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<Student>())).Returns(false);
            _studentRepoEmpty = studentRepoEmpty;


        }

        private void CreateController(Mock<IStudentRepository> studentRepo)
        {
            _studentController = new StudentController(studentRepo.Object);
        }

        [Test]
        public void UpdateSuccessTest()
        {
            ActionResult result = null;
            CreateController(_studentRepo);

            Assert.DoesNotThrow(() => { result = _studentController.Put(_studentsList[0].Id, _studentsToUpdate); });

            OkResult okResult = (OkResult)result;

            _studentRepo.Verify(s => s.Update(It.IsAny<Guid>(), It.IsAny<Student>()), Times.Once());

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(_studentsToUpdate, _studentsList[0]);
        }

        [Test]
        public void UpdateNotFoundTest()
        {
            ActionResult result = null;
            CreateController(_studentRepoEmpty);


            Assert.DoesNotThrow(() => { result = _studentController.Put(_studentsList[0].Id, _studentsToUpdate); });

            NotFoundResult notFoundResult = (NotFoundResult)result;
            _studentRepoEmpty.Verify(s => s.Update(It.IsAny<Guid>(), It.IsAny<Student>()), Times.Once());
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreNotEqual(_studentsToUpdate, _studentsList[0]);
        }
    }
}
