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

        //boolean variable for if user is logged in
        public bool IsUserLoggedIn => CurrentUserId != null;
    }
}
