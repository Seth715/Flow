using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace Flow.Views;

public partial class CalendarPage : ContentPage
{
    private DateTime _currentDate;
    public CalendarPage()
	{
		InitializeComponent();
        _currentDate = DateTime.Now; // Set current month
        LoadCalendar();
    }

    private void LoadCalendar()
    {
        // Set the header for the current month and year
        MonthYearLabel.Text = _currentDate.ToString("MMMM yyyy");

        // Get the first day of the month
        DateTime firstDayOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);

        // Determine the number of days in the month
        int daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

        // Determine the day of the week the month starts on
        int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

        // Fill the grid with dates
        List<string> days = new List<string>();

        // Add blank entries for previous month's trailing days
        for (int i = 0; i < startDayOfWeek; i++)
        {
            days.Add(string.Empty);
        }

        // Add days for the current month
        for (int day = 1; day <= daysInMonth; day++)
        {
            days.Add(day.ToString());
        }

        CalendarGrid.ItemsSource = days;
    }

    private void OnPrevMonthClicked(object sender, EventArgs e)
    {
        _currentDate = _currentDate.AddMonths(-1);
        LoadCalendar();
    }

    private void OnNextMonthClicked(object sender, EventArgs e)
    {
        _currentDate = _currentDate.AddMonths(1);
        LoadCalendar();
    }
}