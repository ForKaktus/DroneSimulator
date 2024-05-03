using SQLite4Unity3d;

namespace nsDB
{
    [Table("products")]
    public class Product
    {
        [PrimaryKey, AutoIncrement, Column("product_id")]
        public int  product_id        { get; set; }
        public string  product_name    { get; set; }
        public string  product_file_path     { get; set; }
    }
}