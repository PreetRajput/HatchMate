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
        public string petImage= "./pets/default.png";

      
        [ObservableProperty]
        public string emoteOne;
        
        [ObservableProperty]
        public string emoteTwo;
        
        [ObservableProperty]
        public string emoteThree;
        
        [ObservableProperty]
        public string emoteFour;


        public async Task getEmotes()
        {
            var petInfo = await _apiService.GetPetAsync();
            PetDto dto = new PetDto()
            {
                Pet_Type = petInfo.Pet_Type,
                Pet_Level = petInfo.Pet_Level,
            };
            petImage = "sdsd";
            var emoteInfo = await _apiService.GetAnimationAsync(dto);
            for (int i = 0; i < Math.Min(_setter.Length, emoteInfo.Count); i++)
            {
                _setter[i](emoteInfo[i].Icon);
            }

        }

        public async Task IdleAnimaton()
        {
            if(petInfo.Pet_Type=="cow")
            {

            }
        }

    }
}
