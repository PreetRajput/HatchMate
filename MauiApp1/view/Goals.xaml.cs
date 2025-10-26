using MauiApp1.apiCalls;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;
using System.Globalization;
using System.ComponentModel;
using MauiApp1.model;
using MauiApp1.viewModel;

namespace MauiApp1;

public partial class Goals : ContentPage, INotifyPropertyChanged
{
    public Goals()
    {
        InitializeComponent();

        BindingContext = new GoalsViewModel();
        tasksContainer.BindingContext = BindingContext;


    }
    bool _isInitialized = false;
    protected override void OnAppearing()
    {

        base.OnAppearing();
        if (_isInitialized) return;
        (BindingContext as GoalsViewModel)?.addingTasksCommand.Execute(null);
        _isInitialized = true;
    }

}
