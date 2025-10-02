using DataEntrySystemDL.Common;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Service
{
    public interface IData_values_eavService
    {
        Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20);
        Task<PayLoad<object>> FindOne(int id);
        Task<PayLoad<data_values_eavDTO>> Add(data_values_eavDTO data);
        Task<PayLoad<data_values_eavDTO>> Updata(int id, data_values_eavDTO data);
        Task<PayLoad<string>> Delete (int id);
    }
}
