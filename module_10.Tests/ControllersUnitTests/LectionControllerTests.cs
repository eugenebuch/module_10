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
    class LectionControllerTests
    {
        private LectionController LectionController { get; set; }

        private Mock<IDTOService<LectionDTO, Lection>> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IDTOService<LectionDTO, Lection>>();
            Mock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());
            Mock.Setup(service => service.CreateAsync(It.IsAny<LectionDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.UpdateAsync(It.IsAny<LectionDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.Find(It.IsAny<Func<Lection, bool>>()))
                .Returns(PutFindTest());
            Mock.Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(ViewModel());

            LectionController = new LectionController(Mock.Object, new WebMapper());
        }

        [Test]
        public async Task GetLection_ValidCall()
        {
            var response = await LectionController.Get(1);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task GetLection_BadRequest()
        {
            var response = await LectionController.Get(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PostLection_ValidCall()
        {
            var response = await LectionController.Post(ViewModel().Result);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task PostLection_BadRequest()
        {
            var response = await LectionController.Post(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PutLection_ValidCall()
        {
            var response = await LectionController.Put(ViewModel().Result);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task PutLection_BadRequest()
        {
            var response = await LectionController.Put(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public async Task PutLection_NotFound()
        {
            Mock.Setup(service => service.Find(It.IsAny<Func<Lection, bool>>()))
                .Returns(PutNotFoundTest());

            var response = await LectionController.Put(ViewModel().Result);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.NotFound, code.StatusCode);
        }

        [Test]
        public async Task DeleteLection_ValidCall()
        {
            var response = await LectionController.Delete(1);
            var code = ((ObjectResult)response.Result).StatusCode;

            Assert.AreEqual((int)HttpStatusCode.OK, code);
        }

        [Test]
        public async Task DeleteLection_BadRequest()
        {
            var response = await LectionController.Delete(null);
            var code = (StatusCodeResult)response.Result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        private static async Task<LectionDTO> GetTest()
        {
            var Lection = new LectionDTO()
            {
                Id = 1,
                Name = "Math",
                LecturerId = 1,
                LectionHomework = null
            };
            return Lection;
        }

        private static async Task<LectionViewModel> ViewModel()
        {
            var Lection = new LectionViewModel
            {
                Id = 1,
                Name = "Math",
                LecturerId = 1,
            };
            return Lection;
        }

        private static IEnumerable<LectionDTO> PutNotFoundTest()
        {
            var Lection = new List<LectionDTO>();
            return Lection;
        }

        private static IEnumerable<LectionDTO> PutFindTest()
        {
            var Lection = new List<LectionDTO>()
            {
                new LectionDTO()
                {
                    Id = 1,
                    Name = "Math",
                    LecturerId = 1,
                    LectionHomework = null
                }
            };
            return Lection;
        }
    }
}
