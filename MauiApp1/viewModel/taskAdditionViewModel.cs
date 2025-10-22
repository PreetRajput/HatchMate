using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.apiCalls;
using Microsoft.Maui.Controls.Shapes;
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


        userDetails Player;
        public taskAdditionViewModel(userDetails Player)
        {
            this.Player = Player;
        }

        [RelayCommand]
        void addGoal()
        {
            Tasks.Add(new taskItem{Text = "Custom Task", EntryText = ""});
          
        }
      

        [RelayCommand]
        public async Task hatchEgg()
        {
            foreach (var item in Tasks)
            {
                 await Application.Current.MainPage.DisplayAlert("Success", $"{item.EntryText}", "OK");
                  Player.tasks.Add(item.EntryText);
            }
            await Application.Current.MainPage.Navigation.PushAsync(new NewPage5(Player));

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
