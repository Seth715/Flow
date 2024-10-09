using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flow.Views;

namespace Flow.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    [RelayCommand]
    async Task LoginAsync()
    {
        await Shell.Current.GoToAsync("//HomePage");
    }

    [RelayCommand]

    async Task NewUserAsync()
    {
        await Shell.Current.GoToAsync("//NewUserPage");
    }
}

