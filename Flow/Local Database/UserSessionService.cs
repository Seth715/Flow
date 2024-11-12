using Flow.Models;

namespace Flow.Local_Database
{
    public class UserSessionService
    {
        private static UserSessionService _instance;

        // Singleton instance
        public static UserSessionService Instance => _instance ??= new UserSessionService();

        // Property to hold the current user's ID
        public int? CurrentUserId { get; private set; }

        private UserSessionService() { }

        public void SetUser(int userId)
        {
            CurrentUserId = userId;
        }

        public void ClearUser()
        {
            CurrentUserId = null;
        }

        public bool IsUserLoggedIn => CurrentUserId != null;

        public static async Task<IEnumerable<ToDoItem>> GetToDoItemsForLoggedInUser()
        {
            int? userId = UserSessionService.Instance.CurrentUserId;
            if (userId == null)
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }

            // Delegates the call to LocalDBService to fetch to-do items for the current user
            return await LocalDBService.GetToDoItemsForUser();
        }
    }
}
