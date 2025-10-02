using AutoMapper;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Mapper
{
    public class FieldDefinitionMapper : Profile
    {
        public FieldDefinitionMapper()
        {
            CreateMap<FieldDefinition, FieldDefinitionDTO>();
            CreateMap<FieldDefinitionDTO, FieldDefinition>()
                .ForMember(a => a.fieldType, b => b.Ignore()) // Bỏ qua không map trường dữ liệu fieldType
                .ForMember(a => a.table_id, b => b.Ignore()); // Bỏ qua không map trường dữ liệu table_id
        }
    }
}
