using DataEntrySystemDL.Common;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;
using System.Text.Json;

namespace DataEntrySystemDL.Service
{
    public class Data_values_eavService : IData_values_eavService
    {
        private readonly DBContext _context;
        private readonly IUserTokenService _userToken;
        public Data_values_eavService(DBContext context, IUserTokenService userToken)
        {
            _context = context;
            _userToken = userToken;
        }
        public async Task<PayLoad<data_values_eavDTO>> Add(data_values_eavDTO data)
        {
            try
            {
                var user = _userToken.name();
                var checkUser = _context.users.FirstOrDefault(x => x.id == int.Parse(user) && !x.deleted);
                var checkField = _context.fielddefinitions.FirstOrDefault(x => x.id == data.field_id && !x.deleted);
                var checkRow = _context.data_rows_eavs.FirstOrDefault(x => x.id == data.row_id && !x.deleted);

                if (checkUser == null || checkField == null || checkRow == null) return await Task.FromResult(PayLoad<data_values_eavDTO>.CreatedFail(Status.DATANULL));
                var newData = new data_values_eav();
                newData.row_id = checkRow.id;
                newData.data_rows_eavs = checkRow;
                newData.field_definition = checkField;
                newData.field_id = data.field_id;
                newData.field_key = checkField.field_key;

                //if (checkField.fieldType == FieldType.Text)
                //    newData.value_text = data.value_text;
                //else if (checkField.fieldType == FieldType.Date || checkField.fieldType == FieldType.DateTime)
                //    newData.value_date = data.value_date;
                //else if (checkField.fieldType == FieldType.Number)
                //    newData.value_number = data.value_number;
                //else if (checkField.fieldType == FieldType.Boolean)
                //    newData.ValueBoolean = data.ValueBoolean;

                if (data.data is JsonElement element)
                {
                    if (checkField.fieldType == FieldType.Text)
                        newData.value_text = element.GetString();
                    else if (checkField.fieldType == FieldType.Date || checkField.fieldType == FieldType.DateTime)
                    {
                        if(element.ValueKind == JsonValueKind.String && DateTime.TryParse(element.GetString(), out var dt))
                        {
                            newData.value_date = dt;
                        }else newData.value_date = null;

                    }
                        
                    else if (checkField.fieldType == FieldType.Number)
                    {
                        if (element.ValueKind == JsonValueKind.Number)
                            newData.value_number = element.GetDecimal();
                        else if (decimal.TryParse(element.GetString(), out var num))
                            newData.value_number = num;
                    }
                        
                    else if (checkField.fieldType == FieldType.Boolean)
                    {
                        if(element.ValueKind == JsonValueKind.True || element.ValueKind == JsonValueKind.False)
                            newData.ValueBoolean = element.GetBoolean();
                        else if(bool.TryParse(element.GetString(), out var b)) 
                            newData.ValueBoolean = b;
                    }
                        
                }
                _context.data_values_eavs.Add(newData);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<data_values_eavDTO>.Successfully(data));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<data_values_eavDTO>.CreatedFail(ex.Message));
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

        public async Task<PayLoad<data_values_eavDTO>> Updata(int id, data_values_eavDTO data)
        {
            try
            {
                var user = _userToken.name();
                var checkId = _context.data_values_eavs.FirstOrDefault(x => x.id == id && x.field_id == data.field_id && x.row_id == data.row_id && !x.deleted);
                var checkField = _context.fielddefinitions.FirstOrDefault(x => x.id == data.field_id && !x.deleted);
                var checkUser = _context.users.FirstOrDefault(x => x.id == int.Parse(user) && !x.deleted);

                if (checkId == null || checkField == null || checkUser == null)
                    return await Task.FromResult(PayLoad<data_values_eavDTO>.CreatedFail(Status.DATANULL));

                //if (checkField.fieldType == FieldType.Text)
                //    checkId.value_text = data.value_text;
                //else if (checkField.fieldType == FieldType.Date || checkField.fieldType == FieldType.DateTime)
                //    checkId.value_date = data.value_date;
                //else if (checkField.fieldType == FieldType.Number)
                //    checkId.value_number = data.value_number;
                //else if (checkField.fieldType == FieldType.Boolean)
                //    checkId.ValueBoolean = data.ValueBoolean;

                if(data.data is JsonElement element)
                {
                    if (checkField.fieldType == FieldType.Text)
                        checkId.value_text = element.GetString();
                    else if (checkField.fieldType == FieldType.Date || checkField.fieldType == FieldType.DateTime)
                    {
                        if (element.ValueKind == JsonValueKind.String && DateTime.TryParse(element.GetString(), out var dt))
                            checkId.value_date = dt;
                         else checkId.value_date = null;
                        
                    }
                    else if (checkField.fieldType == FieldType.Number)
                    {
                        if(element.ValueKind == JsonValueKind.Number)
                            checkId.value_number = element.GetDecimal();
                        else if(decimal.TryParse(element.GetString(), out var num)) 
                            checkId.value_number = num;
                    }
                        
                    else if (checkField.fieldType == FieldType.Boolean)
                    {
                        if (element.ValueKind == JsonValueKind.True || element.ValueKind == JsonValueKind.False)
                            checkId.ValueBoolean = element.GetBoolean();
                        else if (bool.TryParse(element.GetString(), out var b))
                            checkId.ValueBoolean = b;
                    }
                }
                checkId.cretoredit = checkUser.username + " đã thay đổi bản ghi vào lúc " + DateTime.UtcNow;

                _context.data_values_eavs.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<data_values_eavDTO>.Successfully(data));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<data_values_eavDTO>.CreatedFail(ex.Message));
            }
        }
    }
}
