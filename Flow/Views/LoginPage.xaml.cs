using Flow.ViewModels;

namespace Flow.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new User_DatabaseViewModel();
    }
}