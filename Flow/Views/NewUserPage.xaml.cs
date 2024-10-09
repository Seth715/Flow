using Flow.ViewModels;

namespace Flow.Views;

public partial class NewUserPage : ContentPage
{
    public NewUserPage()
    {
        InitializeComponent();
        BindingContext = new NewUserViewModel();
    }
}