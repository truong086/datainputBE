using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class users : BaseEntity
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? display_name { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<user_roles>? User_Roles { get; set; }
        public virtual ICollection<projects>? Projects { get; set; }
        public virtual ICollection<row_access_rules>? row_access_ruless { get; set; }
        public virtual ICollection<data_rows_json>? data_rows_jsons { get; set; }
        public virtual ICollection<data_rows_eav>? data_rows_eavs { get; set; }
        public virtual ICollection<tables>? tabless { get; set; }
    }
}
