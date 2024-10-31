using Flow.ViewModels;
namespace Flow.Views;

public partial class User_Database : ContentPage
{
    public User_Database()
    {
        InitializeComponent();
        BindingContext = new User_DatabaseViewModel();
    }
}