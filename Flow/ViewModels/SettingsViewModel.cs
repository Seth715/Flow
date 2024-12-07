using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flow.Local_Database;
using Flow.ViewModels;
using Flow.Models;
using CommunityToolkit.Maui.Views;
using Flow.Views;

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
            var popup = new AdminCredentialPopup();
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

            if (result is bool success && success)
            {
                // Validate credentials
                if (popup.EnteredUsername == "admin" && popup.EnteredPassword == "password") // Example check
                {
                    await Shell.Current.GoToAsync("//User_Database", true);
                }
                else if(popup.EnteredUsername == "admin" && popup.EnteredUsername != "password")
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid Admin Password.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid credentials.", "OK");
                }
            }
        }

        [RelayCommand]
        async Task ProfilePageAsync()
        {
            await Shell.Current.GoToAsync("//ProfilePage", true);
        }
    }
}
