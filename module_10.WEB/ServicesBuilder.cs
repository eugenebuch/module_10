using Microsoft.Extensions.DependencyInjection;
using module_10.BLL.DTO;
using module_10.BLL.Infrastructure;
using module_10.BLL.Interfaces;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.BLL.Mapper;
using module_10.BLL.Services;
using module_10.BLL.Services.HomeworkHandler;
using module_10.BLL.Services.Report;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using module_10.DAL.Repositories;
using module_10.WEB.Interfaces;
using System;
using module_10.WEB.Mapper;

namespace module_10.WEB
{
    public class ServicesBuilder
    {
        public static void Build(IServiceCollection services)
        {
            services.AddScoped<IRepository<Homework>, HomeworkRepository>();
            services.AddScoped<IDTOService<HomeworkDTO, Homework>, HomeworkService>();

            services.AddScoped<IRepository<Lection>, LectionRepository>();
            services.AddScoped<IDTOService<LectionDTO, Lection>, LectionService>();

            services.AddScoped<IRepository<Lecturer>, LecturerRepository>();
            services.AddScoped<IDTOService<LecturerDTO, Lecturer>, LecturerService>();

            services.AddScoped<IRepository<Student>, StudentRepository>();
            services.AddScoped<IDTOService<StudentDTO, Student>, StudentService>();

            services.AddScoped<IHomeworkHandler, HomeworkHandler>();

            services.AddScoped<IReportService, ReportService>();

            services.AddSingleton<SMSSender>();
            services.AddSingleton<EmailSender>();

            services.AddTransient<Func<string, IMessageSender>>(serviceProvider => key =>
            {
                return key switch
                {
                    "SMS" => serviceProvider.GetService<SMSSender>(),
                    "Email" => serviceProvider.GetService<EmailSender>(),
                    _ => throw new ValidationException($"Wrong message type: {key}")
                };
            });

            services.AddSingleton<IBllMapper, BllMapper>();

            services.AddSingleton<IWebMapper, WebMapper>();
        }
    }
}
