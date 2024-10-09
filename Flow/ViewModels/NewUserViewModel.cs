using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Flow.ViewModels
{
    public partial class NewUserViewModel : ObservableObject
    {
        [RelayCommand]

        async Task CreateAccountAsync()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}