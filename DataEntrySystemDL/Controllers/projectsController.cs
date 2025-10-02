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
    public class projectsController : ControllerBase
    {
        private readonly IProjectsService _service;
        public projectsController(IProjectsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route(nameof(FindAll))]
        public async Task<PayLoad<object>> FindAll (string? name, int page = 1, int pageSize = 20)
        {
            return await _service.FindAll(name, page, pageSize);
        }

        [HttpGet]
        [Route(nameof(FindOne))]
        public async Task<PayLoad<object>> FindOne(int id)
        {
            return await _service.FindOne(id);
        }

        [HttpPost]
        [Route(nameof(Add))]
        public async Task<PayLoad<projectDTO>> Add(projectDTO data)
        {
            return await _service.Add(data);
        }

        [HttpPut]
        [Route(nameof(Update))]
        public async Task<PayLoad<projectDTO>> Update(int id, projectDTO data)
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
