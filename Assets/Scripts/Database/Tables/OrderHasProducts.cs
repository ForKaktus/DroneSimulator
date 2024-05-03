using SQLite4Unity3d;

namespace nsDB
{
    [Table("orders_has_products")]
    public class OrderHasProducts
    {
        [PrimaryKey, AutoIncrement, Column("orhp_id")]
        public int orhp_id       { get; set; }
        public int orhp_order_id     { get; set; }
        public int orhp_product_id { get; set; }
    }
}