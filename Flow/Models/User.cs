using SQLite;


namespace Flow.Models
{
    [Table("user_info")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("first_name")]
        public string First_name { get; set; }
        [Column("last_name")]
        public string Last_name { get; set; }
        [Column("user_password")]
        public string Password { get; set; }
    }
}
 