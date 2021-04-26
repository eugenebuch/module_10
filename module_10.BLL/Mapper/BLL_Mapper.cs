using AutoMapper;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using module_10.DAL.Entities;

namespace module_10.BLL.Mapper
{
    public class BLL_Mapper : IBLL_Mapper
    {
        public IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Homework, HomeworkDTO>().ReverseMap();
                cfg.CreateMap<Student, StudentDTO>().ReverseMap();
                cfg.CreateMap<Lecturer, LecturerDTO>().ReverseMap();
                cfg.CreateMap<Lection, LectionDTO>().ReverseMap();
            }).CreateMapper();
        }
    }
}
