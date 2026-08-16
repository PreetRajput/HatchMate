using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Input;

namespace MauiApp1.PopUp.ViewModels
{
    public partial class CompletionPopUp_ViewModel : BasePopUp
    {
        public IPopupService _popUpService;
        public ICommand CompletedCommand {get; }
        public CompletionPopUp_ViewModel()
        {
            _popUpService = AppService.GetService<IPopupService>();
            CompletedCommand = new AsyncRelayCommand(DoneAsync);
        }
        private String? _message;
        public String? Message
        {
            get=> _message;
            set => SetProperty(ref _message, value);
        }
        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _done;
        public string Done
        {
            get => _done;
            set => SetProperty(ref _done, value);
        }
        public async Task DoneAsync()
        {
            await Task.CompletedTask;
        }
    }
}
