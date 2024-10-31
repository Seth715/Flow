using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Flow.Models;
using MvvmHelpers.Commands;
using Flow.Local_Database;

namespace Flow.ViewModels
{
    public partial class User_DatabaseViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {

        [ObservableProperty]
        private string firstNameDB;

        [ObservableProperty]
        private string lastNameDB;

        [ObservableProperty]
        private string usernameDB;

        [ObservableProperty]
        private string passwordDB;

        public ObservableRangeCollection<User> User { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<User> RemoveCommand { get; }

        public User_DatabaseViewModel() 
        {
            User = new ObservableRangeCollection<User>();

            RefreshCommand = new AsyncCommand(Refresh);
            RemoveCommand = new AsyncCommand<User>(Remove);
        }

        async Task Remove(User user)
        {
            await LocalDBService.RemoveUser(user.Id);
            await Refresh();
        }
        async Task Refresh()
        {
            await Task.Delay(2000);

            User.Clear();

            var users = await LocalDBService.GetUser();

            User.AddRange(users);
        }

        [RelayCommand]
        async Task SettingsAsync()
        {
            await Shell.Current.GoToAsync("//SettingsPage", true);
        }
    }
}
