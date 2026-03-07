using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class chooseEggViewModel: ObservableObject
    {
        public PetInfoDto pet;
        public chooseEggViewModel(PetInfoDto pet)
        {
            this.pet = pet;
        }
        [RelayCommand]
        public async Task choosedEgg(string eggBtn)
        {
            pet.PetNum = int.Parse(eggBtn);
            var page = ((App)Application.Current).Services.GetRequiredService<taskAddition>();
            var vm = (taskAdditionViewModel)page.BindingContext;
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
