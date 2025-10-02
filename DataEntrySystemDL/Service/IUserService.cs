using DataEntrySystemDL.Common;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Service
{
    public interface IUserService
    {
        Task<PayLoad<userDTO>> add (userDTO userDTO);
        Task<PayLoad<userDTO>> update(int id, userDTO userDTO);
        Task<PayLoad<object>> findAll(string? name, int page = 1, int pageSize = 20);
        Task<PayLoad<string>> delete(int id);
        Task<PayLoad<object>> FindOne(int id);
        Task<PayLoad<object>> login(LoginDTO data);
        Task<PayLoad<string>> logout();
    }
}
