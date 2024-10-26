using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.ViewModels
{
    public partial class User_DatabaseViewModel
    {
        [RelayCommand]
        async Task SettingsAsync()
        {
            await Shell.Current.GoToAsync("//SettingsPage", true);
        }
    }
}
