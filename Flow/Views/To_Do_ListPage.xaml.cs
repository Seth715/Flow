using Flow.ViewModels;

namespace Flow.Views;

public partial class To_Do_ListPage : ContentPage
{
    private readonly ToDoListViewModel _viewModel;

    public To_Do_ListPage()
    {
        InitializeComponent();
        _viewModel = new ToDoListViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadToDoItemsForUser(); // Call the function when the page appears
    }
}