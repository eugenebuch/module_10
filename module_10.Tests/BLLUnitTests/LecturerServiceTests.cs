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
    public class LecturerServiceTests
    {
        private readonly LecturerDTO _LecturerDTO = new LecturerDTO
        {
            Id = 1,
            FirstName = "Eugene",
            LastName = "Buchenkov",
            Lections = null
        };

        private LecturerService LecturerService { get; set; }

        private Mock<IRepository<Lecturer>> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IRepository<Lecturer>>();
            Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllTest());
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());

            LecturerService = new LecturerService(Mock.Object, new BLL_Mapper(), new NullLoggerFactory());
        }

        [Test]
        public void GetAllAsync_ValidCall()
        {
            var Lecturers = LecturerService.GetAllAsync().Result.ToList();

            Mock.Verify(m => m.GetAllAsync());

            for (var i = 1; i < GetAllTest().Result.Count(); ++i)
            {
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Id, Lecturers[i].Id);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].FirstName, Lecturers[i].FirstName);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].LastName, Lecturers[i].LastName);
            }
        }

        [Test]
        public void GetAllAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LecturerService.GetAllAsync());
        }

        [Test]
        public void GetAsync_ValidCall()
        {
            const int id = 1;
            var Lecturer = LecturerService.GetAsync(id).Result;

            Mock.Verify(m => m.GetAsync(id));
            Assert.AreEqual(GetTest().Result.Id, Lecturer.Id);
            Assert.AreEqual(GetTest().Result.FirstName, Lecturer.FirstName);
            Assert.AreEqual(GetTest().Result.LastName, Lecturer.LastName);
        }

        [Test]
        public void GetAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LecturerService.GetAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await LecturerService.GetAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UpdateAsync_ValidCall()
        {
            await LecturerService.UpdateAsync(_LecturerDTO);

            Mock.Verify(m => m.Update(It.IsAny<Lecturer>()));
        }

        [Test]
        public void UpdateAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LecturerService
                .UpdateAsync(_LecturerDTO));
        }

        [Test]
        public async Task DeleteAsync_ValidCall()
        {
            await LecturerService.DeleteAsync(It.IsAny<int>());

            Mock.Verify(m => m.Delete(It.IsAny<Lecturer>()));
        }

        [Test]
        public void DeleteAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LecturerService.DeleteAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await LecturerService
                .DeleteAsync(It.IsAny<int>()));
        }

        private static async Task<IEnumerable<Lecturer>> GetAllTest()
        {
            var Lecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    FirstName = "Eugene",
                    LastName = "Buchenkov",
                    Lections = null
                },
                new Lecturer {
                    Id = 2,
                    FirstName = "Alexander",
                    LastName = "Nikitin",
                    Lections = null
                }
            };
            return Lecturers;
        }

        private static async Task<IEnumerable<Lecturer>> GetAllExceptionTest()
        {
            var Lecturers = new List<Lecturer>();
            return Lecturers;
        }

        private static async Task<Lecturer> GetExceptionTest()
        {
            return null;
        }

        private static async Task<Lecturer> GetTest()
        {
            var Lecturer = new Lecturer
            {
                Id = 1,
                FirstName = "Eugene",
                LastName = "Buchenkov",
                Lections = null
            };
            return Lecturer;
        }
    }
}
