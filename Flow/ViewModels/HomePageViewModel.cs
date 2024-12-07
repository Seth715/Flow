using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flow.Local_Database;
using Flow.Models;

namespace Flow.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private User? _currentUser;
        
        public User? CurrentUser
        {
            get => _currentUser;
            set
            {
                SetProperty(ref _currentUser, value);

                GreetingText = _currentUser != null ? $"Hello, {_currentUser.First_name}" : "Hello, User";
            }
        }

        private string _greetingText;
        public string GreetingText
        {
            get => _greetingText;
            set => SetProperty(ref _greetingText, value);
        }

        public HomePageViewModel()
        {
            LoadUserData();
        }

        public async void LoadUserData()
        {
            var userId = UserSessionService.Instance.CurrentUserId;

            if(userId != null)
            {
                CurrentUser = await LocalDBService.GetUser(userId.Value);
            } else
            {
                GreetingText = "Hello, User";
            }
        }

        [RelayCommand]
        async Task CalendarAsync()
        {
            await Shell.Current.GoToAsync("//CalendarPage");
        }

        [RelayCommand]
        async Task TaskAsync()
        {
            await Shell.Current.GoToAsync("//To_Do_ListPage");
        }

        [RelayCommand]
        async Task GoalAsync()
        {
            await Shell.Current.GoToAsync("//GoalsPage");
        }
    }
}
