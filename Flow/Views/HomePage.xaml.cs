using Flow.ViewModels;
using Flow.Local_Database;

namespace Flow.Views;

public partial class HomePage : ContentPage
{
    private HomePageViewModel _viewModel;

    public HomePage()
	{
		InitializeComponent();
		_viewModel = new HomePageViewModel();
		BindingContext = _viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!UserSessionService.Instance.IsUserLoggedIn)
        {
            _viewModel.CurrentUser = null;
        }
        else
        {
            _viewModel.LoadUserData();
        }
    }
}