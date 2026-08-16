using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.Interfaces
{
    public interface IPopupService
    {
        public Task OpenUserAddedPopUp(string message, string title);
    }
}
