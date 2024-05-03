using SQLite4Unity3d;

namespace nsDB
{
    [Table("drones")]
    public class Drone
    {
        [PrimaryKey, AutoIncrement, Column("dron_id")]
        public int    dron_id          { get; set; }
        public string dron_name      { get; set; }
        public float dron_speed       { get; set; }
    }
}