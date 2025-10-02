using AutoMapper;
using DataEntrySystemDL.Common;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataEntrySystemDL.Service
{
    public class UserService : IUserService
    {
        private readonly DBContext _context;
        private IMapper _mapper;
        private Jwt _jwt;
        private readonly IUserTokenService _userTokenService;
        public UserService(DBContext context, IMapper mapper, IOptionsMonitor<Jwt> jwt, IUserTokenService userTokenService)
        {
            _context = context;
            _mapper = mapper;
            _jwt = jwt.CurrentValue;
            _userTokenService = userTokenService;
        }
        public async Task<PayLoad<userDTO>> add(userDTO userDTO)
        {
            try
            {
                var checkName = _context.users.FirstOrDefault(x => (x.username == userDTO.username || x.display_name == userDTO.display_name) && !x.deleted);
                if (checkName != null)
                    return await Task.FromResult(PayLoad<userDTO>.CreatedFail(Status.DATATONTAI));

                userDTO.password = EncryptionHelper.CreatePasswordHash(userDTO.password, _jwt.Key);

                var dataNew = _mapper.Map<users>(userDTO);
                _context.users.Add(dataNew);
                if(_context.SaveChanges() > 0)
                {
                    var dataNewSort = _context.users.OrderByDescending(x => x.created_at).FirstOrDefault();
                    var roleName = _context.roles.FirstOrDefault(x => x.name == "User" && !x.deleted);

                    if (dataNewSort == null || roleName == null)
                        return await Task.FromResult(PayLoad<userDTO>.CreatedFail("Role " + Status.DATANULL));

                    var dataUserRole = new user_roles
                    {
                        user_id = dataNewSort.id,
                        user = dataNewSort,
                        role = roleName,
                        role_id = roleName.id
                    };

                    _context.user_Roles.Add(dataUserRole);
                    _context.SaveChanges();
                }

                return await Task.FromResult(PayLoad<userDTO>.Successfully(userDTO));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<userDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<string>> delete(int id)
        {
            try
            {
                var checkId = _context.users.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null) return await Task.FromResult(PayLoad<string>.CreatedFail(Status.DATANULL));

                checkId.deleted = true;

                _context.users.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<string>.Successfully(Status.SUCCESS));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<string>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<object>> findAll(string? name, int page = 1, int pageSize = 20)
        {
            try
            {
                var data = _context.users.Where(x => !x.deleted).ToList();

                if (!string.IsNullOrEmpty(name))
                    data = data.Where(x => (x.username.Contains(name) || x.display_name.Contains(name)) && !x.deleted).ToList();

                var pageList = new PageList<object>(data, page - 1, pageSize);

                return await Task.FromResult(PayLoad<object>.Successfully(new
                {
                    data = pageList,
                    page,
                    pageList.pageSize,
                    pageList.totalCounts,
                    pageList.totalPages
                }));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<object>> FindOne(int id)
        {
            try
            {
                var checkId = _context.users.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null)
                    return await Task.FromResult(PayLoad<object>.CreatedFail(Status.DATANULL));

                return await Task.FromResult(PayLoad<object>.Successfully(checkId));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<object>> login(LoginDTO data)
        {
            try
            {
                var password = EncryptionHelper.CreatePasswordHash(data.password, _jwt.Key);
                var checkData = _context.users.Where(x => (x.username == data.username || x.email == data.username) && !x.deleted && x.password == password).Select(u => new
                {
                    u.id,
                    u.username,
                    u.display_name,
                    u.email,
                    roles = u.User_Roles.Select(x => new
                    {
                        x.id,
                        roleid = x.role.id,
                        rolename = x.role.name
                    }).ToList()
                }).FirstOrDefault();
                if (checkData == null) return await Task.FromResult(PayLoad<object>.CreatedFail(Status.DATANULL));

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(Status.IDAUTHENTICATION, checkData.id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, checkData.id.ToString())
                };

                return await Task.FromResult(PayLoad<object>.Successfully(new
                {
                    id = checkData.id,
                    username = checkData.username,
                    token = GenerateToken(claims),
                    role = checkData.roles
                }));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }

        private string GenerateToken(List<Claim>? claim)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creadentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwt.Issuer,
                _jwt.Issuer,
                expires: DateTime.Now.AddMinutes(12000),
                claims: claim,
                signingCredentials: creadentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<PayLoad<userDTO>> update(int id, userDTO userDTO)
        {
            try
            {
                var checkData = _context.users.FirstOrDefault(x => x.id == id && !x.deleted);
                if(checkData == null) return await Task.FromResult(PayLoad<userDTO>.CreatedFail(Status.DATANULL));

                var checkName = _context.users.FirstOrDefault(x => (x.display_name == userDTO.display_name || x.username == userDTO.username || x.email == userDTO.email) && x.id != checkData.id);
                if(checkName != null) return await Task.FromResult(PayLoad<userDTO>.CreatedFail(Status.DATATONTAI));

                checkData.display_name = userDTO.display_name;
                checkData.username = userDTO.username;
                checkData.email = userDTO.email;

                _context.users.Update(checkData);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<userDTO>.Successfully(userDTO));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<userDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<string>> logout()
        {
            _userTokenService.Logout();
            return await Task.FromResult(PayLoad<string>.Successfully(Status.SUCCESS));
        }
    }
}
