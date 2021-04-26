using Microsoft.EntityFrameworkCore;
using module_10.DAL.Entities;
using System;

namespace module_10.DAL.DataAccess
{
    public sealed class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated(); // comment it to migrate
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Lection> Lections { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=data;trusted_connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student[]
                {
                    new Student { Id = 1, FirstName = "Eugene", LastName = "Buchenkov",
                        AverageMark = 4.3f, MissedLections = 0},

                    new Student { Id = 2, FirstName = "Kirill", LastName = "Makarov",
                    AverageMark = 0.0f, MissedLections = 3},

                    new Student { Id = 3, FirstName = "Mikhail", LastName = "Eremin",
                        AverageMark = 4.2f, MissedLections = 0},

                    new Student { Id = 4, FirstName = "Alexander", LastName = "Nikitin",
                        AverageMark = 1.5f, MissedLections = 2},

                    new Student { Id = 5, FirstName = "Ivan", LastName = "Shkikavy",
                        AverageMark = 3.9f, MissedLections = 0}
                });

            modelBuilder.Entity<Lecturer>().HasData(
                new Lecturer[]
                {
                    new Lecturer { Id = 1, FirstName = "Anastasia", LastName = "Yarovikova" },
                    new Lecturer { Id = 2, FirstName = "Vlad", LastName = "Sinotov" },
                    new Lecturer { Id = 3, FirstName = "Ilya", LastName = "Maddyson" }
                });

            modelBuilder.Entity<Lection>().HasData(
                new Lection[]
                {
                    new Lection { Id = 1, Name = "Maths", LecturerId = 1},
                    new Lection { Id = 2, Name = "Physics", LecturerId = 1},
                    new Lection { Id = 3, Name = "English", LecturerId = 3}
                });

            modelBuilder.Entity<Homework>().HasData(
                new Homework[]
                {
                    new Homework { Id = 1, StudentId = 1, LectionId = 1, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2020,12, 23)},
                    new Homework { Id = 2, StudentId = 2, LectionId = 1, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2020,12, 23)},
                    new Homework { Id = 3, StudentId = 3, LectionId = 1, StudentPresence = true,
                        HomeworkPresence = true, Mark = 4, Date = new DateTime(2020,12, 23)},
                    new Homework { Id = 4, StudentId = 4, LectionId = 1, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2020,12, 23)},
                    new Homework { Id = 5, StudentId = 5, LectionId = 1, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2020,12, 23)},

                    new Homework { Id = 6, StudentId = 1, LectionId = 2, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2020,12, 28)},
                    new Homework { Id = 7, StudentId = 2, LectionId = 2, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2020,12, 28)},
                    new Homework { Id = 8, StudentId = 3, LectionId = 2, StudentPresence = true,
                        HomeworkPresence = true, Mark = 4, Date = new DateTime(2020,12, 28)},
                    new Homework { Id = 9, StudentId = 4, LectionId = 2, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2020,12, 28)},
                    new Homework { Id = 10, StudentId = 5, LectionId = 2, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2020,12, 28)},

                    new Homework { Id = 11, StudentId = 1, LectionId = 3, StudentPresence = true,
                        HomeworkPresence = true, Mark = 4, Date = new DateTime(2020,12, 31)},
                    new Homework { Id = 12, StudentId = 2, LectionId = 3, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2020,12, 31)},
                    new Homework { Id = 13, StudentId = 3, LectionId = 3, StudentPresence = true,
                        HomeworkPresence = true, Mark = 4, Date = new DateTime(2020,12, 31)},
                    new Homework { Id = 14, StudentId = 4, LectionId = 3, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2020,12, 31)},
                    new Homework { Id = 15, StudentId = 5, LectionId = 3, StudentPresence = true,
                        HomeworkPresence = true, Mark = 1, Date = new DateTime(2020,12, 31)}
                });
        }
    }
}
