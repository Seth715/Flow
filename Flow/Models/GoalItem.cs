using SQLite;

namespace Flow.Models
{
    [Table("goal_items")]
    public class GoalItem
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("task")]
        public string Goal { get; set; }
        [Column("is_completed")]
        public bool IsCompleted { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }  // Foreign key linking to User.Id
    }
}