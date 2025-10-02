using DataEntrySystemDL.Common;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Service
{
    public interface IProjectsService
    {
        Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20);
        Task<PayLoad<object>> FindOne(int id);
        Task<PayLoad<string>> Delete(int id);
        Task<PayLoad<projectDTO>> Add(projectDTO data);
        Task<PayLoad<projectDTO>> Update(int id, projectDTO data);
    }
}
