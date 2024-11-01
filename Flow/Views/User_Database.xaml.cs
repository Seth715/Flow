using Flow.ViewModels;
namespace Flow.Views;

public partial class User_Database : ContentPage
{
    public User_Database()
    {
        InitializeComponent();
        BindingContext = new User_DatabaseViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = (User_DatabaseViewModel)BindingContext;
        if (vm.User.Count == 0)
            await vm.RefreshCommand.ExecuteAsync();
    }
}