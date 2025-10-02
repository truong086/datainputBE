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
    public class Data_rows_eavController : ControllerBase
    {
        private readonly IData_rows_eavService _service;
        public Data_rows_eavController(IData_rows_eavService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route(nameof(Add))]
        public async Task<PayLoad<data_rows_eavDTO>> Add (data_rows_eavDTO data)
        {
            return await _service.Add(data);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<PayLoad<string>> Delete (int id)
        {
            return await _service.Delete(id);   
        }
    }
}
