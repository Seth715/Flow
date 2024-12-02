using Flow.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Calendar.Models;
using Plugin.Maui.Calendar.Enums;
using Flow.Local_Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.ViewModels
{
    public partial class CalendarViewModel : ObservableObject
    {
        // Observable collection for binding to Calendar
        public EventCollection Events { get; set; } = new EventCollection();

        //---------------set default values---------------------------------------
        private string _title = "No events available";  // Default value

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description = "No events available";  // Default value

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        //---------------set default values---------------------------------------
        public CalendarViewModel()
        {
            // Load calendar events asynchronously
            Initialize();
        }
        private async void Initialize()
        {
            Events = new EventCollection();
            await LoadCalendarEvents();
        }
        private static IEnumerable<CalendarEvent> GenerateListForDate(IEnumerable<ToDoItem> toDoItems, IEnumerable<GoalItem> goalItems, DateTime specificDate)
        {
            // Combine ToDoItems and GoalItems for the specific date
            var toDoEvents = toDoItems
                .Where(item => item.EndTime.Date == specificDate) // Filter by the specific date
                .Select(item => new CalendarEvent
                {
                    Title = item.Task,
                    Description = "Task",
                    EndTime = item.EndTime.AddHours(1), // Example duration
                });

            var goalEvents = goalItems
                .Where(item => item.EndTime.Date == specificDate) // Filter by the specific date
                .Select(item => new CalendarEvent
                {
                    Title = item.Goal,
                    Description = "Goal",
                    EndTime = item.EndTime.AddHours(1), // Example duration
                });

            // Return a combined list of ToDo and Goal events
            return toDoEvents.Concat(goalEvents);
        }
        public async Task LoadCalendarEvents()
        {
            try
            {
                Events.Clear();

                var toDoItems = await LocalDBService.GetToDoItemsForUser();
                var goalItems = await LocalDBService.GetGoalItemsForUser();

                var uniqueDates = toDoItems
                    .Select(item => item.EndTime.Date)
                    .Concat(goalItems.Select(item => item.EndTime.Date))
                    .Distinct()
                    .OrderBy(date => date) // Optional: Sort the dates
                    .ToList();

                foreach (var date in uniqueDates)
                {
                    Events.Add(date, new List<CalendarEvent>(GenerateListForDate(toDoItems, goalItems, date)));
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
            }
        }
       //public void OnEventTapped(CalendarEvent selectedEvent)
       //{
       //    if (selectedEvent == null) return;
       //
       //    // Handle event type based on its Description or Title
       //    if (selectedEvent.Description == "To-Do Task")
       //    {
       //        // Show details for the selected To-Do task
       //        DisplayToDoDetails(selectedEvent);
       //    }
       //    else if (selectedEvent.Description == "Goal Item")
       //    {
       //        // Show details for the selected Goal
       //        DisplayGoalDetails(selectedEvent);
       //    }
       //}
       //
       //private void DisplayToDoDetails(CalendarEvent selectedEvent)
       //{
       //    // Example logic to display To-Do details
       //    Console.WriteLine($"To-Do: {selectedEvent.Title}, Due: {selectedEvent.StartTime}");
       //    // Trigger navigation or show a modal with details
       //}
       //
       //private void DisplayGoalDetails(CalendarEvent selectedEvent)
       //{
       //    // Example logic to display Goal details
       //    Console.WriteLine($"Goal: {selectedEvent.Title}, Due: {selectedEvent.StartTime}");
       //    // Trigger navigation or show a modal with details
       //}
    }
}
