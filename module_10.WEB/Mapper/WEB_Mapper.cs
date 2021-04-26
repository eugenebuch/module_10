using AutoMapper;
using module_10.BLL.DTO;
using module_10.WEB.Interfaces;
using module_10.WEB.ViewModels;

namespace module_10.WEB.Mappers
{
    public class WEB_Mapper : IWEB_Mapper
    {
        public IMapper Create() => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HomeworkDTO, HomeworkViewModel>().ReverseMap();
                cfg.CreateMap<StudentDTO, StudentViewModel>().ReverseMap();
                cfg.CreateMap<LecturerDTO, LecturerViewModel>().ReverseMap();
                cfg.CreateMap<LectionDTO, LectionViewModel>().ReverseMap();
            }).CreateMapper();
    }
}
