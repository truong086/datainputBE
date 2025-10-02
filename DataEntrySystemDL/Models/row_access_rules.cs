using DataEntrySystemDL.Common;
using System.Text.Json.Serialization;

namespace DataEntrySystemDL.Models
{
    public class row_access_rules : BaseEntity
    {
        public int? table_id { get; set; }
        public tables? table {  get; set; } 
        public int? user_id { get; set; }
        public users? users { get; set; }
        public int? role_id { get; set; }
        public roles? role { get; set; }
        public string? filter_conditions { get; set; }
        public access_types? access_type { get; set; }


    }

    //[JsonConverter(typeof(JsonStringEnumConverter))] // Cho cái này vào để lúc lấy ra dữ liệu thì enum sẽ lấy ra text bên trong (read, write, delete) nếu không thì enum chỉ lấy ra số index
    public enum access_types
    {
        read,
        write,
        delete
    }
}
