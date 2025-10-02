using AutoMapper;
using DataEntrySystemDL.Common;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;
using System.Data;

namespace DataEntrySystemDL.Service
{
    public class FieldDefinitionService : IFieldDefinitionService
    {
        private readonly DBContext _context;
        private IMapper _mapper;
        private readonly IUserTokenService _userToken;
        public FieldDefinitionService(DBContext context, IMapper mapper, IUserTokenService userToken)
        {
            _context = context;
            _mapper = mapper;
            _userToken = userToken;
        }
        public async Task<PayLoad<FieldDefinitionDTO>> Add(FieldDefinitionDTO data)
        {
            try
            {
                var checkName = _context.fielddefinitions.FirstOrDefault(x => x.field_name == data.field_name && x.table_id == data.table_id && !x.deleted);
                if (checkName != null) return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(Status.DATATONTAI));

                var checkTable = _context.tables.FirstOrDefault(x => x.id == data.table_id && !x.deleted);
                if (checkTable == null) return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(Status.DATANULL));

                var mapData = _mapper.Map<FieldDefinition>(data);
                if (Enum.TryParse<FieldType>(data.fieldType, true, out var typeData)) // Kiểm tra xem dữ liệu chuyền lên xem dữ liệu có đúng dữ liệu trong Enum hay không
                {
                    
                    //var idlast = _context.FieldDefinitions.OrderBy(x => x.id).LastOrDefault();
                    var idlast = _context.fielddefinitions.Any() ?  _context.fielddefinitions.Max(x => x.id) : 0; // Dùng "Max()" để lấy ra id lớn nhất
                    int nextId = idlast + 1;
                    mapData.fieldType = typeData;
                    mapData.field_key = RanDomCode.geneAction(8) + "_" + nextId.ToString();
                    mapData.sort_order = data.sort_order;
                    mapData.is_required = true;
                    mapData.table = checkTable;
                    mapData.table_id = checkTable.id;

                    _context.fielddefinitions.Add(mapData);
                    if(_context.SaveChanges() > 0)
                    {
                        var dataNew = _context.fielddefinitions.OrderByDescending(x => x.created_at).FirstOrDefault();

                        if(data.listRoles != null && data.listRoles.Count > 0) 
                        {
                            var list = new List<field_permissions>();
                            foreach(var item in data.listRoles)
                            {
                                var checkRole = _context.roles.FirstOrDefault(x => x.id == item.role_id && !x.deleted);
                                if (checkRole == null) return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(Status.DATANULL));

                                var dataRole = new field_permissions
                                {
                                    can_read = item.can_read,
                                    can_write = item.can_write,
                                    role = checkRole,
                                    role_id = checkRole.id,
                                    field_id = dataNew.id,
                                    fieldDefinition = dataNew
                                };

                                list.Add(dataRole);
                            }

                            _context.field_permissions.AddRange(list);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(Status.DATANULL));
                    }

                }
                else
                {
                    return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(Status.ENUMERROR));
                }

                return await Task.FromResult(PayLoad<FieldDefinitionDTO>.Successfully(data));
                
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<object>> CheckRole(int table)
        {
            try
            {
                var user = _userToken.name();
                var checkId = _context.tables.FirstOrDefault(x => x.id == table && !x.deleted);
                var checkUser = _context.users.FirstOrDefault(x => x.id == int.Parse(user) && !x.deleted);

                if (checkId == null || checkUser == null) return await Task.FromResult(PayLoad<object>.CreatedFail(Status.DATANULL));

                var checkRole = _context.row_access_rules.Select(x => new
                {
                    x.table_id,
                    x.user_id,
                    type = x.access_type.ToString(),
                    x.filter_conditions,
                    x.deleted
                }).FirstOrDefault(x => x.table_id == checkId.id && x.user_id == checkUser.id && !x.deleted);
                if (checkRole == null) return await Task.FromResult(PayLoad<object>.CreatedFail(Status.DATANULL));

                return await Task.FromResult(PayLoad<object>.Successfully(checkRole));

            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }

        public Task<PayLoad<string>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PayLoad<object>> FindAll(string? name, int page = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public Task<PayLoad<object>> FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PayLoad<FieldDefinitionDTO>> Update(int id, FieldDefinitionDTO data)
        {
            try
            {
                var checkId = _context.fielddefinitions.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null) return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(Status.DATANULL));

                var checkName = _context.fielddefinitions.FirstOrDefault(x => x.field_name == data.field_name && !x.deleted && x.id != checkId.id);
                if (checkName != null) return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(Status.DATATONTAI));

                checkId.field_name = data.field_name;

                _context.fielddefinitions.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<FieldDefinitionDTO>.Successfully(data));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<FieldDefinitionDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<row_access_rulesDTO>> AddUserRoleTable(row_access_rulesDTO data)
        {
            try
            {
                var user = _userToken.name();
                var checkTable = _context.tables.FirstOrDefault(x => x.id == data.table_id && !x.deleted);
                var checkUser = _context.users.FirstOrDefault(x => x.id == data.user_id && !x.deleted);
                
                var checkUserCreate = _context.users.FirstOrDefault(x => x.id == Convert.ToInt32(user) && !x.deleted);

                if (checkUserCreate == null 
                    || checkTable == null 
                    || checkUser == null) 
                    return await Task.FromResult(PayLoad<row_access_rulesDTO>.CreatedFail(Status.DATANULL));

                var checkRoleUser = _context.user_roles.FirstOrDefault(x => x.user_id == checkUser.id && !x.deleted);
                if(checkRoleUser == null) return await Task.FromResult(PayLoad<row_access_rulesDTO>.CreatedFail(Status.DATANULL));

                var checkRole = _context.roles.FirstOrDefault(x => x.id == checkRoleUser.role_id && !x.deleted);

                if (Enum.TryParse<access_types>(data.type, true, out var valueType))
                {
                    var dataNew = new row_access_rules
                    {
                        access_type = valueType,
                        cretoredit = checkUserCreate.username + " đã add " + checkUser.username + " vào lúc " + DateTime.Now,
                        role = checkRole,
                        role_id = checkRole.id,
                        user_id = checkUser.id,
                        users = checkUser,
                        table_id = checkTable.id,
                        table = checkTable
                    };

                    _context.row_access_rules.Add(dataNew);
                    _context.SaveChanges();
                }
                else
                {
                    return await Task.FromResult(PayLoad<row_access_rulesDTO>.CreatedFail(Status.DATANULL));
                }

                return await Task.FromResult(PayLoad<row_access_rulesDTO>.Successfully(data));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<row_access_rulesDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<RolePermissionsDTO>> AddRole(RolePermissionsDTO data)
        {
            try
            {
                var checkRole = _context.field_permissions.FirstOrDefault(x => x.field_id == data.id_field && x.role_id == data.id_role && !x.deleted);
                if (checkRole != null) return await Task.FromResult(PayLoad<RolePermissionsDTO>.CreatedFail(Status.DATATONTAI));

                var checkField = _context.fielddefinitions.FirstOrDefault(x => x.id == data.id_field && !x.deleted);
                var checkRoleId = _context.roles.FirstOrDefault(x => x.id == data.id_role && !x.deleted);

                if(checkField == null || checkRoleId == null) return await Task.FromResult(PayLoad<RolePermissionsDTO>.CreatedFail(Status.DATANULL));

                var dataNew = new field_permissions
                {
                    can_write = data.can_write,
                    can_read = data.can_read,
                    role = checkRoleId,
                    role_id = data.id_role,
                    fieldDefinition = checkField,
                    field_id = data.id_field
                };

                _context.field_permissions.Add(dataNew);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<RolePermissionsDTO>.Successfully(data));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<RolePermissionsDTO>.CreatedFail(ex.Message));
            }
        }
    }
}
