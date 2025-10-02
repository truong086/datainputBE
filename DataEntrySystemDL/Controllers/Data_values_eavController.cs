using DataEntrySystemDL.Common;
using DataEntrySystemDL.Service;
using DataEntrySystemDL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataEntrySystemDL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Data_values_eavController : ControllerBase
    {
        private readonly IData_values_eavService _service;
        public Data_values_eavController(IData_values_eavService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route(nameof(Add))]
        public async Task<PayLoad<data_values_eavDTO>> Add (data_values_eavDTO data)
        {
            return await _service.Add(data);    
        }

        [HttpPut]
        [Route(nameof(Update))]
        public async Task<PayLoad<data_values_eavDTO>> Update(int id, data_values_eavDTO data)
        {
            return await _service.Updata(id, data); 
        }
    }
}
