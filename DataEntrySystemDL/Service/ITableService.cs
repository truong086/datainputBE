using DataEntrySystemDL.Common;
using DataEntrySystemDL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DataEntrySystemDL.Service
{
    public interface ITableService
    {
        Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20);
        Task<PayLoad<object>> FindOneTable(int id, int page = 1, int pageSize = 20);
        Task<PayLoad<object>> FindOne(int id);
        Task<PayLoad<tableDTO>> Add(tableDTO data);
        Task<PayLoad<tableDTO>> Update(int id, tableDTO data);
        Task<PayLoad<string>> Delete (int id);
        byte[] ExcelExPost (int id);
        Task<PayLoad<ImportTable>> importData(ImportTable data);
    }
}
