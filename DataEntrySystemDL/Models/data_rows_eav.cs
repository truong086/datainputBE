using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class data_rows_eav : BaseEntity
    {
        public int? table_id { get; set; }
        public tables? table { get; set; }
        public string? row_uuid { get; set; } = Guid.NewGuid().ToString();
        public int? created_by { get; set; }
        public users? users { get; set; }
        public virtual ICollection<data_values_eav>? Data_Values_Eavs { get; set; }
    }
}
