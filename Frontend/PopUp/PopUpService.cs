using CommunityToolkit.Maui.Extensions;
using MauiApp1.BaseClass;
using MauiApp1.Interfaces;
using MauiApp1.PopUp.View;
using MauiApp1.PopUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.PopUp
{
    public class PopUpService : IPopupService
    {
        public async Task OpenUserAddedPopUp(string Message, string Title)
        {
            try
            {
            var dialog = new CompletionPopUp();
            var vm = new CompletionPopUp_ViewModel();
            vm.Message = Message;
            vm.Title = Title;
            dialog.BindingContext = vm;
            await Application.Current.MainPage.ShowPopupAsync(dialog);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
