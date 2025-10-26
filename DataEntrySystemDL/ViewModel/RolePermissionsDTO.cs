namespace DataEntrySystemDL.ViewModel
{
    public class RolePermissionsDTO
    {
        public int? id_field { get; set; }
        public int? id_role { get; set; }
        public bool? can_read { get; set; }
        public bool? can_write { get; set;}
    }
}
