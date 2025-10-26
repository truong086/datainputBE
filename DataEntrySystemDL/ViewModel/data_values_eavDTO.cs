namespace DataEntrySystemDL.ViewModel
{
    public class data_values_eavDTO
    {
        public int? row_id { get; set; }
        public int? field_id { get; set; }
        public string? field_key { get; set; }
        public object? data { get; set; }
        public string? value_text { get; set; }
        public decimal? value_number { get; set; }
        public DateTime? value_date { get; set; }
        public DateTime? value_datetime { get; set; }
        public bool? ValueBoolean { get; set; }
    }
}
