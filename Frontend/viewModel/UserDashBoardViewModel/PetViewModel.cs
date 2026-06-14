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
        public Action<string>[] _ImageSetter;
        public Action<List<string>>[] _AnimationSetter;
        private CancellationTokenSource? _token;
        public PetViewModel(ApiService apiService)
        {
            _apiService = apiService;
            _ImageSetter = new Action<string>[]
             {
                       v => EmoteOne.Image =v,
                       v => EmoteTwo.Image =v,
                       v => EmoteThree.Image =v,
                       v => EmoteFour.Image =v,

                };
            _AnimationSetter = new Action<List<string>>[]
            {
                       v => EmoteOne.Animation =v,
                       v => EmoteTwo.Animation =v,
                       v => EmoteThree.Animation =v,
                       v => EmoteFour.Animation =v,
            };
        }

        bool _Animating;
        [ObservableProperty]
        public partial string? PetImage { get; set; }

        [ObservableProperty]
        public partial int PetLevel { get; set; } = 0;
        [ObservableProperty]
        public Pet? emoteOne = new();
        
        [ObservableProperty]
        public Pet? emoteTwo = new();

        [ObservableProperty]
        public Pet? emoteThree = new();

        [ObservableProperty]
        public Pet? emoteFour = new();

        [ObservableProperty]
        public partial string? PetName {  get; set; }


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
                var emoteInfo = await _apiService.GetAnimationAsync(dto);
                        for (int i = 0; i < Math.Min(_ImageSetter.Length, emoteInfo.Count); i++)
                    {
                            
                           _ImageSetter[i](emoteInfo[i].Icon);
                           _AnimationSetter[i](emoteInfo[i].Animation.ToList());
                    }
            }
            catch(Exception e)
            {
                return;
            }

        }

        [RelayCommand]
        public async Task PlayAnimation(Pet pet)
        {
            try
            {
                _token?.Cancel();
                _token = new CancellationTokenSource();
                var token = _token.Token;
                if (pet?.Animation == null || pet.Animation.Count == 0)
                    return;

                _Animating = true;

                while (_Animating)
                {
                    foreach (var frame in pet.Animation)
                    {
                        if (token.IsCancellationRequested)
                            return;
                        PetImage = frame;
                        await Task.Delay(100);
                    }
                }
            }
            catch (Exception e)
            {
            
            }

        }
    }
    public partial class Pet : ObservableObject
    {
        [ObservableProperty]
        public partial string? Image { get; set; }
        [ObservableProperty]
        public partial List<string>? Animation { get; set; }
    }
}
