using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class field_permissions : BaseEntity
    {
        public int? field_id { get; set; }
        public FieldDefinition? fieldDefinition { get; set; }
        public int? role_id { get; set; }
        public roles? role { get; set; }
        public bool can_read { get; set; }
        public bool can_write { get; set;}
    }
}
