using Flow.ViewModels;
using Plugin.Maui.Calendar.Models;
using Microsoft.Maui.Controls;
using System;

namespace Flow.Views
{
    public partial class CalendarPage : ContentPage
    {
        private readonly CalendarViewModel _calendarViewModel;
        public CalendarPage()
        {
            InitializeComponent();
            _calendarViewModel = new CalendarViewModel();
            BindingContext = _calendarViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _calendarViewModel.LoadCalendarEvents();
        }
    }
}

