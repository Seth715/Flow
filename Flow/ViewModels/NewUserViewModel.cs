using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flow.Local_Database;

namespace Flow.ViewModels
{
    public partial class NewUserViewModel : ObservableObject
    {
        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;
        
        [RelayCommand]
        async Task CreateAccountAsync()
        {
            await LocalDBService.AddUser(firstName, lastName, username, password);
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}