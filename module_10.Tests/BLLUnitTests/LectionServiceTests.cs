using Microsoft.Extensions.Logging.Abstractions;
using module_10.BLL.DTO;
using module_10.BLL.Infrastructure;
using module_10.BLL.Mapper;
using module_10.BLL.Services;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace module_10.Tests.BLLUnitTests
{
    [TestFixture]
    public class LectionServiceTests
    {
        private readonly LectionDTO _LectionDTO = new LectionDTO
        {
            Id = 1,
            Name = "Math",
            LecturerId = 1,
            LectionHomework = null
        };

        private LectionService LectionService { get; set; }

        private Mock<IRepository<Lection>> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IRepository<Lection>>();
            Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllTest());
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());

            LectionService = new LectionService(Mock.Object, new BLL_Mapper(), new NullLoggerFactory());
        }

        [Test]
        public void GetAllAsync_ValidCall()
        {
            var Lections = LectionService.GetAllAsync().Result.ToList();

            Mock.Verify(m => m.GetAllAsync());

            for (var i = 1; i < GetAllTest().Result.Count(); ++i)
            {
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Id, Lections[i].Id);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Name, Lections[i].Name);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].LecturerId, Lections[i].LecturerId);
            }
        }

        [Test]
        public void GetAllAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LectionService.GetAllAsync());
        }

        [Test]
        public void GetAsync_ValidCall()
        {
            const int id = 1;
            var Lection = LectionService.GetAsync(id).Result;

            Mock.Verify(m => m.GetAsync(id));
            Assert.AreEqual(GetTest().Result.Id, Lection.Id);
            Assert.AreEqual(GetTest().Result.Name, Lection.Name);
            Assert.AreEqual(GetTest().Result.LecturerId, Lection.LecturerId);
        }

        [Test]
        public void GetAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LectionService.GetAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await LectionService.GetAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UpdateAsync_ValidCall()
        {
            await LectionService.UpdateAsync(_LectionDTO);

            Mock.Verify(m => m.Update(It.IsAny<Lection>()));
        }

        [Test]
        public void UpdateAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LectionService
                .UpdateAsync(_LectionDTO));
        }

        [Test]
        public async Task DeleteAsync_ValidCall()
        {
            await LectionService.DeleteAsync(It.IsAny<int>());

            Mock.Verify(m => m.Delete(It.IsAny<Lection>()));
        }

        [Test]
        public void DeleteAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LectionService.DeleteAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await LectionService
                .DeleteAsync(It.IsAny<int>()));
        }

        private static async Task<IEnumerable<Lection>> GetAllTest()
        {
            var Lections = new List<Lection>
            {
                new Lection
                {
                    Id = 1,
                    Name = "English",
                    LecturerId = 1,
                    LectionHomework = null
                },
                new Lection {
                    Id = 2,
                    Name = ".NET",
                    LecturerId = 1,
                    LectionHomework = null
                }
            };
            return Lections;
        }

        private static async Task<IEnumerable<Lection>> GetAllExceptionTest()
        {
            var Lections = new List<Lection>();
            return Lections;
        }

        private static async Task<Lection> GetExceptionTest()
        {
            return null;
        }

        private static async Task<Lection> GetTest()
        {
            var Lection = new Lection
            {
                Id = 1,
                Name = "Physics",
                LecturerId = 1,
                LectionHomework = null
            };
            return Lection;
        }
    }
}
