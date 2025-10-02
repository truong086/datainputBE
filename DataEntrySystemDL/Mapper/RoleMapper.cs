using AutoMapper;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Mapper
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<roles, roleDTO>();
            CreateMap<roleDTO, roles>();
        }
    }
}
