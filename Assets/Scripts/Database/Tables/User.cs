using SQLite4Unity3d;

namespace nsDB
{
    [Table("users")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("user_id")]
        public int    user_id       { get; set; }
        public string user_login     { get; set; }
        public string user_password { get; set; }
        public int user_role_id     { get; set; }
    }
}