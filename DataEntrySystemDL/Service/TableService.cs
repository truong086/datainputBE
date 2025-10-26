using AutoMapper;
using DataEntrySystemDL.Common;
using DataEntrySystemDL.Models;
using DataEntrySystemDL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Drawing.Printing;
using System.Text.Json;

namespace DataEntrySystemDL.Service
{
    public class TableService : ITableService
    {
        private readonly DBContext _context;
        private IMapper _mapper;
        private readonly IUserTokenService _userToken;
        private readonly IFieldDefinitionService _fieldDefinitionService;
        private readonly IData_rows_eavService _Rows_EavService;
        private readonly IData_values_eavService _Values_EavService;
        public TableService(DBContext context, IMapper mapper, 
            IUserTokenService userToken, 
            IFieldDefinitionService fieldDefinitionService, 
            IData_rows_eavService Rows_EavService, IData_values_eavService Values_EavService)
        {
            _context = context;
            _mapper = mapper;
            _userToken = userToken;
            _fieldDefinitionService = fieldDefinitionService;
            _Rows_EavService = Rows_EavService;
            _Values_EavService = Values_EavService;
        }
        public async Task<PayLoad<tableDTO>> Add(tableDTO data)
        {
            try
            {
                var user = _userToken.name();
                var checkName = _context.tables.FirstOrDefault(x => x.name == data.name && x.project_id == data.project_id && !x.deleted);
                if (checkName != null) return await Task.FromResult(PayLoad<tableDTO>.CreatedFail(Status.DATATONTAI));

                var checkUser = _context.users.FirstOrDefault(x => x.id == Convert.ToInt32(user) && !x.deleted);
                if(checkUser == null) return await Task.FromResult(PayLoad<tableDTO>.CreatedFail(Status.DATANULL));

                var checkproject = _context.projects.FirstOrDefault(x => x.id == data.project_id && !x.deleted);
                if (checkproject == null) return await Task.FromResult(PayLoad<tableDTO>.CreatedFail(Status.DATANULL));

                var mapData = _mapper.Map<tables>(data);
                mapData.project_id = checkproject.id;
                mapData.project = checkproject;
                mapData.is_active = true;
                mapData.users = checkUser;
                mapData.user_id = checkUser.id;

                _context.tables.Add(mapData);
                _context.SaveChanges();

                var dataNew = _context.tables.OrderByDescending(x => x.id).FirstOrDefault();
                var checkRole = _context.user_roles.FirstOrDefault(x => x.user_id == checkUser.id && !x.deleted);
                var checkRoleData = _context.roles.FirstOrDefault(x => x.id == checkRole.role_id && !x.deleted);
                var roleNewTable = new row_access_rules
                {
                    role_id = checkRoleData.id,
                    user_id = checkUser.id,
                    table_id = dataNew.id,
                    table = dataNew,
                    users = checkUser,
                    role = checkRoleData,
                    access_type = Enum.Parse<access_types>("write")
                };

                _context.row_access_rules.Add(roleNewTable);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<tableDTO>.Successfully(data));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<tableDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<string>> Delete(int id)
        {
            try
            {
                var checkId = _context.tables.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null) return await Task.FromResult(PayLoad<string>.CreatedFail(Status.DATANULL));

                checkId.deleted = true;

                _context.tables.Update(checkId);
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
                var user = _userToken.name();
                var data = _context.tables.Where(x => !x.deleted && x.row_access_ruless.Any(r => r.user_id == Convert.ToInt32(user)))
                    .AsNoTracking().Select(x => new
                {
                    x.id,
                    project_id = x.project.id,
                    project_name = x.project.name,
                    x.name,
                    x.description,
                    field = x.FieldDefinitions.Select(x1 => new
                    {
                        x1.id,
                        x1.field_key,
                        x1.field_name,
                        type = x1.fieldType.ToString(),
                        x1.field_options,
                        role = x1.Field_Permissions.Select(x2 => new
                        {
                            role_id = x2.role.id,
                            role_name = x2.role.name,
                            x2.can_write,
                            x2.can_read
                        }).ToList(),
                        rows = x.data_rows_eavs.Where(x6 => x6.table_id == x.id).Select(x5 => new
                        {
                            x5.id,
                            x5.table_id,
                            x5.row_uuid,
                            field_row = x5.Data_Values_Eavs.Where(x7 => x7.row_id == x5.id && x7.field_id == x1.id).Select(x8 => new
                            {
                                x8.row_id,
                                x8.field_id,
                                x8.field_key,
                                x8.value_text,
                                x8.value_number,
                                x8.value_date,
                                x8.value_datetime,
                                x8.ValueBoolean
                            }).ToList()
                        }).ToList()
                    }).ToList(),
                    row = x.data_rows_eavs.Select(x3 => new
                    {
                        x3.table_id,
                        x3.row_uuid,
                        user = x3.users.username,
                        data_value = x3.Data_Values_Eavs.Select(x4 => new
                        {
                            x4.row_id,
                            x4.field_id,
                            x4.field_key,
                            x4.value_text,
                            x4.value_number,
                            x4.value_date,
                            x4.value_datetime,
                            x4.ValueBoolean
                        }).ToList()
                    }).ToList()
                }).ToList();

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
            try {
                var data = _context.tables.Where(x => !x.deleted && x.id == id)
                    .AsNoTracking().Select(x => new
                    {
                        x.id,
                        project_id = x.project.id,
                        project_name = x.project.name,
                        x.name,
                        x.description,
                        field = x.FieldDefinitions.Select(x1 => new
                        {
                            x1.id,
                            x1.field_key,
                            x1.field_name,
                            type = x1.fieldType.ToString(),
                            x1.field_options,
                            role = x1.Field_Permissions.Select(x2 => new
                            {
                                role_id = x2.role.id,
                                role_name = x2.role.name,
                                x2.can_write,
                                x2.can_read
                            }).ToList(),
                            rows = x.data_rows_eavs.Where(x6 => x6.table_id == x.id).Select(x5 => new
                            {
                                x5.id,
                                x5.table_id,
                                x5.row_uuid,
                                field_row = x5.Data_Values_Eavs.Where(x7 => x7.row_id == x5.id && x7.field_id == x1.id).Select(x8 => new
                                {
                                    x8.row_id,
                                    x8.field_id,
                                    x8.field_key,
                                    x8.value_text,
                                    x8.value_number,
                                    x8.value_date,
                                    x8.value_datetime,
                                    x8.ValueBoolean
                                }).ToList()
                            }).ToList()
                        }).ToList(),
                        row = x.data_rows_eavs.Select(x3 => new
                        {
                            x3.table_id,
                            x3.row_uuid,
                            user = x3.users.username,
                            data_value = x3.Data_Values_Eavs.Select(x4 => new
                            {
                                x4.row_id,
                                x4.field_id,
                                x4.field_key,
                                x4.value_text,
                                x4.value_number,
                                x4.value_date,
                                x4.value_datetime,
                                x4.ValueBoolean
                            }).ToList()
                        }).ToList()
                    }).FirstOrDefault();

                if (data == null) return await Task.FromResult(PayLoad<object>.CreatedFail(Status.DATANULL));

                return await Task.FromResult(PayLoad<object>.Successfully(data));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<tableDTO>> Update(int id, tableDTO data)
        {
            try
            {
                var user = _userToken.name();
                var checkId = _context.tables.FirstOrDefault(x => x.id == id && !x.deleted);
                if (checkId == null) return await Task.FromResult(PayLoad<tableDTO>.CreatedFail(Status.DATANULL));

                var checkUser = _context.users.FirstOrDefault(x => x.id == int.Parse(user) && !x.deleted);
                if(checkUser == null) return await Task.FromResult(PayLoad<tableDTO>.CreatedFail(Status.DATANULL));

                if(data.project_id != null && data.project_id != 0)
                {
                    var checkproject = _context.projects.FirstOrDefault(x => x.id == data.project_id && !x.deleted);
                    if(checkproject == null) return await Task.FromResult(PayLoad<tableDTO>.CreatedFail(Status.DATANULL));

                    checkId.project_id = checkproject.id;
                    checkId.project = checkproject;

                }

                checkId.name = data.name != null && data.name != "" && !string.IsNullOrEmpty(data.name) ? data.name : checkId.name;
                checkId.description = data.description != null && data.description != "" && !string.IsNullOrEmpty(data.description) ? data.description : checkId.description;

                _context.tables.Update(checkId);
                _context.SaveChanges();

                return await Task.FromResult(PayLoad<tableDTO>.Successfully(data));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<tableDTO>.CreatedFail(ex.Message));
            }
        }

        public async Task<PayLoad<object>> FindOneTable(int id, int page = 1, int pageSize = 20)
        {
            try
            {
                var user = _userToken.name();
                var data = _context.tables.Where(x => !x.deleted 
                && x.row_access_ruless.Any(r => r.user_id == Convert.ToInt32(user)) // "x.row_access_ruless.Any(r => r.user_id == Convert.ToInt32(user)" lọc giá trị trong "ICollection<row_access_rules>? row_access_ruless { get; set; }" này
                && x.project_id == id)
                    .AsNoTracking().Select(x => new
                    {
                        x.id,
                        project_id = x.project.id,
                        project_name = x.project.name,
                        x.name,
                        x.description,
                        field = x.FieldDefinitions.Select(x1 => new
                        {
                            x1.id,
                            x1.field_key,
                            x1.field_name,
                            type = x1.fieldType.ToString(),
                            x1.field_options,
                            role = x1.Field_Permissions.Select(x2 => new
                            {
                                role_id = x2.role.id,
                                role_name = x2.role.name,
                                x2.can_write,
                                x2.can_read
                            }).ToList(),
                            rows = x.data_rows_eavs.Where(x6 => x6.table_id == x.id).Select(x5 => new
                            {
                                x5.id,
                                x5.table_id,
                                x5.row_uuid,
                                field_row = x5.Data_Values_Eavs.Where(x7 => x7.row_id == x5.id && x7.field_id == x1.id).Select(x8 => new
                                {
                                    x8.id,
                                    x8.row_id,
                                    x8.field_id,
                                    x8.field_key,
                                    x8.value_text,
                                    x8.value_number,
                                    x8.value_date,
                                    x8.value_datetime,
                                    x8.ValueBoolean,
                                    field_name = x8.field_definition.field_name
                                }).FirstOrDefault()
                            }).ToList()
                        }).ToList(),
                        row = x.data_rows_eavs.Select(x3 => new
                        {
                            x3.table_id,
                            x3.row_uuid,
                            user = x3.users.username,
                            data_value = x3.Data_Values_Eavs.Select(x4 => new
                            {
                                x4.row_id,
                                x4.field_id,
                                x4.field_key,
                                x4.value_text,
                                x4.value_number,
                                x4.value_date,
                                x4.value_datetime,
                                x4.ValueBoolean,
                                field_name = x4.field_definition.field_name
                            }).ToList()
                        }).ToList()
                    }).ToList();

                var pageList = new PageList<object>(data, page - 1, pageSize);

                return await Task.FromResult(PayLoad<object>.Successfully(new
                {
                    data = pageList,
                    page,
                    pageList.pageSize,
                    pageList.totalCounts,
                    pageList.totalPages
                }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }

        public byte[] ExcelExPost(int id)
        {
            var user = _userToken.name();
            var data = _context.tables.Where(x => !x.deleted
            && x.row_access_ruless.Any(r => r.user_id == Convert.ToInt32(user)) // "x.row_access_ruless.Any(r => r.user_id == Convert.ToInt32(user)" lọc giá trị trong "ICollection<row_access_rules>? row_access_ruless { get; set; }" này
            && x.id == id)
                .AsNoTracking().Select(x => new
                {
                    x.id,
                    project_id = x.project.id,
                    project_name = x.project.name,
                    x.name,
                    x.description,
                    field = x.FieldDefinitions.Select(x1 => new
                    {
                        x1.id,
                        x1.field_key,
                        x1.field_name,
                        type = x1.fieldType.ToString(),
                        x1.field_options,
                        role = x1.Field_Permissions.Select(x2 => new
                        {
                            role_id = x2.role.id,
                            role_name = x2.role.name,
                            x2.can_write,
                            x2.can_read
                        }).ToList(),
                        rows = x.data_rows_eavs.Where(x6 => x6.table_id == x.id).Select(x5 => new
                        {
                            x5.id,
                            x5.table_id,
                            x5.row_uuid,
                            field_row = x5.Data_Values_Eavs.Where(x7 => x7.row_id == x5.id && x7.field_id == x1.id).Select(x8 => new
                            {
                                x8.row_id,
                                x8.field_id,
                                x8.field_key,
                                x8.value_text,
                                x8.value_number,
                                x8.value_date,
                                x8.value_datetime,
                                x8.ValueBoolean
                            }).FirstOrDefault()
                        }).ToList()
                    }).ToList(),
                    row = x.data_rows_eavs.Select(x3 => new
                    {
                        x3.table_id,
                        x3.row_uuid,
                        user = x3.users.username,
                        data_value = x3.Data_Values_Eavs.Select(x4 => new
                        {
                            x4.row_id,
                            x4.field_id,
                            x4.field_key,
                            x4.value_text,
                            x4.value_number,
                            x4.value_date,
                            x4.value_datetime,
                            x4.ValueBoolean
                        }).ToList()
                    }).ToList()
                }).ToList();

            using (var packer = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = packer.Workbook.Worksheets.Add("table");
                int col = 1;

                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        foreach (var field in item.field)
                        {
                            int row = 2;
                            worksheet.Cells[1, col].Value = field.field_name;

                            // AutoFit cho tất cả cột có dữ liệu
                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns(10, 50);
                            foreach (var rowItem in field.rows)
                            {
                                if (rowItem.field_row != null)
                                {
                                    if (rowItem.field_row.ValueBoolean != null)
                                        worksheet.Cells[row++, col].Value = rowItem.field_row.ValueBoolean;
                                    else if (!string.IsNullOrEmpty(rowItem.field_row.value_text))
                                        worksheet.Cells[row++, col].Value = rowItem.field_row.value_text;
                                    else if (rowItem.field_row.value_date != null)
                                    {
                                        worksheet.Cells[row++, col].Value = rowItem.field_row.value_date;
                                        worksheet.Cells[row - 1, col].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss";
                                    }
                                        
                                    else if (rowItem.field_row.value_datetime != null)
                                    {
                                        worksheet.Cells[row++, col].Value = rowItem.field_row.value_datetime;
                                        worksheet.Cells[row - 1, col].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss";
                                    }
                                        
                                    else if (rowItem.field_row.value_number != null)
                                        worksheet.Cells[row++, col].Value = rowItem.field_row.value_number;
                                }
                                else worksheet.Cells[row++, col].Value = "";

                            }
                            col++;
                        }
                    }
                }

                // Xuất file Excel
                var stream = new MemoryStream();
                packer.SaveAs(stream);
                stream.Position = 0;

                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = $"Export_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                // return File để client tải file
                return packer.GetAsByteArray();
            }
        }

        public async Task<PayLoad<ImportTable>> importData(ImportTable data)
        {
            try
            {
                var user = _userToken.name();
                var checkData = _context.projects.FirstOrDefault(x => x.id == data.id_project && !x.deleted);
                var checkUser = _context.users.FirstOrDefault(x => x.id == int.Parse(user) && !x.deleted);

                if (checkData == null || checkUser == null || data.file == null) return await Task.FromResult(PayLoad<ImportTable>.CreatedFail(Status.DATANULL));

                var dataTableDTO = new tableDTO
                {
                    project_id = checkData.id,
                    name = data.file.FileName,
                    description = data.file.Name
                };
                Add(dataTableDTO);
                var dataNewTable = _context.tables.OrderByDescending(x => x.id).FirstOrDefault();
                var checkRole = _context.user_roles.FirstOrDefault(x => x.user_id == checkUser.id && !x.deleted);

                var rowAccess = new row_access_rulesDTO
                {
                    role_id = checkRole.role_id,
                    type = "write",
                    table_id = dataNewTable.id,
                    user_id = checkUser.id
                };
                _fieldDefinitionService.AddUserRoleTable(rowAccess);
                var listRow = new List<int>();

                int insertCount = 0;
                using(var steam = new MemoryStream())
                {
                    await data.file.CopyToAsync(steam);
                    using(var packer = new ExcelPackage(steam))
                    {
                        var worksheet = packer.Workbook.Worksheets[0]; // Lấy ra Sheet đầu tiên
                        int rowCount = worksheet.Dimension.Rows; // Lấy ra tổng số row (dòng)
                        int colCount = worksheet.Dimension.Columns; // Lấy ra tổng số colum (cột)

                        var checkRow = 0;
                        for(int col = 1; col <= colCount; col++)
                        {
                            string header = worksheet.Cells[1, col].Value?.ToString();

                            if(header != null && header != "")
                            {

                                var newFieldDTO = new FieldDefinitionDTO
                                {
                                    fieldType = "Text",
                                    field_key = "",
                                    field_name = header,
                                    field_options = "",
                                    listRoles = new List<listRole>
                                    {
                                        new listRole
                                        {
                                            can_read = true,
                                            can_write = true,
                                            role_id = checkRole.role_id
                                        }
                                    },
                                    sort_order = 1,
                                    table_id = dataNewTable.id
                                };
                                _fieldDefinitionService.Add(newFieldDTO);
                                var dataNewField = _context.fielddefinitions.OrderByDescending(x => x.id).FirstOrDefault();
                                for (int row = 2; row <= rowCount; row++)
                                {
                                    var cell = worksheet.Cells[row, col];
                                    //var cellValue = worksheet.Cells[row, col].Value?.ToString();
                                    var cellValue = worksheet.Cells[row, col].Value;
                                    var cellCheck = cell.Value;
                                    JsonElement? jsonelement = null;
                                    if (cell.Style.Numberformat.Format.Contains("yy") || cell.Style.Numberformat.Format.Contains("dd"))
                                    {
                                        if (cellCheck is double d)
                                        {
                                            cellValue = FromExcelSerialDate(d);
                                            jsonelement = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(cellValue));
                                        }
                                        else if (DateTime.TryParse(cellValue?.ToString(), out var dt))
                                        {
                                            cellValue = dt;
                                            jsonelement = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(cellValue));
                                        }
                                    }
                                    else
                                    {
                                        jsonelement = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(cellValue?.ToString()));
                                    }
                                    if (checkRow == 0)
                                    {
                                        _Rows_EavService.Add(new data_rows_eavDTO
                                        {
                                            table_id = dataNewTable.id
                                        });

                                        var newRow = _context.data_rows_eavs.OrderByDescending(x => x.id).FirstOrDefault();
                                        listRow.Add(newRow.id);

                                        if (!string.IsNullOrEmpty(cellValue?.ToString()))
                                        {
                                            _Values_EavService.Add(new data_values_eavDTO
                                            {
                                                field_id = dataNewField.id,
                                                field_key = dataNewField.field_key,
                                                row_id = newRow.id,
                                                ValueBoolean = false,
                                                value_date = null,
                                                value_datetime = null,
                                                value_number = null,
                                                value_text = cellValue?.ToString(),
                                                data = jsonelement
                                            });
                                        }
                                    }else if(checkRow == 1)
                                    {
                                        var sortInt = listRow.OrderBy(x => x).ToList(); // Sắp xếp tăng dần
                                        // Hoặc có thể làm theo cách này cũng có thể sắp xếp tăng dần
                                        //listRow.Sort();
                                        var indexArr = sortInt[row - 2];

                                        if (!string.IsNullOrEmpty(cellValue?.ToString()))
                                        {
                                            _Values_EavService.Add(new data_values_eavDTO
                                            {
                                                field_id = dataNewField.id,
                                                field_key = dataNewField.field_key,
                                                row_id = indexArr,
                                                ValueBoolean = false,
                                                value_date = null,
                                                value_datetime = null,
                                                value_number = null,
                                                value_text = cellValue?.ToString(),
                                                data = jsonelement
                                            });
                                        }
                                    }
                                    
                                }

                                if (listRow.Count > 0 && checkRow == 0)
                                    checkRow = 1;
                            }
                            
                        }
                    }
                }

                return await Task.FromResult(PayLoad<ImportTable>.Successfully(data));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<ImportTable>.CreatedFail(ex.Message));
            }
        }

        public static DateTime FromExcelSerialDate(double serialDate)
        {
            // Excel bắt đầu từ ngày 1900-01-01
            DateTime startDate = new DateTime(1899, 12, 30); // Excel đếm sai 2 ngày, phải trừ bù
            return startDate.AddDays(serialDate);
        }

        public async Task<PayLoad<object>> FindAllFieldBtTable(int id)
        {
            try
            {
                var data = _context.fielddefinitions.Where(x => x.table_id == id && !x.deleted).Select(x => new
                {
                    x.id,
                    x.field_name
                }).ToList();

                return await Task.FromResult(PayLoad<object>.Successfully(new
                {
                    data = data
                }));
            }catch(Exception ex)
            {
                return await Task.FromResult(PayLoad<object>.CreatedFail(ex.Message));
            }
        }
    }
}
