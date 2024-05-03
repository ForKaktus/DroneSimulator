using SQLite4Unity3d;

namespace nsDB
{
    [Table("errors")]
    public class Error
    {
        [PrimaryKey, AutoIncrement, Column("err_id")]
        public int err_id     { get; set; }
        public int err_exam_id  { get; set; }
        public string err_description { get; set; }
    }
}