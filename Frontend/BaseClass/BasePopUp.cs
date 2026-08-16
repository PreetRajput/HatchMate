using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.BaseClass
{
    public class BasePopUp : ObservableObject 
    {
        public Task Accept() => Task.CompletedTask;
        public Task Cancel() => Task.CompletedTask;
    }
}
