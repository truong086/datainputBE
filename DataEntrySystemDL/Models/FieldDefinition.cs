using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class FieldDefinition : BaseEntity
    {
        public int? table_id { get; set; }
        public tables? table {  get; set; }
        public string? field_key { get; set; }
        public string? field_name { get; set; }
        public FieldType fieldType { get; set; }
        public string? field_options { get; set; }
        public bool is_required { get; set; }
        public int sort_order { get; set; }
        public virtual ICollection<field_permissions>? Field_Permissions { get; set; }
        public virtual ICollection<data_values_eav>? Data_Values_Eavs { get; set; }
    }

    public enum FieldType
    {
        Text,
        Number,
        Date,
        DateTime,
        Boolean,
        Select,
        MultiSelect
    }

}
