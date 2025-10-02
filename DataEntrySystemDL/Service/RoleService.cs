using AutoMapper;
using DataEntrySystemDL.Common;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Service
{
    public class RoleService : IRoleService
    {
        private readonly DBContext _context;
        private IMapper _mapper;
        public RoleService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PayLoad<roleDTO>> Add(roleDTO role)
        {
            try
            {
                var checkName = _context.roles.FirstOrDefault(x => x.name == role.name && !x.deleted);
                if (checkName != null) return await Task.FromResult(PayLoad<roleDTO>.CreatedFail(Status.DATATONTAI));

                var mapData = _mapper.Map<roles>(role);

                _context.roles.Add(mapData);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<roleDTO>.Successfully(role));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<roleDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<string>> Delete(int id)
        {
            try { 
                var checkId = _context.roles.FirstOrDefault(x => x.id == id && !x.deleted);
                if(checkId == null) return await Task.FromResult(PayLoad<string>.CreatedFail(Status.DATANULL));

                checkId.deleted = true;

                _context.roles.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<string>.Successfully(Status.SUCCESS));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<string>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 10)
        {
            try
            {
                var data = _context.roles.Where(x => !x.deleted).ToList();

                if (!string.IsNullOrEmpty(name))
                    data = data.Where(x => x.name.Contains(name) && !x.deleted).ToList();

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
                var checkId = _context.roles.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null) return await Task.FromResult(PayLoad<object>.CreatedFail(Status.DATANULL));

                return await Task.FromResult(PayLoad<object>.Successfully(checkId));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<roleDTO>> Update(int id, roleDTO role)
        {
            try
            {
                var checkId = _context.roles.FirstOrDefault(x => x.id == id && !x.deleted);
                if(checkId == null) return await Task.FromResult(PayLoad<roleDTO>.CreatedFail(Status.DATANULL));

                var checkData = _context.roles.FirstOrDefault(x => x.name == role.name && !x.deleted && x.id != checkId.id);
                if(checkData != null) return await Task.FromResult(PayLoad<roleDTO>.CreatedFail(Status.DATATONTAI));

                checkId.name = role.name;
                checkId.description = role.description;

                _context.roles.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<roleDTO>.Successfully(role));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<roleDTO>.CreatedFail(ex.Message));
            }
        }
    }
}
