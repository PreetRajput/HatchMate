using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.apiCalls;
using MauiApp1.model;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.viewModel
{
    public partial class GoalsViewModel:ObservableObject
    {
        string? id;
        int trash = 0;


        [ObservableProperty]
        ICommand taskBtn;

        List<string> totalTasks = new List<string>();


        [ObservableProperty]
        bool addBtnVisible = false;

        [ObservableProperty]
        string btnText= "+ Add Goals";

        [ObservableProperty]
        bool isEditing = false;

        int totalTask = 0;

        [ObservableProperty]
        ObservableCollection<TaskList> borderContext= new();

         updateData postApi = new updateData();
        [RelayCommand]
        public async Task addingTasks()
        {
           TaskBtn= addMoreTasksCommand;
            var user = detailsPage.currentUser?.tasks;
            if (user is null)
            {
                id = Preferences.Get("email", string.Empty);
                var apiForRetrieve = new retrieveData();
                userDetails check = await apiForRetrieve.retrieveUserData(id);
                detailsPage.currentUser = check;
                user = check?.tasks;
            }

            foreach (var item in user)
            {
                     BorderContext.Add(new TaskList { Goal = item });
            }
          

        }

        [RelayCommand]
        public void addMoreTasks()
        {
            IsEditing = true;
           AddBtnVisible = true;

            BtnText= "Done";
            TaskBtn = addedMoreTasksCommand;

        }
        [RelayCommand]
        public void addOneMoreTask()
        {
            BorderContext.Add(new TaskList { Goal = "" });

        }

        [RelayCommand]
        public async void deleteTask(TaskList task)
        {
            BorderContext.Remove(task);
            totalTasks.Clear();

            foreach (var item in BorderContext)
            {
                totalTasks.Add(item.Goal!);
            }
            userDetails updatedUserData = new();
            if (detailsPage.currentUser?.id is not null)
            {
                updatedUserData.id = detailsPage.currentUser?.id;
            }
            else
            {
                id = Preferences.Get("email", string.Empty);
                updatedUserData.id = id;
            }
            updatedUserData.tasks = totalTasks;
            await postApi.updateUserInfo(updatedUserData);
        }


        [RelayCommand]
        public async Task addedMoreTasks()
        {
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

                BtnText = "+ Add Goals";
                TaskBtn = addMoreTasksCommand;

                AddBtnVisible = false;

                IsEditing = false;


                totalTasks.Clear();

                foreach (var item  in BorderContext)
                {
                    totalTasks.Add(item.Goal!);
                }
                userDetails updatedUserData = new();
                if (detailsPage.currentUser?.id is not null)
                {
                        updatedUserData.id = detailsPage.currentUser?.id;
                }
                else
                { 
                    id = Preferences.Get("email", string.Empty);
                    updatedUserData.id = id;
                }
                    updatedUserData.tasks = totalTasks;
                await postApi.updateUserInfo(updatedUserData);

            }

            trash = 0;
        }


    
        public partial class TaskList: ObservableObject
        {
            [ObservableProperty]
            public string? goal;

        }

    }

}
