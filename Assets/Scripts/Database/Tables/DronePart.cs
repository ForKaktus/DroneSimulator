using SQLite4Unity3d;

namespace nsDB
{
    [Table("drone_parts")]
    public class DronePart
    {
        [PrimaryKey, AutoIncrement, Column("drpa_id")]
        public int    drpa_id       { get; set; }
        public int    drpa_dron_id       { get; set; }
        public string drpa_model_path     { get; set; }
        public int drpa_type   { get; set; }
    }
}