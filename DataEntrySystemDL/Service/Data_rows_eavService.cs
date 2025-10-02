using DataEntrySystemDL.Common;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;

namespace DataEntrySystemDL.Service
{
    public class Data_rows_eavService : IData_rows_eavService
    {
        private readonly DBContext _context;
        private readonly IUserTokenService _userToken;
        public Data_rows_eavService(DBContext context, IUserTokenService userToken)
        {
            _context = context;
            _userToken = userToken;
        }
        public async Task<PayLoad<data_rows_eavDTO>> Add(data_rows_eavDTO data)
        {
            try
            {
                var user = _userToken.name();
                var checkTable = _context.tables.FirstOrDefault(x => x.id == data.table_id && !x.deleted);
                var checkUser = _context.users.FirstOrDefault(x => x.id == int.Parse(user) && !x.deleted);

                if (checkTable == null || checkUser == null) return await Task.FromResult(PayLoad<data_rows_eavDTO>.CreatedFail(Status.DATANULL));

                var dataNew = new data_rows_eav
                {
                    table = checkTable,
                    table_id = checkTable.id,
                    created_by = checkUser.id,
                    users = checkUser
                };

                _context.data_Rows_Eavs.Add(dataNew);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<data_rows_eavDTO>.Successfully(data));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<data_rows_eavDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<string>> Delete(int id)
        {
            try
            {
                var user = _userToken.name();
                var checkId = _context.data_Rows_Eavs.FirstOrDefault(x => x.id == id && !x.deleted);
                var checkUser = _context.users.FirstOrDefault(x => x.id == Convert.ToInt32(id) && !x.deleted);

                if (checkId == null || checkUser == null) return await Task.FromResult(PayLoad<string>.CreatedFail(Status.DATANULL));

                checkId.deleted = true;
                checkId.cretoredit = checkUser.username + " đã xóa và lúc " + DateTime.Now;

                _context.data_Rows_Eavs.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<string>.Successfully(Status.SUCCESS));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<string>.CreatedFail(ex.Message));
            }
        }

        public Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public Task<PayLoad<object>> FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PayLoad<data_rows_eavDTO>> Update(int id, data_rows_eavDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
