using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class roles : BaseEntity
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public virtual ICollection<user_roles>? User_Roles { get; set; }
        public virtual ICollection<field_permissions>? Field_Permissions { get; set; }
        public virtual ICollection<row_access_rules>? row_access_ruless { get; set; }
    }
}
