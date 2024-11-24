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

        public GoalsPageViewModel()
        {
            GoalItems.Clear();
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
            await LocalDBService.AddGoalItem(NewGoal);

            // Clear the input fields
            NewGoal = string.Empty;

            // Reload the to-do items
            LoadGoalItemsForUser();
        });

        // Command to delete a task
        public AsyncRelayCommand<GoalItem> DeleteCommand => new AsyncRelayCommand<GoalItem>(async (item) =>
        {
            if (item == null) return;

            // Delete the to-do item from the database
            await LocalDBService.RemoveGoalItem(item.Id);

            // Reload the to-do items
            LoadGoalItemsForUser();
        });
    }
}
