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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route(nameof(FindAll))]
        public async Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20)
        {
            return await _service.FindAll(name, page, pageSize);    
        }

        [HttpGet]
        [Route(nameof(FindOne))]
        public async Task<PayLoad<object>> FindOne(int id)
        {
            return await _service.FindOne(id);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(nameof(Add))]
        public async Task<PayLoad<roleDTO>> Add(roleDTO role)
        {
            return await _service.Add(role);
        }

        [HttpPut]
        [Route(nameof(Update))]
        public async Task<PayLoad<roleDTO>> Update(int id, roleDTO role)
        {
            return await _service.Update(id, role);
        }

        [HttpDelete]
        [Route(nameof(delete))]
        public async Task<PayLoad<string>> delete(int id)
        {
            return await _service.Delete(id);
        }
    }
}
