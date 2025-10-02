using DataEntrySystemDL.Common;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Service
{
    public interface IData_rows_eavService
    {
        Task<PayLoad<data_rows_eavDTO>> Add(data_rows_eavDTO data);
        Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20);
        Task<PayLoad<object>> FindOne(int id);
        Task<PayLoad<string>> Delete(int id);
        Task<PayLoad<data_rows_eavDTO>> Update(int id, data_rows_eavDTO data);
    }
}
