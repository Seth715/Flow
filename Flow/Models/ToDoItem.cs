using SQLite;

namespace Flow.Models
{
    [Table("todo_items")]
    public class ToDoItem
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("task")]
        public string Task { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }  // Foreign key linking to User.Id

        [Column("end_time")]
        public DateTime EndTime { get; set; }
    }
}
