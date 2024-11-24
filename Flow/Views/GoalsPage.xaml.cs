using Flow.ViewModels;

namespace Flow.Views;

public partial class GoalsPage : ContentPage
{
    private readonly GoalsPageViewModel _viewModel;
    public GoalsPage()
	{
		InitializeComponent();
        _viewModel = new GoalsPageViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadGoalItemsForUser(); // Call the function when the page appears
    }
}