using Microsoft.AspNetCore.Mvc;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.DAL.Entities;
using module_10.WEB.Controllers;
using module_10.WEB.Mappers;
using module_10.WEB.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace module_10.Tests.ControllersUnitTests
{
    [TestFixture]
    class StudentControllerTests
    {
        private StudentController StudentController { get; set; }

        private Mock<IDTOService<StudentDTO, Student>> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IDTOService<StudentDTO, Student>>();
            Mock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());
            Mock.Setup(service => service.CreateAsync(It.IsAny<StudentDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.UpdateAsync(It.IsAny<StudentDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.Find(It.IsAny<Func<Student, bool>>()))
                .Returns(PutFindTest());
            Mock.Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(ViewModel());

            StudentController = new StudentController(Mock.Object, new WEB_Mapper());
        }

        [Test]
        public async Task GetStudent_ValidCall()
        {
            var response = await StudentController.Get(1);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task GetStudent_BadRequest()
        {
            var response = await StudentController.Get(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PostStudent_ValidCall()
        {
            var response = await StudentController.Post(ViewModel().Result);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task PostStudent_BadRequest()
        {
            var response = await StudentController.Post(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PutStudent_ValidCall()
        {
            var response = await StudentController.Put(ViewModel().Result);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task PutStudent_BadRequest()
        {
            var response = await StudentController.Put(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PutStudent_NotFound()
        {
            Mock.Setup(service => service.Find(It.IsAny<Func<Student, bool>>()))
                .Returns(PutNotFoundTest());

            var response = await StudentController.Put(ViewModel().Result);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.NotFound, code.StatusCode);
        }

        [Test]
        public async Task DeleteStudent_ValidCall()
        {
            var response = await StudentController.Delete(1);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task DeleteStudent_BadRequest()
        {
            var response = await StudentController.Delete(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        private static async Task<StudentDTO> GetTest()
        {
            var student = new StudentDTO
            {
                Id = 1,
                FirstName = "Denis",
                LastName = "Kolosov",
                AverageMark = (float)4.67,
                MissedLections = 0,
                StudentHomework = null
            };
            return student;
        }

        private static async Task<StudentViewModel> ViewModel()
        {
            var student = new StudentViewModel
            {
                Id = 1,
                FirstName = "Denis",
                LastName = "Kolosov",
            };
            return student;
        }

        private static IEnumerable<StudentDTO> PutNotFoundTest()
        {
            var student = new List<StudentDTO>();
            return student;
        }

        private static IEnumerable<StudentDTO> PutFindTest()
        {
            var student = new List<StudentDTO>()
            {
                new StudentDTO()
                {
                    Id = 1,
                    FirstName = "Denis",
                    LastName = "Kolosov",
                    AverageMark = (float)4.67,
                    MissedLections = 0,
                    StudentHomework = null
                }
            };
            return student;
        }
    }
}
