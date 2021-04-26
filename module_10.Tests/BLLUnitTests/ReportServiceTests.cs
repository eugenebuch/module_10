using Microsoft.Extensions.Logging.Abstractions;
using module_10.BLL.DTO;
using module_10.BLL.Infrastructure;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.BLL.Services.Report;
using module_10.DAL.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace module_10.Tests.BLLUnitTests
{
    [TestFixture]
    class ReportServiceTests
    {
        private ReportService ReportService { get; set; }

        private Mock<IDTOService<StudentDTO, Student>> StudentServiceMock { get; set; }

        private Mock<IDTOService<LectionDTO, Lection>> LectionServiceMock { get; set; }

        private Mock<IDTOService<LecturerDTO, Lecturer>> LecturerServiceMock { get; set; }

        [SetUp]
        public void SetUp()
        {
            StudentServiceMock = new Mock<IDTOService<StudentDTO, Student>>();
            LectionServiceMock = new Mock<IDTOService<LectionDTO, Lection>>();
            LecturerServiceMock = new Mock<IDTOService<LecturerDTO, Lecturer>> ();
            StudentServiceMock.Setup(service => service.Find(It.IsAny<Func<Student, bool>>()))
                .Returns(FindStudents());
            StudentServiceMock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetAsyncStudent());
            LectionServiceMock.Setup(service => service.Find(It.IsAny<Func<Lection, bool>>()))
                .Returns(FindLections());
            LectionServiceMock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetAsyncLection());
            LecturerServiceMock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetAsyncLecturer());

            ReportService = new ReportService(StudentServiceMock.Object,
                LectionServiceMock.Object,
                LecturerServiceMock.Object,
                new NullLoggerFactory());
        }

        [Test]
        public void MakeStudentReport_ValidCall()
        {
            ReportService.MakeStudentReport("Eugene", "Buchenkov");

            StudentServiceMock.Verify(s => s.Find(It.IsAny<Func<Student, bool>>()));
            LectionServiceMock.Verify(l => l.GetAsync(It.IsAny<int>()));
            LecturerServiceMock.Verify(p => p.GetAsync(It.IsAny<int>()));
        }

        [Test]
        public void MakeStudentReport_ThrowsValidationException()
        {
            StudentServiceMock.Setup(service => service.Find(It.IsAny<Func<Student, bool>>()))
                .Returns(FindStudentsValidationExceptionTest());

            Assert.Throws<ValidationException>(() => ReportService.MakeStudentReport("Kirill", "Kononov"));
        }

        [Test]
        public void MakeLectionReport_ValidCall()
        {
            ReportService.MakeLectionReport("Physics");

            LectionServiceMock.Verify(s => s.Find(It.IsAny<Func<Lection, bool>>()));
            StudentServiceMock.Verify(l => l.GetAsync(It.IsAny<int>()));
            LecturerServiceMock.Verify(p => p.GetAsync(It.IsAny<int>()));
        }

        [Test]
        public void MakeLectionReport_ThrowsValidationException()
        {
            LectionServiceMock.Setup(service => service.Find(It.IsAny<Func<Lection, bool>>()))
                .Returns(FindLectionsValidationExceptionTest());

            Assert.Throws<ValidationException>(() => ReportService.MakeLectionReport("Math"));
        }

        private static IEnumerable<StudentDTO> FindStudents()
        {
            var students = new List<StudentDTO>()
            {
                new StudentDTO
                {
                    Id = 1,
                    FirstName = "Eugene",
                    LastName = "Buchenkov",
                    AverageMark = (float) 4.67,
                    MissedLections = 0,
                    StudentHomework = new List<HomeworkDTO>()
                    {
                        new HomeworkDTO()
                        {
                            Id = 1,
                            StudentId = 1,
                            LectionId = 1,
                            StudentPresence = true,
                            HomeworkPresence = true,
                            Mark = 4,
                            Date = DateTime.Now
                        }
                    }
                }
            };
            return students;
        }

        private static async Task<StudentDTO> GetAsyncStudent()
        {
            var student = new StudentDTO
            {
                Id = 1,
                FirstName = "Eugene",
                LastName = "Buchenkov",
                AverageMark = (float)4.67,
                MissedLections = 0,
                StudentHomework = null
            };
            return student;
        }

        private static IEnumerable<StudentDTO> FindStudentsValidationExceptionTest()
        {
            var students = new List<StudentDTO>();
            return students;
        }

        private static IEnumerable<LectionDTO> FindLections()
        {
            var Lections = new List<LectionDTO>()
            {
                new LectionDTO
                {
                    Id = 1,
                    Name = "Physics",
                    LecturerId = 1,
                    LectionHomework = new List<HomeworkDTO>()
                    {
                        new HomeworkDTO()
                        {
                            Id = 1,
                            StudentId = 1,
                            LectionId = 1,
                            StudentPresence = true,
                            HomeworkPresence = true,
                            Mark = 4,
                            Date = DateTime.Now
                        }
                    }
                }
            };
            return Lections;
        }

        private static async Task<LectionDTO> GetAsyncLection()
        {
            var Lection = new LectionDTO
            {
                Id = 1,
                Name = "Physics",
                LecturerId = 1,
                LectionHomework = null
            };
            return Lection;
        }

        private static IEnumerable<LectionDTO> FindLectionsValidationExceptionTest()
        {
            var Lections = new List<LectionDTO>();
            return Lections;
        }

        private static async Task<LecturerDTO> GetAsyncLecturer()
        {
            var Lecturer = new LecturerDTO
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
