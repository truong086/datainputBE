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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route(nameof(FindOne))]
        public async Task<PayLoad<object>> FindOne(int id)
        {
            return await _userService.FindOne(id);
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route(nameof(FindAll))]
        public async Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20)
        {
            return await _userService.findAll(name, page, pageSize);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(nameof(Add))]
        public async Task<PayLoad<userDTO>> Add(userDTO user)
        {
            return await _userService.add(user);
        }

        [HttpPut]
        [Route(nameof(update))]
        public async Task<PayLoad<userDTO>> update(int id, userDTO user)
        {
            return await _userService.update(id, user);
        }

        [HttpDelete]
        [Route(nameof(delete))]
        public async Task<PayLoad<string>> delete(int id)
        {
            return await _userService.delete(id);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(nameof(login))]
        public async Task<PayLoad<object>> login(LoginDTO data)
        {
            return await _userService.login(data);
        }

        [HttpPost]
        [Route(nameof(logout))]
        public async Task<PayLoad<string>> logout()
        {
            return await _userService.logout(); 
        }
    }
}
