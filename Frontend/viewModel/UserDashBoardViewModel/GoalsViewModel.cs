using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using models.Dtos.TaskDtos;
using models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
    public partial class GoalsViewModel:ObservableObject
    {
        string? id;
        int trash = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        [ObservableProperty]
        ObservableCollection<Filter> filters = new ObservableCollection<Filter>()
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

        bool ToCheckIfTaskCompleted;

        [ObservableProperty]
        bool addBtnVisible = false;

        [ObservableProperty]
        string btnText= "+ Add Goals";

        [ObservableProperty]
        bool isEditing = false;

        List<TaskItemDto> currentTasks;
        TasksIdDto UpdatedTasks= new();
        int totalTask = 0;
        List<string> totalTasks = new();

        TaskListDto updatedUserData = new();

        [ObservableProperty]
        ObservableCollection<TaskList> borderContext= new();
        Collection<TaskList> AllTasks = new();

        public const string auth_token = "auth_token";

         private readonly ApiService _apiService;
        public GoalsViewModel(ApiService api)
        {
            _apiService = api;
        }

        [RelayCommand]
        public async Task addingTasks()
        {
            Debug.WriteLine("addingTasks command run");
            try
            {

            currentTasks = await _apiService.RetrieveUserTasksAsync();
            BorderContext.Clear();


                for (int i = 0; i < currentTasks.Count; i++)
                {
                    AllTasks.Add(new TaskList
                    {
                        Goal = currentTasks[i].Task,
                        BgColor = "Transparent",
                        Id = currentTasks[i].Id,
                        IsSelected = false,
                        IsCompleted = currentTasks[i].IsCompleted
                    });
                    if (!currentTasks[i].IsCompleted)
                    {
                        BorderContext.Add(AllTasks[i]);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("errorr", ex.ToString());
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
            TaskList newTask = new TaskList { Goal = "" };
            AllTasks.Add(newTask);
            BorderContext.Add(newTask);
            NewAddedTasks.Add(newTask);
            
        }

        [RelayCommand]
        public async void deleteTask(TaskList task)
        {
            Debug.WriteLine("deleteTask run");

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
            Debug.WriteLine("markAsCompleetd ran");
            Debug.WriteLine("color of the task was", task.BgColor);

            task.BgColor = task.BgColor == "Transparent" ? "LightGreen" : "Transparent";

            Debug.WriteLine("color of the task is", task.BgColor);


            task.IsSelected = task.IsSelected == true ? false : true;

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
        public async  void updateCompletedTask()
        {
            try
            {
               
                var toProcess = BorderContext.Where(x => x.IsSelected).ToList();

                foreach (var item in toProcess)
                {
                    UpdatedTasks.TaskIds.Add(item.Id);

                    item.IsCompleted = !item.IsCompleted;
                    item.IsSelected = item.IsSelected == true ? false : true;
                    UpdatedTasks.IsCompleted = item.IsCompleted;

                    BorderContext.Remove(item);

                    Debug.WriteLine($"goal id is {item.Id}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.ToString()}");
            }

                await _apiService.UpdateTaskToCompletedAsync(UpdatedTasks);
                UpdatedTasks.TaskIds.Clear();
                TaskName = TaskMode.ADD;
                Debug.WriteLine("value of TaskName is:", TaskName);
            

        }

       

        [RelayCommand]
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

                    totalTasks.Add(item.Goal!);
                }
                    updatedUserData.Tasks = totalTasks;
               List<TaskItemDto> goalsList=  await _apiService.PostTaskAsync(updatedUserData);
                    for(int i= 0; i<goalsList.Count; i++)
                {
                    Debug.WriteLine("UMOKAYYY");
                    foreach (var item in BorderContext)
                    {
                    Debug.WriteLine("UMOKAYYY12");
                    Debug.WriteLine(item.Id);
                        if (item.Id == Guid.Empty)
                        {
                            Debug.WriteLine("UMOKAYYY12345");
                            item.Id = goalsList[i].Id;
                            break;
                        }
                    }
                }
                NewAddedTasks.Clear();
                totalTasks.Clear();
            }

        }

        public void ShowFilteredTasks()
        {
            TaskName = TaskMode.ADD;

            BorderContext.Clear();
            if(SelectedFilter == Filter.Completed)
            {
                foreach (var task in AllTasks)
                {
                    if (task.IsCompleted)
                    {
                        task.BgColor = "LightGreen";
                        BorderContext.Add(task);
                    }
                }
            }
            else
            {
                foreach (var task in AllTasks)
                {
                    if (!task.IsCompleted)
                    {
                        task.BgColor = "Transparent";
                        BorderContext.Add(task);
                    }
                }
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
