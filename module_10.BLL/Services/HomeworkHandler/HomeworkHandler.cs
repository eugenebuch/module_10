using Microsoft.Extensions.Logging;
using module_10.BLL.Infrastructure;
using module_10.BLL.Interfaces;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace module_10.BLL.Services.HomeworkHandler
{
    public class HomeworkHandler : IHomeworkHandler
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly ILogger _logger;
        private readonly IMessageSender _smsMessageSender;
        private readonly IMessageSender _emailMessageSender;

        public HomeworkHandler(IRepository<Student> studentRepository,
            Func<string, IMessageSender> messageSender,
            ILoggerFactory factory = null)
        {
            _studentRepository = studentRepository;
            _smsMessageSender = messageSender.Invoke("SMS");
            _emailMessageSender = messageSender.Invoke("Email");
            _logger = factory?.CreateLogger("Homework Handler");
        }

        public enum UpdateType
        {
            AddHomework,
            UpdateHomework,
            RemoveHomeworkWhileUpdate,
            RemoveHomework
        }

        public async Task UpdateAsync(Homework homework, UpdateType updateType, bool previousPresence = true)
        {
            var student = await _studentRepository.GetAsync(homework.StudentId);

            var validator = new Validations();
            validator.EntityValidation(student, _logger, nameof(student));

            student.AverageMark = AverageMarkCount(student.StudentHomework, homework.Mark, updateType);

            student.MissedLections= MissedLecturesCount(homework.StudentPresence, previousPresence,
                student.MissedLections, updateType);

            _studentRepository.Update(student);

            if (updateType == UpdateType.AddHomework || updateType == UpdateType.UpdateHomework)
                SendMessage(student);
        }

        private float AverageMarkCount(IReadOnlyCollection<Homework> studentHomework, int mark,
            UpdateType updateType)
        {
            float marks = studentHomework.Sum(work => work.Mark);
            return updateType == UpdateType.RemoveHomeworkWhileUpdate ?
                (marks - mark) / (studentHomework.Count - 1) : marks / studentHomework.Count;

        }

        private int MissedLecturesCount(bool presence, bool previousPresence,
            int missedLectures, UpdateType updateType)
        {
            if (updateType == UpdateType.UpdateHomework)
            {
                if (!previousPresence && presence)
                    return missedLectures - 1;

                if (previousPresence && !presence)
                    return missedLectures + 1;
            }

            if (!presence)
                return updateType == UpdateType.AddHomework ?
                    missedLectures + 1 : missedLectures - 1;

            return missedLectures;
        }

        private void SendMessage(Student student)
        {
            if (student.AverageMark < 4)
                _smsMessageSender.Send(student, _logger);

            if (student.MissedLections > 3)
            {
                _emailMessageSender.Send(student, _logger);
                _emailMessageSender.Send(student, _logger);
            }

        }
    }
}
