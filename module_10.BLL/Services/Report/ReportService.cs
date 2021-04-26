using Microsoft.Extensions.Logging;
using module_10.BLL.DTO;
using module_10.BLL.Infrastructure;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace module_10.BLL.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IDTOService<StudentDTO, Student> _studentService;
        private readonly IDTOService<LectionDTO, Lection> _lectionService;
        private readonly IDTOService<LecturerDTO, Lecturer> _lecturerService;
        private readonly ILogger _logger;

        public ReportService(IDTOService<StudentDTO, Student> studentService,
            IDTOService<LectionDTO, Lection> lectionService,
            IDTOService<LecturerDTO, Lecturer> lecturerService,
            ILoggerFactory factory = null)
        {
            _studentService = studentService;
            _lectionService = lectionService;
            _lecturerService = lecturerService;
            _logger = factory?.CreateLogger("Report Service");
        }

        public string MakeStudentReport(string firstName, string lastName,
            Func<IEnumerable<Attendance>, string> serializer = null)
        {
            var students = _studentService.Find(s =>
                s.FirstName == firstName && s.LastName == lastName).ToList();

            if (students.Count == 0)
            {
                var mes = $"Entered student {firstName} {lastName} doesn't exist";
                _logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }

            if (serializer == null)
            {
                var jsonSerializer = new JsonReportSerializer();
                serializer = jsonSerializer.Serialize;
            }

            var attendance = from student in students
                             from homework in student.StudentHomework
                             select new Attendance()
                             {
                                 LectionName = _lectionService.GetAsync(homework.LectionId).Result.Name,
                                 LecturerName = $"{_lecturerService.GetAsync(_lectionService.GetAsync(homework.LectionId).Result.LecturerId).Result.FirstName} " +
                                                 $"{_lecturerService.GetAsync(_lectionService.GetAsync(homework.LectionId).Result.LecturerId).Result.LastName}",
                                 StudentName = $"{student.FirstName} {student.LastName}",
                                 StudentPresence = homework.StudentPresence,
                                 HomeworkPresence = homework.HomeworkPresence,
                                 Mark = homework.Mark,
                                 Date = homework.Date
                             };
            return serializer(attendance);
        }

        public string MakeLectionReport(string lectionName, Func<IEnumerable<Attendance>, string> serializer = null)
        {
            var lections = _lectionService.Find(l => l.Name == lectionName).ToList();

            if (lections.Count == 0)
            {
                var mes = $"Entered lection {lectionName} doesn't exist";
                _logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }

            if (serializer == null)
            {
                var jsonSerializer = new JsonReportSerializer();
                serializer = jsonSerializer.Serialize;
            }

            var attendance = from lection in lections
                             from homework in lection.LectionHomework
                             select new Attendance()
                             {
                                 LectionName = lection.Name,
                                 LecturerName = $"{_lecturerService.GetAsync(lection.LecturerId).Result.FirstName} " +
                                                 $"{_lecturerService.GetAsync(lection.LecturerId).Result.LastName}",
                                 StudentName = $"{_studentService.GetAsync(homework.StudentId).Result.FirstName} " +
                                               $"{_studentService.GetAsync(homework.StudentId).Result.LastName}",
                                 StudentPresence = homework.StudentPresence,
                                 HomeworkPresence = homework.HomeworkPresence,
                                 Mark = homework.Mark,
                                 Date = homework.Date
                             };
            return serializer(attendance);
        }
    }
}