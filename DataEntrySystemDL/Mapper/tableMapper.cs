using AutoMapper;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Mapper
{
    public class tableMapper : Profile
    {
        public tableMapper()
        {
            CreateMap<tables, tableDTO>();
            CreateMap<tableDTO, tables>()
                .ForMember(x => x.project_id, b => b.Ignore()); // Bỏ qua trường dữ liệu "project_id" không map
        }
    }
}
