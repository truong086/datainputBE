using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class tables : BaseEntity
    {
        public int? project_id { get; set; }
        public projects? project { get; set; }
        public int? user_id { get; set; }
        public users? users { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<FieldDefinition>? FieldDefinitions { get; set; }
        public virtual ICollection<row_access_rules>? row_access_ruless { get; set; }
        public virtual ICollection<data_rows_eav>? data_rows_eavs { get; set; }
        public virtual ICollection<data_rows_json>? data_rows_jsons { get; set; }
        
    }
}
