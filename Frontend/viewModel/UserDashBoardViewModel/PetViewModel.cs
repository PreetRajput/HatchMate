using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using models.Dtos.PetDtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class PetViewModel: ObservableObject
    {
        public ApiService _apiService;
        public Action<string>[] _setter;
        public PetViewModel(ApiService apiService)
        {
            _apiService = apiService;
            _setter = new Action<string>[]
             {
                       v => EmoteOne =v,
                       v => EmoteTwo =v,
                       v => EmoteThree =v,
                       v => EmoteFour =v,

                };
        }


        [ObservableProperty]
        public string petImage= "./pets/default_img.png";


        [ObservableProperty]
        public int petLevel = 0;

        [ObservableProperty]
        public string emoteOne = "./pets/default_img.png" ;
        
        [ObservableProperty]
        public string emoteTwo = "./pets/default_img.png";
        
        [ObservableProperty]
        public string emoteThree= "./pets/default_img.png";
        
        [ObservableProperty]
        public string emoteFour= "./pets/default_img.png";

        [ObservableProperty]
        public string petName;

        [RelayCommand]
        public async Task getEmotes()
        {
            try
            {
                var petInfo = await _apiService.GetPetAsync();
                PetDto dto = new PetDto()
                {
                    Pet_Type = petInfo.Pet_Type,
                    Pet_Level = petInfo.Pet_Level,
                };
                PetName = petInfo.PetName;
                PetLevel = petInfo.Pet_Level;
                if(petInfo.Pet_Type is "cow")
                {
                    PetImage = "./pets/cow/cute/c1.png";
                }
                else if (petInfo.Pet_Type is "DragonWarrior")
                {
                    PetImage = "./pets/dragon_warrior.png";
                }
                var emoteInfo = await _apiService.GetAnimationAsync(dto);
                for (int i = 0; i < Math.Min(_setter.Length, emoteInfo.Count); i++)
                {
                    _setter[i](emoteInfo[i].Icon);
                }
            }
            catch(Exception e)
            {
                return;
            }

        }

        public async Task IdleAnimaton()
        {
   
        }

    }
}
