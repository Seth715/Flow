using Flow.Models;
using SQLite;

namespace Flow.Local_Database
{
    public static class LocalDBService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<User>();

            await db.CreateTableAsync<ToDoItem>();

            await db.CreateTableAsync<GoalItem>();
        }

        public static async Task AddUser(string first_name, string last_name, string username, string password)
        {
            await Init();

            var user = new User
            {
                First_name = first_name,
                Last_name = last_name,
                Username = username,
                Password = password
            };
            
            var id = await db.InsertAsync(user);
        }
        
        public static async Task RemoveUser(int id)
        {
            await Init();

            await db.DeleteAsync<User>(id);
        }

        public static async Task<IEnumerable<User>> GetUser()
        {
            await Init();

            var user = await db.Table<User>().ToListAsync();
            return user;
        }

        public static async Task<User> GetUser(int id)
        {
            await Init();

            var user = await db.Table<User>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return user;
        }

        public static async Task<User> GetUserByUsername(string username)
        {
            await Init();

            return await db.Table<User>().FirstOrDefaultAsync(u => u.Username == username);
        }

        //-----------to-do-list-stuff----------------------------------------------------
        public static async Task<IEnumerable<ToDoItem>> GetToDoItemsForUser()
        {
            await Init();

            int? userId = UserSessionService.Instance.CurrentUserId;
            if (userId == null)
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }

            return await db.Table<ToDoItem>()
                           .Where(item => item.UserId == userId.Value)
                           .ToListAsync();
        }        
        public static async Task AddToDoItem(string task, DateTime startTime, DateTime endTime, bool isAllDay)
        {
            await Init();

            // Get the current user's ID from the session
            int? userId = UserSessionService.Instance.CurrentUserId;
            if (userId == null)
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }

            var toDoItem = new ToDoItem
            {
                Task = task,
                StartTime = startTime,
                EndTime = endTime,
                IsAllDay = isAllDay,
                IsCompleted = false,
                UserId = userId.Value // Associate to-do item with the current user
            };

            await db.InsertAsync(toDoItem);
        }

        public static async Task RemoveToDoItem(int id)
        {
            await Init();

            await db.DeleteAsync<ToDoItem>(id);
        }

        public static async Task UpdateToDoItemStatus(int id, bool isCompleted)
        {
            await Init();

            var item = await db.Table<ToDoItem>().FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                item.IsCompleted = isCompleted;
                await db.UpdateAsync(item);
            }
        }

        //-----------goal-stuff--------------------------------------------------------

        public static async Task<IEnumerable<GoalItem>> GetGoalItemsForUser()
        {
            await Init();

            int? userId = UserSessionService.Instance.CurrentUserId;
            if (userId == null)
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }

            return await db.Table<GoalItem>()
                           .Where(item => item.UserId == userId.Value)
                           .ToListAsync();
        }
        public static async Task AddGoalItem(string goal, DateTime startTime, DateTime endTime, bool isAllDay)
        {
            await Init();

            // Get the current user's ID from the session
            int? userId = UserSessionService.Instance.CurrentUserId;
            if (userId == null)
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }

            var goalItem = new GoalItem
            {
                Goal = goal,
                StartTime = startTime,
                EndTime = endTime,
                IsAllDay = isAllDay,
                IsCompleted = false,
                UserId = userId.Value // Associate to-do item with the current user
            };

            await db.InsertAsync(goalItem);
        }

        public static async Task RemoveGoalItem(int id)
        {
            await Init();

            await db.DeleteAsync<GoalItem>(id);
        }

        public static async Task UpdateGoalItemStatus(int id, bool isCompleted)
        {
            await Init();

            var item = await db.Table<GoalItem>().FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                item.IsCompleted = isCompleted;
                await db.UpdateAsync(item);
            }
        }

        //-----------------------------------------------------------------------------------
    }
}
