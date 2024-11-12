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
        private string firstName = string.Empty;

        [ObservableProperty]
        private string lastName = string.Empty;

        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

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
        async Task LoginAsync()
        {
            var user = await LocalDBService.GetUserByUsername(Username);

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }
            else if (user == null)
            {
                await Shell.Current.DisplayAlert("Error", "Username does not exit.", "OK");
                
                Username = string.Empty;
                Password = string.Empty;
        
                return;
            }
            else if (user.Password != Password)
            {
                await Shell.Current.DisplayAlert("Error", "Incorrect password.", "OK");
                
                Password = string.Empty;
                
                return;
            } else 
            {
                UserSessionService.Instance.SetUser(user.Id); //-----------new code

                await Shell.Current.GoToAsync("//HomePage");

                Username = string.Empty;
                Password = string.Empty;
            }
        }

        [RelayCommand]
        async Task NewUserAsync()
        {
            await Shell.Current.GoToAsync("//NewUserPage");
        }

        [RelayCommand]
        async Task CreateAccountAsync()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "All fields are required.", "OK");
                return;
            } 

            await LocalDBService.AddUser(first_name: FirstName, last_name: LastName, username: Username, password: Password);
                        
            await Shell.Current.GoToAsync("//LoginPage");

            FirstName = string.Empty;
            LastName = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
        }

       [RelayCommand]
       async Task LogoutAsync()
       {
           await Shell.Current.GoToAsync("//LoginPage", true);
       
           FirstName = string.Empty;
           LastName = string.Empty;
           Username = string.Empty;
           Password = string.Empty;
       }

        [RelayCommand]
        async Task SettingsAsync()
        {
            await Shell.Current.GoToAsync("//SettingsPage", true);
        }
    }
}
