using SQLite4Unity3d;

namespace nsDB
{
    [Table("user_roles")]
    public class UserRole
    {
        [PrimaryKey, AutoIncrement, Column("role_id")]
        public int    role_id          { get; set; }
        public string role_name        { get; set; }
    }
}