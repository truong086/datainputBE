using DataEntrySystemDL.Common;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Service
{
    public interface IFieldDefinitionService
    {
        Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20);
        Task<PayLoad<object>> FindOne(int id);
        Task<PayLoad<string>> Delete(int id);
        Task<PayLoad<FieldDefinitionDTO>> Add(FieldDefinitionDTO data);
        Task<PayLoad<FieldDefinitionDTO>> Update(int id, FieldDefinitionDTO data);
        Task<PayLoad<object>> CheckRole(int table);
        Task<PayLoad<row_access_rulesDTO>> AddUserRoleTable(row_access_rulesDTO data);
        Task<PayLoad<RolePermissionsDTO>> AddRole(RolePermissionsDTO data);
    }
}
