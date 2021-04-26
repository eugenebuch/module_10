using Microsoft.Extensions.Logging;
using module_10.BLL.Interfaces;
using module_10.DAL.Entities;
using System;

namespace module_10.BLL.Services.HomeworkHandler
{
    public class EmailSender : IMessageSender
    {
        public void Send(Student student, ILogger logger = null)
        {
            logger?.LogInformation($"[{DateTime.UtcNow}]: Student [Name: {student.FirstName} {student.LastName}, " +
                                  $"ID: {student.Id}] with average mark - {student.AverageMark}." +
                                  " Do something with this!");
        }
    }
}
