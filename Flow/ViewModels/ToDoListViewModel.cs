using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flow.Local_Database;
using Flow.Models;
using System.Collections.ObjectModel;

namespace Flow.ViewModels
{
    public class ToDoListViewModel : ObservableObject
    {
        public ObservableCollection<ToDoItem> ToDoItems { get; } = new ObservableCollection<ToDoItem>();

        private string _newTask;
        public string NewTask
        {
            get => _newTask;
            set => SetProperty(ref _newTask, value);
        }

        private DateTime _newStartTime;
        public DateTime NewStartTime
        {
            get => _newStartTime;
            set => SetProperty(ref _newStartTime, value);
        }

        private DateTime _newEndTime;
        public DateTime NewEndTime
        {
            get => _newEndTime;
            set => SetProperty(ref _newEndTime, value);
        }

        private bool _isAllDay;
        public bool IsAllDay
        {
            get => _isAllDay;
            set => SetProperty(ref _isAllDay, value);
        }
        public ToDoListViewModel()
        {
            ToDoItems.Clear();
            NewStartTime = DateTime.Now; // Default start time
            NewEndTime = DateTime.Now.AddHours(1);
        }

        public async Task LoadToDoItemsForUser()
        {
            if (UserSessionService.Instance.IsUserLoggedIn)
            {
                var userId = UserSessionService.Instance.CurrentUserId;

                ToDoItems.Clear();

                var toDoItems = await LocalDBService.GetToDoItemsForUser();

                foreach (var item in toDoItems)
                {
                    ToDoItems.Add(item);
                }
            }
            else
            {
                ToDoItems.Clear();
            }
        }

        // Command to add a new task
        public AsyncRelayCommand AddToDoItemCommand => new AsyncRelayCommand(async () =>
        {
            if (string.IsNullOrWhiteSpace(NewTask)) return;

            // Add the new task to the database
            await LocalDBService.AddToDoItem(NewTask, NewEndTime);

            // Clear the input field
            NewTask = string.Empty;
            NewEndTime =(DateTime.Now.AddHours(1));

            // Reload the to-do items
            await LoadToDoItemsForUser();
        });

        // Command to delete a task
        public AsyncRelayCommand<ToDoItem> DeleteCommand => new AsyncRelayCommand<ToDoItem>(async (item) =>
        {
            if (item == null) return;

            // Delete the to-do item from the database
            await LocalDBService.RemoveToDoItem(item.Id);

            // Reload the to-do items
            await LoadToDoItemsForUser();
        });
    }
}
