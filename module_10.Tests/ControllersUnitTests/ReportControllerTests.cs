using Microsoft.AspNetCore.Mvc;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.BLL.Services.Report;
using module_10.WEB.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace module_10.Tests.ControllersUnitTests
{
    [TestFixture]
    class ReportControllerTests
    {
        private ReportController ReportController { get; set; }

        private Mock<IReportService> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IReportService>();
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeJsonReportData());
            Mock.Setup(service => service.MakeLectionReport(It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeJsonReportData());

            ReportController = new ReportController(Mock.Object);
        }

        [Test]
        public void GetStudentJsonReport_ValidCall()
        {
            var response = ReportController.GetStudentReport(ReportController.FileType.JSON,
                "Harry", "Potter");

            Mock.Verify(m => m.MakeStudentReport(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
            Assert.IsInstanceOf<FileContentResult>(response);
        }

        [Test]
        public void GetStudentJsonReport_BadRequestResult()
        {
            var response = ReportController.GetStudentReport(ReportController.FileType.JSON,
                null, "Potter");
            var code = (StatusCodeResult)response;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public void GetLectionJsonReport_ValidCall()
        {
            var response = ReportController.GetLectionReport(ReportController.FileType.JSON,
                "Math");

            Mock.Verify(m => m.MakeLectionReport(It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
            Assert.IsInstanceOf<FileContentResult>(response);
        }

        [Test]
        public void GetLectionJsonReport_BadRequestResult()
        {
            var response = ReportController.GetLectionReport(ReportController.FileType.JSON,
                null);
            var code = (StatusCodeResult)response;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public void GetStudentXmlReport_ValidCall()
        {
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            Mock.Setup(service => service.MakeLectionReport(It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);

            var response = ReportController.GetStudentReport(ReportController.FileType.XML,
                "Harry", "Potter");

            Mock.Verify(m => m.MakeStudentReport(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
            Assert.IsInstanceOf<FileContentResult>(response);
        }

        [Test]
        public void GetStudentXmlReport_BadRequestResult()
        {
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            Mock.Setup(service => service.MakeLectionReport(It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);

            var response = ReportController.GetStudentReport(ReportController.FileType.XML,
                null, "Potter");
            var code = (StatusCodeResult)response;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        [Test]
        public void GetLectionXmlReport_ValidCall()
        {
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            Mock.Setup(service => service.MakeLectionReport(It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);

            var response = ReportController.GetLectionReport(ReportController.FileType.XML,
                "Math");

            Mock.Verify(m => m.MakeLectionReport(It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
            Assert.IsInstanceOf<FileContentResult>(response);
        }

        [Test]
        public void GetLectionXmlReport_BadRequestResult()
        {
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            Mock.Setup(service => service.MakeLectionReport(It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);

            var response = ReportController.GetLectionReport(ReportController.FileType.XML,
                null);
            var code = (StatusCodeResult)response;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, code.StatusCode);
        }

        private static string MakeJsonReportData()
        {
            var attendance = new List<Attendance>()
            {
                new Attendance()
                {
                    LectionName = "Math",
                    LecturerName = "Harry Potter",
                    StudentName = "Harry Potter",
                    HomeworkPresence = true,
                    StudentPresence = true,
                    Mark = 4,
                    Date = DateTime.Now
                }
            };
            var serializer = new JSONReportSerializer();
            return serializer.Serialize(attendance);
        }

        private static string MakeXmlReportData()
        {
            var attendance = new List<Attendance>()
            {
                new Attendance()
                {
                    LectionName = "Math",
                    LecturerName = "Harry Potter",
                    StudentName = "Harry Potter",
                    HomeworkPresence = true,
                    StudentPresence = true,
                    Mark = 4,
                    Date = DateTime.Now
                }
            };
            var serializer = new XMLReportSerializer();
            return serializer.Serialize(attendance);
        }
    }
}
