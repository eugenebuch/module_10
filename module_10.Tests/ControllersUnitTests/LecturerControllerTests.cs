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
    class LecturerControllerTests
    {
        private LecturerController LecturerController { get; set; }

        private Mock<IDTOService<LecturerDTO, Lecturer>> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IDTOService<LecturerDTO, Lecturer>>();
            Mock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());
            Mock.Setup(service => service.CreateAsync(It.IsAny<LecturerDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.UpdateAsync(It.IsAny<LecturerDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.Find(It.IsAny<Func<Lecturer, bool>>()))
                .Returns(PutFindTest());
            Mock.Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(ViewModel());

            LecturerController = new LecturerController(Mock.Object, new WEB_Mapper());
        }

        [Test]
        public async Task GetLecturer_ValidCall()
        {
            var response = await LecturerController.Get(1);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task GetLecturer_BadRequest()
        {
            var response = await LecturerController.Get(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PostLecturer_ValidCall()
        {
            var response = await LecturerController.Post(ViewModel().Result);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task PostLecturer_BadRequest()
        {
            var response = await LecturerController.Post(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PutLecturer_ValidCall()
        {
            var response = await LecturerController.Put(ViewModel().Result);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task PutLecturer_BadRequest()
        {
            var response = await LecturerController.Put(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PutLecturer_NotFound()
        {
            Mock.Setup(service => service.Find(It.IsAny<Func<Lecturer, bool>>()))
                .Returns(PutNotFoundTest());

            var response = await LecturerController.Put(ViewModel().Result);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.NotFound, code.StatusCode);
        }

        [Test]
        public async Task DeleteLecturer_ValidCall()
        {
            var response = await LecturerController.Delete(1);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task DeleteLecturer_BadRequest()
        {
            var response = await LecturerController.Delete(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        private static async Task<LecturerDTO> GetTest()
        {
            var Lecturer = new LecturerDTO()
            {
                Id = 1,
                FirstName = "Andrew",
                LastName = "Sokolov",
                Lections = null
            };
            return Lecturer;
        }

        private static async Task<LecturerViewModel> ViewModel()
        {
            var Lecturer = new LecturerViewModel
            {
                Id = 1,
                FirstName = "Andrew",
                LastName = "Sokolov",
            };
            return Lecturer;
        }

        private static IEnumerable<LecturerDTO> PutNotFoundTest()
        {
            var Lecturer = new List<LecturerDTO>();
            return Lecturer;
        }

        private static IEnumerable<LecturerDTO> PutFindTest()
        {
            var Lecturer = new List<LecturerDTO>()
            {
                new LecturerDTO()
                {
                    Id = 1,
                    FirstName = "Andrew",
                    LastName = "Sokolov",
                    Lections = null
                }
            };
            return Lecturer;
        }
    }
}
