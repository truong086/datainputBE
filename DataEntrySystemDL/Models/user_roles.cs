using DataEntrySystemDL.Common;

namespace DataEntrySystemDL.Models
{
    public class user_roles : BaseEntity
    {
        public int? user_id { get; set; }
        public users? user { get; set; }
        public int? role_id { get; set; }
        public roles? role { get; set; }
    }
}
