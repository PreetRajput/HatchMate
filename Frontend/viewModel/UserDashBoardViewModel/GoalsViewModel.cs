using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HatchMate.Api;
using MauiApp1.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp1.viewModel
{
    public enum TaskMode
    {
        ADD,
        DONE,
        PATCH
    };
    public enum Filter
    {
        Completed,
        Pending
    };
    public partial class GoalsViewModel(ApiService api) : ObservableObject
    {
        string? id;
        int trash = 0;
        int skip = 0;
        int take = 10;
        bool hasMoreTask;
        [ObservableProperty]
        bool isLoading = false;
        public List<int> SkeletonItems { get; } =
        [
            1,2,3
        ];
        [ObservableProperty]
        ObservableCollection<Filter> filters = new()
        {
          Filter.Completed,
          Filter.Pending
        };
        private Filter selectedFilter= Filter.Pending;
        public  Filter SelectedFilter
        {
            get
            {
                return selectedFilter;
            }
            set
            {
                selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
                ShowFilteredTasks();
            }
        }
        [ObservableProperty]
        ICommand taskBtn;

        [ObservableProperty]
        public TaskMode? taskName= TaskMode.ADD;

        bool forColor= true;
        List<TaskList> NewAddedTasks = new List<TaskList>();

        [ObservableProperty]
        bool addBtnVisible = false;

        [ObservableProperty]
        string btnText= "+ Add Goals";

        [ObservableProperty]
        bool isEditing = false;

        List<TaskItemDto>? currentIncompleteTasks;
        TasksIdDto UpdatedTasks= new();

        readonly TaskListDto updatedUserData = new();

        [ObservableProperty]
        ObservableCollection<TaskList> borderContext= new();

        public const string auth_token = "auth_token";

         private readonly ApiService _apiService = api;

        [RelayCommand]
        public async Task addingTasks()
        {
            try
            {
                currentIncompleteTasks = (await _apiService.RetrieveUserIncompleteTasksAsync())?.ToList();
                var items = new List<TaskList>();
                if (currentIncompleteTasks != null)
                {
                    for (int i = 0; i < currentIncompleteTasks.Count; i++)
                    {
                        items.Add(new TaskList
                        {
                            Goal = currentIncompleteTasks[i].Task,
                            BgColor = "Transparent",
                            Id = currentIncompleteTasks[i].Id,
                            IsSelected = false,
                            IsCompleted = currentIncompleteTasks[i].IsCompleted
                        });
                    }
                }
                BorderContext = new ObservableCollection<TaskList>(items);
            }
            catch(Exception ex)
            {
                Console.WriteLine("errorr", ex);
            }
        }

        [RelayCommand]
        public void addMoreTasks()
        {
            IsEditing = true;
            AddBtnVisible = true;
            TaskName = TaskMode.DONE;
        }

        [RelayCommand]
        public void addOneMoreTask()
        {
            TaskList newTask = new() { Goal = "" };
            BorderContext.Add(newTask);
            NewAddedTasks.Add(newTask);
        }

        [RelayCommand]
        public async Task deleteTask(TaskList task)
        {
            BorderContext.Remove(task);
            NewAddedTasks.Remove(task);

            Guid id = task.Id;
            try
            {
                if(!(id == Guid.Empty))
                {
                  await _apiService.DeleteTaskAsync(id);
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine("excpetion is ", ex.ToString());
            }
        }

        [RelayCommand]
        public void markAsCompleted(TaskList task)
        {
            task.BgColor = task.BgColor == "Transparent" ? "LightGreen" : "Transparent";
            task.IsSelected = task.IsSelected != true;
            foreach (var item in BorderContext)
                {
                    if (item.IsSelected)
                    {
                        TaskName = TaskMode.PATCH;
                        forColor = false;
                    }
                }
            if (forColor)
            {
                TaskName = TaskMode.ADD;
            }
                forColor= true;
        }

        [RelayCommand]
        public async Task updateCompletedTask()
        {
            try
            {
                UpdatedTasks.TaskIds ??= new List<Guid>();
                var toProcess = BorderContext.Where(x => x.IsSelected).ToList();
                foreach (var item in toProcess)
                {
                    UpdatedTasks.TaskIds.Add(item.Id);
                    item.IsCompleted = !item.IsCompleted;
                    item.IsSelected = !item.IsSelected;
                    UpdatedTasks.IsCompleted = item.IsCompleted;
                    BorderContext.Remove(item);
                }
                await _apiService.UpdateTaskToCompletedAsync(UpdatedTasks);
                UpdatedTasks.TaskIds.Clear();
                TaskName = TaskMode.ADD;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex}");
            }
        }

       

        [RelayCommand]
        [Obsolete]
        public async Task addedMoreTasks()
        {
            trash = 0;
            foreach (var item in BorderContext)
            {
                if (string.IsNullOrWhiteSpace(item.Goal))
                {
                   trash++;
                   await Application.Current.MainPage.DisplayAlert("error", "please fill up all tasks", "OK");
                }
            }
            if (trash == 0)
            {
                TaskName = TaskMode.ADD;
                AddBtnVisible = false;
                IsEditing = false;
                foreach (var item  in NewAddedTasks)
                {
                    updatedUserData.Tasks.Add(item.Goal!);
                }
                    List<TaskItemDto>? goalsList=  (await _apiService.PostTaskAsync(updatedUserData))?.ToList();
                    for(int i= 0; i<goalsList?.Count; i++)
                    {
                        foreach (var item in BorderContext)
                        {
                            if (item.Id == Guid.Empty)
                            {
                                item.Id = goalsList.ElementAt(i).Id;
                                break;
                            }
                        }
                    }
                NewAddedTasks.Clear();
            }
        }

        public async Task ShowFilteredTasks()
        {
            try
            {
                TaskName = TaskMode.ADD;
                if (SelectedFilter == Filter.Completed)
                {
                    var CompletedTasks = await _apiService.RetrieveUserCompletedTasksAsync(0, 10);
                    hasMoreTask = CompletedTasks.HasMoreTask;
                    var items = CompletedTasks.Tasks.Select(task => new TaskList
                    {
                        Goal = task.Task,
                        BgColor = "Green",
                        Id = task.Id,
                        IsSelected = false,
                        IsCompleted = task.IsCompleted
                    }).ToList();
                    BorderContext = new ObservableCollection<TaskList>(items);
                }
                else
                {
                    var IncompleteTasks = (await _apiService.RetrieveUserIncompleteTasksAsync())?.ToList() ?? new List<TaskItemDto>();
                    var items = IncompleteTasks.Select(task => new TaskList
                    {
                        Goal = task.Task,
                        BgColor = "Transparent",
                        Id = task.Id,
                        IsSelected = false,
                        IsCompleted = task.IsCompleted
                    }).ToList();
                    BorderContext = new ObservableCollection<TaskList>(items);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        [RelayCommand]
        public async Task LoadMoreTasks()
        {
            if(hasMoreTask)
            { 
                skip += take;
                take = 10;
                var completedTasks = await _apiService.RetrieveUserCompletedTasksAsync(skip, take);
                hasMoreTask = completedTasks.HasMoreTask;
                var newItems = completedTasks.Tasks.Select(task => new TaskList
                {
                    Goal = task.Task,
                    BgColor = "Green",
                    Id = task.Id,
                    IsSelected = false,
                    IsCompleted = task.IsCompleted
                }).ToList();

                var existing = BorderContext ?? new ObservableCollection<TaskList>();
                var combined = existing.Concat(newItems).ToList();
                BorderContext = new ObservableCollection<TaskList>(combined);
            }
        }
        public partial class TaskList: ObservableObject
        {
            [ObservableProperty]
            public string? goal;

            public Guid Id;
            
            public bool IsCompleted;

            public bool IsSelected;

            [ObservableProperty]
            public string? bgColor = "Transparent";

        }

    }

}
