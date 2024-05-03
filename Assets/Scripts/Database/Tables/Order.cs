using SQLite4Unity3d;

namespace nsDB
{
    [Table("orders")]
    public class Order
    {
        [PrimaryKey, AutoIncrement, Column("order_id")]
        public int    order_id       { get; set; }
        public string order_description     { get; set; }
        public int order_user_id { get; set; }
    }
}