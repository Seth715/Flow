using Flow.ViewModels;
using Flow.Local_Database;

namespace Flow.Views;

public partial class ProfilePage : ContentPage
{
    private readonly User_DatabaseViewModel _database;
    public ProfilePage()
	{
        InitializeComponent();
        BindingContext = new User_DatabaseViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadLoggedInUser();
    }

    private async Task LoadLoggedInUser()
    {
        int? userId = UserSessionService.Instance.CurrentUserId;
        if (userId.HasValue)
        {
            var user = await LocalDBService.GetUserById(userId.Value);
            if (user != null)
            {
                UserIdLabel.Text = "User ID: " + user.Id;
                NameLabel.Text = "Name: " + user.First_name + ' ' + user.Last_name;
                UsernameLabel.Text = "Username: " + user.Username;
                PasswordLabel.Text = "Password: " + user.Password;
            }
        }
    }
}