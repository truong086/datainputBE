using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class projects : BaseEntity
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public int? owner_id { get; set; }
        public users? users { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<tables>? Tables { get; set; }
    }
}
