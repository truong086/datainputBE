using System.ComponentModel.DataAnnotations;

namespace DataEntrySystemDL.Common
{
    public class BaseEntity
    {
        protected BaseEntity() { }
        [Key]
        public int id { get; set; }
        public bool deleted { get; set; }
        public string? cretoredit { get; set; }
        public DateTimeOffset? created_at { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? updated_at { get; set; }
    }
}
