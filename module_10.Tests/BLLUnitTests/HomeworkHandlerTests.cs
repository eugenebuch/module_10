using Microsoft.Extensions.Logging.Abstractions;
using module_10.BLL.Infrastructure;
using module_10.BLL.Interfaces;
using module_10.BLL.Services.HomeworkHandler;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace module_10.Tests.BLLUnitTests
{
    [TestFixture]
    public class HomeworkHandlerTests
    {
        private readonly Homework _homework = new Homework
        {
            Id = 1,
            StudentId = 1,
            LectionId = 1,
            StudentPresence = true,
            HomeworkPresence = true,
            Mark = 5,
            Date = new DateTime(2021, 01, 02)
        };

        private HomeworkHandler HomeworkHandler { get; set; }

        private Mock<IRepository<Student>> StudentRepoMock { get; set; }

        private Mock<Func<string, IMessageSender>> MessageServiceAccessorMock { get; set; }

        private Mock<IMessageSender> MessageSender { get; set; }

        [SetUp]
        public void SetUp()
        {
            MessageServiceAccessorMock = new Mock<Func<string, IMessageSender>>();
            MessageSender = new Mock<IMessageSender>();
            MessageServiceAccessorMock.Setup(_ => _.Invoke(It.IsAny<string>()))
                .Returns(MessageSender.Object);

            StudentRepoMock = new Mock<IRepository<Student>>();
            StudentRepoMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetStudentWithSendingMessages());

            HomeworkHandler = new HomeworkHandler(StudentRepoMock.Object,
                MessageServiceAccessorMock.Object,
                new NullLoggerFactory());
        }

        [Test]
        public async Task UpdateAsync_ThreeMessagesWillBeSent_ValidCall()
        {
            await HomeworkHandler.UpdateAsync(_homework, HomeworkHandler.UpdateType.AddHomework);

            StudentRepoMock.Verify(m => m.GetAsync(It.IsAny<int>()));
            StudentRepoMock.Verify(m => m.Update(It.IsAny<Student>()));
            MessageServiceAccessorMock.Verify(m => m.Invoke(It.IsAny<string>())
                .Send(It.IsAny<Student>(), new NullLoggerFactory().CreateLogger("")), Times.Exactly(3));
        }

        [Test]
        public async Task UpdateAsync_NoMessagesWillBeSent_ValidCall()
        {
            StudentRepoMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetStudentWithoutSendingMessages());
            await HomeworkHandler.UpdateAsync(_homework, HomeworkHandler.UpdateType.AddHomework);

            StudentRepoMock.Verify(m => m.GetAsync(It.IsAny<int>()));
            StudentRepoMock.Verify(m => m.Update(It.IsAny<Student>()));
            MessageServiceAccessorMock.Verify(m => m.Invoke(It.IsAny<string>())
                .Send(It.IsAny<Student>(), new NullLoggerFactory().CreateLogger("")), Times.Exactly(0));
        }

        [Test]
        public void UpdateAsync_ThrowsValidationException()
        {
            StudentRepoMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkHandler
                .UpdateAsync(_homework, HomeworkHandler.UpdateType.AddHomework));
        }

        private static async Task<Student> GetStudentWithSendingMessages()
        {
            var student = new Student
            {
                Id = 1,
                FirstName = "Eugene",
                LastName = "Buchenkov",
                AverageMark = 0,
                MissedLections = 4,
                StudentHomework = new List<Homework>()
                {
                    new Homework()
                    {
                        Id = 1,
                        StudentId = 1,
                        LectionId = 1,
                        StudentPresence = false,
                        HomeworkPresence = false,
                        Mark = 0,
                        Date = new DateTime(2021,01,01)
                    }
                }
            };
            return student;
        }

        private static async Task<Student> GetStudentWithoutSendingMessages()
        {
            var student = new Student
            {
                Id = 1,
                FirstName = "Eugene",
                LastName = "Buchenkov",
                AverageMark = 5,
                MissedLections = 0,
                StudentHomework = new List<Homework>()
                {
                    new Homework()
                    {
                        Id = 1,
                        StudentId = 1,
                        LectionId = 1,
                        StudentPresence = true,
                        HomeworkPresence = true,
                        Mark = 5,
                        Date = new DateTime(2021,01,01)
                    },
                    new Homework()
                    {
                        Id = 2,
                        StudentId = 1,
                        LectionId = 1,
                        StudentPresence = true,
                        HomeworkPresence = true,
                        Mark = 5,
                        Date = new DateTime(2021,01,21)
                    },
                    new Homework()
                    {
                        Id = 3,
                        StudentId = 1,
                        LectionId = 1,
                        StudentPresence = true,
                        HomeworkPresence = true,
                        Mark = 5,
                        Date = new DateTime(2021,02,10)
                    }
                }
            };
            return student;
        }

        private static async Task<Student> GetExceptionTest()
        {
            return null;
        }
    }
}
