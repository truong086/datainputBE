using AutoMapper;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<users, userDTO>();
            CreateMap<userDTO, users>();
        }
    }
}
