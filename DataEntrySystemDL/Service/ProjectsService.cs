using AutoMapper;
using DataEntrySystemDL.Common;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;
using System.Xml.Linq;

namespace DataEntrySystemDL.Service
{
    public class ProjectsService : IProjectsService
    {
        private readonly DBContext _context;
        private IMapper _mapper;
        private readonly IUserTokenService _userToken;
        public ProjectsService(DBContext context, IMapper mapper, IUserTokenService userToken)
        {
            _context = context;
            _mapper = mapper;
            _userToken = userToken;

        }
        public async Task<PayLoad<projectDTO>> Add(projectDTO data)
        {
            try
            {
                var user = _userToken.name();

                var checkName = _context.projects.FirstOrDefault(x => x.name == data.name && !x.deleted);
                if (checkName != null) return await Task.FromResult(PayLoad<projectDTO>.CreatedFail(Status.DATANULL));

                var checkUser = _context.users.FirstOrDefault(x => x.id == int.Parse(user) && !x.deleted);
                if(checkUser == null) return await Task.FromResult(PayLoad<projectDTO>.CreatedFail(Status.DATANULL));

                var mapData = _mapper.Map<projects>(data);
                mapData.users = checkUser;
                mapData.owner_id = checkUser.id;
                mapData.is_active = true;

                _context.projects.Add(mapData);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<projectDTO>.Successfully(data));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<projectDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<string>> Delete(int id)
        {
            try
            {
                var checkId = _context.projects.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null) return await Task.FromResult(PayLoad<string>.CreatedFail(Status.DATANULL));

                checkId.deleted = true;

                _context.projects.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<string>.Successfully(Status.SUCCESS));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<string>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20)
        {
            try
            {
                var data = _context.projects.Where(x => !x.deleted).Select(x => new
                {
                    x.id,
                    x.name,
                    x.description,
                    user_create = x.users.username,
                    x.is_active,
                    x.cretoredit
                }).ToList();

                if (!string.IsNullOrEmpty(name))
                    data = data.Where(x => x.name.Contains(name)).ToList();

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
                var checkId = _context.projects.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null)
                    return await Task.FromResult(PayLoad<object>.CreatedFail(Status.DATANULL));

                return await Task.FromResult(PayLoad<object>.Successfully(checkId));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<projectDTO>> Update(int id, projectDTO data)
        {
            try
            {
                var user = _userToken.name();
                var checkId = _context.projects.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null) return await Task.FromResult(PayLoad<projectDTO>.CreatedFail(Status.DATANULL));

                var checkName = _context.projects.FirstOrDefault(x => x.name == data.name && !x.deleted && x.id != checkId.id);
                if(checkName != null) return await Task.FromResult(PayLoad<projectDTO>.CreatedFail(Status.DATATONTAI));

                var checkUser = _context.users.FirstOrDefault(x => x.id == Convert.ToInt32(user) && !x.deleted);
                if(checkUser == null) return await Task.FromResult(PayLoad<projectDTO>.CreatedFail(Status.DATANULL));

                checkId.name = data.name;
                checkId.description = data.description;
                checkId.cretoredit = checkUser.username + " đã sửa bản ghi vào lúc " + DateTime.Now;

                _context.projects.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<projectDTO>.Successfully(data));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<projectDTO>.CreatedFail(ex.Message));
            }
        }
    }
}
