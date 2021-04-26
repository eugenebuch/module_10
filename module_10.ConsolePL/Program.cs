using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using module_10.BLL.Infrastructure;
using module_10.BLL.Interfaces;
using module_10.BLL.Mapper;
using module_10.BLL.Services;
using module_10.BLL.Services.HomeworkHandler;
using module_10.BLL.Services.Report;
using module_10.DAL.DataAccess;
using module_10.DAL.Entities;
using module_10.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace module_10.ConsolePL
{
    class Program
    {
        private static StudentRepository studentsRepo;

        static void Main(string[] args)
        {
            new Program()?.TestMethodAsync();

            Console.ReadKey();
        }

        public async System.Threading.Tasks.Task TestMethodAsync()
        {
            var hw = new Homework
            {
                Id = 23,
                StudentId = 19,
                LectionId = 1,
                StudentPresence = false,
                HomeworkPresence = false,
                Mark = 0,
                Date = new DateTime(2020, 12, 25)
            };

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            var options = optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = Data; Trusted_Connection = True; ").Options;

            using (var db = new DataContext(options))
            {
                studentsRepo = new StudentRepository(db);
                Console.WriteLine("Students list:");

                var students = await studentsRepo.GetAllAsync();

                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} - mark: {student.AverageMark}");
                }
            }

            using (var db = new DataContext(options))
            {
                studentsRepo = new StudentRepository(db);
                Console.WriteLine("\nNow get students list:");
                var students = await studentsRepo.GetAllAsync();
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} - mark: {student.AverageMark}");
                }
            }
        }
    }
}
