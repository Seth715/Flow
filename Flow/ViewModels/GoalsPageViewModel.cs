using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flow.Local_Database;
using Flow.Models;
using System.Collections.ObjectModel;

namespace Flow.ViewModels
{
    public class GoalsPageViewModel : ObservableObject
    {
        public ObservableCollection<GoalItem> GoalItems { get; } = new ObservableCollection<GoalItem>();

        private string _newGoal;
        public string NewGoal
        {
            get => _newGoal;
            set => SetProperty(ref _newGoal, value);
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
        public GoalsPageViewModel()
        {
            GoalItems.Clear();
            NewStartTime = DateTime.Now; // Default start time
            NewEndTime = DateTime.Now.AddHours(1);
        }

        public async Task LoadGoalItemsForUser()
        {
            if (UserSessionService.Instance.IsUserLoggedIn)
            {
                var userId = UserSessionService.Instance.CurrentUserId;

                GoalItems.Clear();

                var goalItems = await LocalDBService.GetGoalItemsForUser();


                foreach (var item in goalItems)
                {
                    GoalItems.Add(item);
                }
            }
            else
            {
                GoalItems.Clear();
            }
        }

        // Command to add a new task
        public AsyncRelayCommand AddGoalItemCommand => new AsyncRelayCommand(async () =>
        {
            if (string.IsNullOrWhiteSpace(NewGoal)) return;

            // Add the new task to the database
            await LocalDBService.AddGoalItem(NewGoal, NewStartTime, NewEndTime, IsAllDay);

            // Clear the input fields
            NewGoal = string.Empty;
            NewStartTime = DateTime.Now;
            NewEndTime = (DateTime.Now.AddHours(1));
            IsAllDay = false;

            // Reload the to-do items
            await LoadGoalItemsForUser();
        });

        // Command to delete a task
        public AsyncRelayCommand<GoalItem> DeleteCommand => new AsyncRelayCommand<GoalItem>(async (item) =>
        {
            if (item == null) return;

            // Delete the to-do item from the database
            await LocalDBService.RemoveGoalItem(item.Id);

            // Reload the to-do items
            await LoadGoalItemsForUser();
        });
    }
}
