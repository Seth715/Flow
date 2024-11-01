using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Flow.Models;
using MvvmHelpers.Commands;
using Flow.Local_Database;
using Flow.Views;

namespace Flow.ViewModels
{
    public partial class User_DatabaseViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        public ObservableRangeCollection<User> User { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<User> RemoveCommand { get; }
        public AsyncCommand<User> SelectedCommand { get; }

        public User_DatabaseViewModel() 
        {
            User = new ObservableRangeCollection<User>();

            RefreshCommand = new AsyncCommand(Refresh);
            RemoveCommand = new AsyncCommand<User>(Remove);
            SelectedCommand = new AsyncCommand<User>(Selected);
        }

        async Task Selected(User user)
        {
            if (user == null)
                return;

            var route = $"{nameof(User_Database)}?UserId={user.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(User user)
        {
            await LocalDBService.RemoveUser(user.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            await Task.Delay(500);

            User.Clear();

            var users = await LocalDBService.GetUser();

            User.AddRange(users);
        }

        [RelayCommand]
        async Task CreateAccountAsync()
        {
            await LocalDBService.AddUser(first_name: FirstName, last_name: LastName, username: Username, password: Password);
            await Shell.Current.GoToAsync("//LoginPage");
        }

        [RelayCommand]
        async Task SettingsAsync()
        {
            await Shell.Current.GoToAsync("//SettingsPage", true);
        }
    }
}
