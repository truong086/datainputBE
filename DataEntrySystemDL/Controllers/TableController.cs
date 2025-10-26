using DataEntrySystemDL.Common;
using DataEntrySystemDL.Service;
using DataEntrySystemDL.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataEntrySystemDL.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _service;
        public TableController(ITableService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route(nameof(FindAll))]
        public async Task<PayLoad<object>> FindAll (string? name, int page = 1, int pageSize = 20)
        {
            return await _service.FindAll (name, page, pageSize);
        }

        [HttpGet]
        [Route(nameof(FindOne))]
        public async Task<PayLoad<object>> FindOne(int id)
        {
            return await _service.FindOne(id);
        }

        [HttpGet]
        [Route(nameof(FindAllFieldByTable))]
        public async Task<PayLoad<object>> FindAllFieldByTable(int id)
        {
            return await _service.FindAllFieldBtTable(id);
        }

        [HttpGet]
        [Route(nameof(FindOneTable))]
        public async Task<PayLoad<object>> FindOneTable(int id, int page = 1, int pageSize = 20)
        {
            return await _service.FindOneTable(id, page, pageSize);
        }

        [HttpPost]
        [Route(nameof(Add))]
        public async Task<PayLoad<tableDTO>> Add(tableDTO data)
        {
            return await _service.Add(data);
        }

        [HttpPost]
        [Route(nameof(ExportExcel))]
        public IActionResult ExportExcel(int data)
        {
            var fileData = _service.ExcelExPost(data);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"Export_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            return File(fileData, contentType, fileName);
        }

        [HttpPost]
        [Route(nameof(importData))]
        public async Task<PayLoad<ImportTable>> importData([FromForm] ImportTable data)
        {
            return await _service.importData(data);
        }

        [HttpPut]
        [Route(nameof(Update))]
        public async Task<PayLoad<tableDTO>> Update(int id, tableDTO data)
        {
            return await _service.Update(id, data);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<PayLoad<string>> Delete(int id)
        {
            return await _service.Delete(id);
        }
    }
}
