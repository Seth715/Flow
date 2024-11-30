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
            Events = new EventCollection();
        }
public async Task LoadCalendarEvents()
        {
            try
            {
                var toDoItems = await LocalDBService.GetToDoItemsForUser();
                var goalItems = await LocalDBService.GetGoalItemsForUser();

                foreach (var item in toDoItems)
                {
                    var calendarEvent = new CalendarEvent
                    {
                        Title = item.Task,
                        Description = "Task",
                        StartTime = item.StartTime,
                        EndTime = item.EndTime.AddHours(1), // Example duration
                        Color = "Blue"
                    };
                    // Add to the EventCollection with DateTime as key
                    Events.Add(item.EndTime.Date, new List<CalendarEvent> { calendarEvent });
                }

                foreach (var item in goalItems)
                {
                    var calendarEvent = new CalendarEvent
                    {
                        Title = item.Goal,
                        Description = "Goal",
                        StartTime = item.StartTime,
                        EndTime = item.EndTime.AddHours(1), // Example duration
                        Color = "Green"
                    };
                    // Add to the EventCollection with DateTime as key
                    Events.Add(item.EndTime.Date, new List<CalendarEvent> { calendarEvent });
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
