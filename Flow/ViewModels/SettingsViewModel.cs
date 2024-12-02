using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flow.Local_Database;
using Flow.ViewModels;
using Flow.Models;

namespace Flow.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [RelayCommand]
        async Task LogoutAsync()
        {
            UserSessionService.Instance.ClearUser();
          
            await Shell.Current.GoToAsync("//LoginPage", true);
        }

        [RelayCommand]
        async Task ViewDatabaseAsync()
        {
            await Shell.Current.GoToAsync("//User_Database", true);
        }

        [RelayCommand]
        async Task ProfilePageAsync()
        {
            await Shell.Current.GoToAsync("//ProfilePage", true);
        }
    }
}
