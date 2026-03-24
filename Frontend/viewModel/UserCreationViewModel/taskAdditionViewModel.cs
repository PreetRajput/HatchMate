using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using models.Dtos.TaskDtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class taskAdditionViewModel: ObservableObject
    {
        [ObservableProperty] 
        ObservableCollection<taskItem> tasks= new();

        [ObservableProperty]
        double rotateY;
        public TaskListDto _tasks;
        public taskAdditionViewModel(TaskListDto tasks )
        {
            _tasks = tasks;
        }
       
        [RelayCommand]
        void addGoal()
        {
            Tasks.Add(new taskItem{Text = "Custom Task", EntryText = ""});
          
        }
        [RelayCommand]
        async void AnimateImage()
        {
            while (true)
            {
                for (global::System.Int32 i = 0; i < 360; i+=2)
                {
                    RotateY = i;
                    await Task.Delay(16);
                }
                RotateY = 0;
            }
        }


        [RelayCommand]
        public async Task hatchEgg()
        {
            foreach (var item in Tasks)
            {
                Console.WriteLine("before the loop");
                 await Application.Current.MainPage.DisplayAlert("Success", $"{item.EntryText}", "OK");
                  _tasks.Tasks.Add(item.EntryText);
                Console.WriteLine("after the loop ");
            }
            Console.WriteLine("omggggg");
            var page = ((App)Application.Current).Services.GetRequiredService<petNameInput>();
            await Application.Current.MainPage.Navigation.PushAsync(page);

        }

        [RelayCommand]
        void removeCont(taskItem item)
        {
            if (item != null && Tasks.Contains(item))
                Tasks.Remove(item);
        }
       
            public partial class taskItem:ObservableObject
            {
                    [ObservableProperty]
                     string text;
                    [ObservableProperty]
                    string entryText;
            }

    }

}
