using Microsoft.AspNetCore.Mvc;
using module_10.BLL.Interfaces;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.BLL.Services.Report;
using System;
using System.Text;

namespace module_10.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public enum FileType
        {
            Json,
            Xml
        }

        ///<summary>
        ///GET : Report/Student
        ///</summary>
        [HttpGet("Student")]
        public IActionResult GetStudentReport(FileType type, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return BadRequest();

            ISerializer serializer = type switch {
                FileType.Json => new JsonReportSerializer(),
                FileType.Xml => new XmlReportSerializer(),
                _ => null
            };

            if (serializer is null)
                return BadRequest();

            var content = _reportService.MakeStudentReport(firstName, lastName, serializer.Serialize);
            return File(Encoding.UTF8.GetBytes(content),
                System.Net.Mime.MediaTypeNames.Application.Json,
                $"{DateTime.Now.ToShortDateString()} - {firstName} {lastName} Attendance {type}.txt");
        }


        ///<summary>
        ///GET : Report/Lection
        ///</summary>
        [HttpGet("Lection")]
        public IActionResult GetLectionReport(FileType type, string lectionName)
        {
            if (string.IsNullOrEmpty(lectionName))
                return BadRequest();

            ISerializer serializer = type switch
            {
                FileType.Json => new JsonReportSerializer(),
                FileType.Xml => new XmlReportSerializer(),
                _ => null
            };

            if (serializer is null)
                return BadRequest();

            var content = _reportService.MakeLectionReport(lectionName, serializer.Serialize);
            return File(Encoding.UTF8.GetBytes(content),
                System.Net.Mime.MediaTypeNames.Application.Json,
                $"{DateTime.Now.ToShortDateString()} - {lectionName} Attendance {type}.txt");
        }
    }
}
