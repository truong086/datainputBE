using Microsoft.EntityFrameworkCore;

namespace DataEntrySystemDL.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base (options)
        {
            
        }

        #region DB Set
        public DbSet<data_rows_eav> data_rows_eavs { get; set; }
        public DbSet<data_rows_json> data_rows_jsons { get; set; }
        public DbSet<data_values_eav> data_values_eavs { get; set; }
        public DbSet<field_permissions> field_permissions { get; set; }
        public DbSet<FieldDefinition> fielddefinitions { get; set; }
        public DbSet<projects> projects { get; set; }
        public DbSet<roles> roles { get; set; }
        public DbSet<row_access_rules> row_access_rules { get; set; }
        public DbSet<tables> tables { get; set; }
        public DbSet<users> users { get; set; }
        public DbSet<user_roles> user_roles { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<data_rows_eav>(e =>
            {
                // Liên kết bảng
                e.HasOne(h => h.table) // Mỗi "data_rows_eav" có 1 "table"
                .WithMany(t => t.data_rows_eavs) // Một "table" có nhiều "data_rows_eav" (navigation property bên "table" => "ICollection<data_rows_eav>? data_rows_eavs")
                .HasForeignKey(f => f.table_id) // Khóa ngoại ở "data_rows_eav"
                .OnDelete(DeleteBehavior.Cascade); // Xóa Table thì xóa luôn "data_rows_eavs"

                e.HasOne(h => h.users) // Mỗi "data_rows_eav" có 1 "users"
                .WithMany(t => t.data_rows_eavs) // Một "users" có nhiều "data_rows_eav" (navigation property bên "table" => "ICollection<data_rows_eav>? data_rows_eavs")
                .HasForeignKey(f => f.created_by) // Khóa ngoại ở "data_rows_eav"
                .OnDelete(DeleteBehavior.Cascade); // Xóa Table thì xóa luôn "data_rows_eavs"

                // Đánh Index để truy vấn nhanh hơn 
                e.HasIndex(i => new { i.table_id, i.row_uuid }) // Tạo index ở 2 trường dữ liệu "table_id" và "row_uuid" để khi tìm kiếm theo 2 trường dữ liệu này cùng lúc sẽ nhanh hơn
                .HasDatabaseName("idx_table_row");

                e.HasIndex(i => new { i.created_at }) // Tạo index ở 1 trường dữ liệu "created_at" để khi tìm kiếm theo 1 trường dữ liệu này sẽ nhanh hơn
                .HasDatabaseName("idx_created_at");
            });


            modelBuilder.Entity<data_rows_json>(e =>
            {
                e.HasOne(o => o.table) // Mỗi "data_rows_json" có 1 "table"
                .WithMany(t => t.data_rows_jsons) // Một "table" có nhiều "data_rows_json" (navigation property bên "table" => "ICollection<data_rows_json>? data_rows_jsons")
                .HasForeignKey(f => f.table_id) // Khóa ngoại ở "data_rows_json"
                .OnDelete(DeleteBehavior.Cascade); // Xóa Table thì xóa luôn "data_rows_jsons"


                e.HasOne(o => o.users) // Mỗi "data_rows_json" có 1 "users"
                .WithMany(t => t.data_rows_jsons) // Một "users" có nhiều "data_rows_json" (navigation property bên "users" => "ICollection<data_rows_json>? data_rows_jsons")
                .HasForeignKey(f => f.created_by) // Khóa ngoại ở "data_rows_json"
                .OnDelete(DeleteBehavior.Cascade); // Xóa Table thì xóa luôn "data_rows_jsons"

                e.HasIndex(i => new { i.table_id, i.row_uuid }) // Tạo index ở 2 trường dữ liệu "table_id" và "row_uuid" để khi tìm kiếm theo 2 trường dữ liệu này cùng lúc sẽ nhanh hơn
                .HasDatabaseName("idx_table_row");

                e.HasIndex(i => new { i.created_at}) // Tạo index ở 1 trường dữ liệu "created_at" để khi tìm kiếm theo 1 trường dữ liệu này sẽ nhanh hơn
                .HasDatabaseName("idx_created_at");
            });

            modelBuilder.Entity<data_values_eav>(e =>
            {
                e.HasOne(o => o.data_rows_eavs) // Mỗi "data_values_eav" có 1 "data_rows_eavs"
                .WithMany(t => t.Data_Values_Eavs) // Một "data_rows_eavs" có nhiều "data_values_eav" (navigation property bên "data_rows_eavs" => "ICollection<data_values_eav>? Data_Values_Eavs")
                .HasForeignKey(f => f.row_id) // Khóa ngoại ở "data_values_eav"
                .OnDelete(DeleteBehavior.Cascade); // Xóa Table thì xóa luôn "Data_Values_Eavs"

                e.HasOne(o => o.field_definition) // Mỗi "data_values_eav" có 1 "field_definition"
                .WithMany(t => t.Data_Values_Eavs) // Một "FieldDefinition" có nhiều "data_values_eav" (navigation property bên "FieldDefinition" => "ICollection<data_values_eav>? Data_Values_Eavs")
                .HasForeignKey(f => f.field_id) // Khóa ngoại ở "data_values_eav"
                .OnDelete(DeleteBehavior.Cascade); // Xóa Table thì xóa luôn "Data_Values_Eavs"

                e.HasIndex(i => new { i.field_id, i.value_text }) // Tạo index ở 2 trường dữ liệu "field_id" và "value_text" để khi tìm kiếm theo 2 trường dữ liệu này cùng lúc sẽ nhanh hơn
                .HasDatabaseName("idx_field_value_text");

                e.HasIndex(i => new { i.field_id, i.value_number }) // Tạo index ở 2 trường dữ liệu "field_id" và "value_number" để khi tìm kiếm theo 2 trường dữ liệu này cùng lúc sẽ nhanh hơn
                .HasDatabaseName("idx_field_value_number");

                e.HasIndex(i => new { i.field_id, i.value_date }) // Tạo index ở 2 trường dữ liệu "field_id" và "value_date" để khi tìm kiếm theo 2 trường dữ liệu này cùng lúc sẽ nhanh hơn
                .HasDatabaseName("idx_field_value_date");

                // Tạo "Unique" cho 2 trường dữ liệu "row_id" và "field_id"
                e.HasIndex(i => new { i.row_id, i.field_id })
                .IsUnique()
                .HasDatabaseName("unique_row_field");

            });

            modelBuilder.Entity<field_permissions>(e =>
            {
                e.HasOne(o => o.fieldDefinition) // Mỗi "field_permissions" có 1 "fieldDefinition"
                .WithMany(t => t.Field_Permissions) // Một "fieldDefinition" có nhiều "field_permissions" (navigation property bên "fieldDefinition" => "ICollection<field_permissions>? Field_Permissions { get; set; }")
                .HasForeignKey(f => f.field_id) // Khóa ngoại ở "field_permissions"
                .OnDelete(DeleteBehavior.Cascade); // Xóa Table thì xóa luôn "Field_Permissions"

                e.HasOne(o => o.role) // Mỗi "field_permissions" có 1 "role"
                .WithMany(t => t.Field_Permissions) // Một "role" có nhiều "field_permissions" (navigation property bên "role" => "ICollection<field_permissions>? Field_Permissions")
                .HasForeignKey(f => f.role_id) // Khóa ngoại ở "field_permissions"
                .OnDelete(DeleteBehavior.Cascade); // Xóa Table thì xóa luôn "Field_Permissions"

                e.HasIndex(i => new { i.field_id, i.role_id })
                .IsUnique()
                .HasDatabaseName("unique_field_role");
            });

            modelBuilder.Entity<FieldDefinition>(e =>
            {
                e.HasOne(o => o.table) // Mỗi "FieldDefinition" có 1 "Table"
                .WithMany(t => t.FieldDefinitions) // Một "Table" có nhiều "FieldDefinitions" (navigation property bên "table" => "ICollection<FieldDefinition>? FieldDefinitions")
                .HasForeignKey(f => f.table_id) // Khóa ngoại ở "FieldDefinition"
                .OnDelete(DeleteBehavior.Cascade);  // Xóa Table thì xóa luôn "FieldDefinitions"

                e.HasIndex(i => new { i.table_id, i.field_key })
                .IsUnique()
                .HasDatabaseName("unique_field_key");

                // Enum lưu thành string (text, number, date, ...)
                e.Property(p => p.fieldType)
                .HasColumnName("fieldtype")
                .HasConversion<string>()  // Lưu enum thành VARCHAR
                .IsRequired();
            });

            modelBuilder.Entity<projects>(e =>
            {
                e.HasOne(o => o.users)
                .WithMany(t => t.Projects)
                .HasForeignKey(f => f.owner_id)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<row_access_rules>(e =>
            {
                e.HasOne(o => o.table)
                .WithMany(t => t.row_access_ruless)
                .HasForeignKey(f => f.table_id)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(o => o.users)
                .WithMany(t => t.row_access_ruless)
                .HasForeignKey(f => f.user_id)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(o => o.role)
                .WithMany(t => t.row_access_ruless)
                .HasForeignKey(f => f.role_id)
                .OnDelete(DeleteBehavior.Cascade);

                // Enum lưu thành string (text, number, date, ...)
                e.Property(p => p.access_type)
                .HasColumnName("access_type")
                .HasConversion<string>() // Lưu enum thành VARCHAR
                .IsRequired();

                // CHECK constraint: user_id IS NOT NULL OR role_id IS NOT NULL
                e.HasCheckConstraint("CK_RowAccessRules_UserOrRole", "user_id IS NOT NULL OR role_id IS NOT NULL");
            });

            modelBuilder.Entity<tables>(e =>
            {
                e.HasOne(o => o.project)
                .WithMany(t => t.Tables)
                .HasForeignKey(f => f.project_id)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.users)
                .WithMany(t => t.tabless)
                .HasForeignKey(f => f.user_id)
                .OnDelete(DeleteBehavior.Cascade);



                e.HasIndex(i => new { i.project_id, i.name })
                .IsUnique()
                .HasDatabaseName("unique_table_name");
            });

            modelBuilder.Entity<user_roles>(e =>
            {
                e.HasKey(k => new { k.user_id, k.role_id }); // Đặt khóa chính cho 2 trường dữ liệu Primary Key (user_id, role_id)

                e.HasOne(o => o.user)
                .WithMany(t => t.User_Roles)
                .HasForeignKey(f => f.user_id)
                .OnDelete(DeleteBehavior.Cascade);


                e.HasOne(o => o.role)
                .WithMany(t => t.User_Roles)
                .HasForeignKey(f => f.role_id)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
