using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class data_rows_json : BaseEntity
    {
        public int? table_id { get; set; }
        public tables? table { get; set; }
        public string? row_uuid { get; set; } = Guid.NewGuid().ToString();
        public string? dynamic_data { get; set; }
        public int? created_by { get; set; }
        public users? users { get; set; }
    }
}
