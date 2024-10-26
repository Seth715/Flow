using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Flow.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [RelayCommand]
        async Task LogoutAsync()
        {
            await Shell.Current.GoToAsync("//LoginPage", true);
        }

        [RelayCommand]

        async Task ViewDatabaseAsync()
        {
            await Shell.Current.GoToAsync("//User_Database", true);
        }
 
    }
}
