using AutoMapper;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Mapper
{
    public class projectMapper : Profile
    {
        public projectMapper()
        {
            CreateMap<projectDTO, projects>();
            CreateMap<projects, projectDTO>();
        }
    }
}
