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
                UserIdLabel.FormattedText = new FormattedString
                {
                    Spans = {
                    new Span { Text = "User ID: ", FontAttributes = FontAttributes.Bold },
                    new Span { Text = user.Id.ToString() }
                }
                };

                NameLabel.FormattedText = new FormattedString
                {
                    Spans = {
                    new Span { Text = "Name: ", FontAttributes = FontAttributes.Bold },
                    new Span { Text = user.First_name + " " + user.Last_name }
                }
                };

                UsernameLabel.FormattedText = new FormattedString
                {
                    Spans = {
                    new Span { Text = "Username: ", FontAttributes = FontAttributes.Bold },
                    new Span { Text = user.Username }
                }
                };

                PasswordLabel.FormattedText = new FormattedString
                {
                    Spans = {
                    new Span { Text = "Password: ", FontAttributes = FontAttributes.Bold },
                    new Span { Text = user.Password }
                }
                };
            }
        }
    }

}