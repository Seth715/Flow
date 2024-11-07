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
    }
}
