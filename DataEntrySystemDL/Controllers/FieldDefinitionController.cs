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
    public class FieldDefinitionController : ControllerBase
    {
        private readonly IFieldDefinitionService _service;
        public FieldDefinitionController(IFieldDefinitionService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route(nameof(Add))]
        public async Task<PayLoad<FieldDefinitionDTO>> Add (FieldDefinitionDTO fieldDefinitionDTO)
        {
            return await _service.Add(fieldDefinitionDTO);
        }

        [HttpPost]
        [Route(nameof(AddUserRoleTable))]
        public async Task<PayLoad<row_access_rulesDTO>> AddUserRoleTable(row_access_rulesDTO data)
        {
            return await _service.AddUserRoleTable(data);
        }

        [HttpPost]
        [Route(nameof(CheckRole))]
        public async Task<PayLoad<object>> CheckRole(int table)
        {
            return await _service.CheckRole(table);
        }

        [HttpPost]
        [Route(nameof(AddRole))]
        public async Task<PayLoad<RolePermissionsDTO>> AddRole(RolePermissionsDTO data)
        {
            return await _service.AddRole(data);
        }

        [HttpPut]
        [Route(nameof(Update))]
        public async Task<PayLoad<FieldDefinitionDTO>> Update(int id, FieldDefinitionDTO data)
        {
            return await _service.Update(id, data);
        }
    }
}
