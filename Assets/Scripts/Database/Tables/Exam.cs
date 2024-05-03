using SQLite4Unity3d;

namespace nsDB
{
    [Table("exams")]
    public class Exam
    {
        [PrimaryKey, AutoIncrement, Column("exam_id")]
        public int exam_id            { get; set; }
        public int exam_user_id          { get; set; }
        public float exam_time_build   { get; set; }
        public float exam_time_delivery { get; set; }
        public int exam_accuracy { get; set; }
        public int exam_broke_product { get; set; }
    }
}