using DataEntrySystemDL.Models;

namespace DataEntrySystemDL.ViewModel
{
    public class FieldDefinitionDTO
    {
        public int? table_id { get; set; }
        public string? field_key { get; set; }
        public string? field_name { get; set; }
        public string? fieldType { get; set; }
        public string? field_options { get; set; }
        public int sort_order { get; set; }
        public List<listRole>? listRoles { get; set; }
    }

    public class listRole
    {
        public int? role_id { get; set; }
        public bool can_read { get; set; }
        public bool can_write { get; set; }
    }
}
