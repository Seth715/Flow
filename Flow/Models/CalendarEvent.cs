using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Maui.Calendar.Models;
using SQLite;

namespace Flow.Models
{
    public class CalendarEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AllDay { get; set; }
        public string Color { get; set; }

        [Column("todo_item_id")]
        public int? ToDoItemId { get; set; }

        [Column("goal_item_id")]
        public int? GoalItemId { get; set; }
    }
}
