using DataEntrySystemDL.Common;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Service
{
    public interface IRoleService
    {
        Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 10);  
        Task<PayLoad<roleDTO>> Add(roleDTO role);
        Task<PayLoad<roleDTO>> Update(int id, roleDTO role);
        Task<PayLoad<string>> Delete(int id);
        Task<PayLoad<object>> FindOne(int id);
    }
}
