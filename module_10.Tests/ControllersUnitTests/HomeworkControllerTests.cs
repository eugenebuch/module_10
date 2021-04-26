using Microsoft.AspNetCore.Mvc;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.DAL.Entities;
using module_10.WEB.Controllers;
using module_10.WEB.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using module_10.WEB.Mapper;

namespace module_10.Tests.ControllersUnitTests
{
    [TestFixture]
    class HomeworkControllerTests
    {
        private HomeworkController HomeworkController { get; set; }

        private Mock<IDTOService<HomeworkDTO, Homework>> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IDTOService<HomeworkDTO, Homework>>();
            Mock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());
            Mock.Setup(service => service.CreateAsync(It.IsAny<HomeworkDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.UpdateAsync(It.IsAny<HomeworkDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.Find(It.IsAny<Func<Homework, bool>>()))
                .Returns(PutFindTest());
            Mock.Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(ViewModel());

            HomeworkController = new HomeworkController(Mock.Object, new WebMapper());
        }

        [Test]
        public async Task GetHomework_ValidCall()
        {
            var response = await HomeworkController.Get(1);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task GetHomework_BadRequest()
        {
            var response = await HomeworkController.Get(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PostHomework_ValidCall()
        {
            var response = await HomeworkController.Post(ViewModel().Result);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task PostHomework_BadRequest()
        {
            var response = await HomeworkController.Post(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PutHomework_ValidCall()
        {
            var response = await HomeworkController.Put(ViewModel().Result);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task PutHomework_BadRequest()
        {
            var response = await HomeworkController.Put(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PutHomework_NotFound()
        {
            Mock.Setup(service => service.Find(It.IsAny<Func<Homework, bool>>()))
                .Returns(PutNotFoundTest());

            var response = await HomeworkController.Put(ViewModel().Result);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.NotFound, code.StatusCode);
        }

        [Test]
        public async Task DeleteHomework_ValidCall()
        {
            var response = await HomeworkController.Delete(1);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task DeleteHomework_BadRequest()
        {
            var response = await HomeworkController.Delete(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        private static async Task<HomeworkDTO> GetTest()
        {
            var homework = new HomeworkDTO()
            {
                Id = 1,
                StudentId = 1,
                LectionId = 1,
                StudentPresence = true,
                HomeworkPresence = true,
                Mark = 2,
                Date = DateTime.Now
            };
            return homework;
        }

        private static async Task<HomeworkViewModel> ViewModel()
        {
            var homework = new HomeworkViewModel
            {
                Id = 1,
                StudentId = 1,
                LectionId = 1,
                StudentPresence = true,
                HomeworkPresence = true,
                Mark = 2,
                Date = DateTime.Now
            };
            return homework;
        }

        private static IEnumerable<HomeworkDTO> PutNotFoundTest()
        {
            var homework = new List<HomeworkDTO>();
            return homework;
        }

        private static IEnumerable<HomeworkDTO> PutFindTest()
        {
            var homework = new List<HomeworkDTO>()
            {
                new HomeworkDTO()
                {
                    Id = 1,
                    StudentId = 1,
                    LectionId = 1,
                    StudentPresence = true,
                    HomeworkPresence = true,
                    Mark = 2,
                    Date = DateTime.Now
                }
            };
            return homework;
        }
    }
}
