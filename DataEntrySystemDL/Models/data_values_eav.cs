using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class data_values_eav : BaseEntity
    {
        public int? row_id { get; set; }
        public data_rows_eav? data_rows_eavs { get; set; }
        public int? field_id { get; set; }
        public FieldDefinition? field_definition { get; set; }
        public string? field_key { get; set; }
        public string? value_text { get; set; }
        public decimal? value_number { get; set; }
        public DateTime? value_date { get; set; }
        public DateTime? value_datetime { get; set; }
        public bool? ValueBoolean { get; set; }

    }
}
