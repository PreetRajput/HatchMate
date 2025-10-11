using MauiApp1.apiCalls;
using MauiApp1.Scripts;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;
using System.Globalization;
using System.ComponentModel;

namespace MauiApp1;

public partial class Goals : ContentPage, INotifyPropertyChanged
{
    Button plusBtn;
    bool tasksLoaded = false;
    public bool _isEditing = false;
    int trash = 0;
    string id;
    int totalTask= 0;
    List<Label> labels = new();
    List<string> updatedTasks = new();
   public bool IsEditing
    {
        get => _isEditing; // returns the current value stored in the private field
        set
        {
            if (_isEditing != value) // check if the new value differs from the old one
            {
                _isEditing = value; // assign the new value to the private field
                OnPropertyChanged(nameof(IsEditing)); // tell MAUI/UI the property changed
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



    public Goals()
    {
        InitializeComponent();
        BindingContext = this;
        Loaded += async (_, _) =>
        {
            if (!tasksLoaded)
            {
                await addingTasks();
                tasksLoaded = true;
            }
        };
    }


    public async Task addingTasks()
    {

        var user = detailsPage.currentUser?.tasks;
        if (user is null)
        {
            id = Preferences.Get("email", string.Empty);
            var apiForRetrieve = new retrieveData();
            userDetails check = await apiForRetrieve.retrieveUserData(id);
            detailsPage.currentUser = check;
            user = check?.tasks;
        }
        
        foreach (var item in user)
        {
                Border border = borderMaker(item);
                tasksContainer.Children.Add(border);
                totalTask++;
        }
        Preferences.Set("totalTask", totalTask);
    }

    private void addMoreTasks(object sender, EventArgs e)
    {
        IsEditing = true;

        Button addGoalsBtn = (Button)sender;
        plusBtn = new Button
        {
            HorizontalOptions = LayoutOptions.Start,
            Text = "+",
            BackgroundColor = Color.FromRgba("#78C001"),
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            CornerRadius = 20,
            Margin = new Thickness(15, 0, 15, 15),
            AutomationId= "okBtn"
        };

        plusBtn.Clicked += moreTasks;

        Grid.SetRow(plusBtn, 2);
        goalsContainer.Children.Add(plusBtn);
        addGoalsBtn.Text = "Done";
        addGoalsBtn.Clicked -= addMoreTasks;
        addGoalsBtn.Clicked += addedMoreTasks;
       
    }

    private void moreTasks(object? sender, EventArgs e)
    {
        IsEditing = true;
        Border border = borderMaker("");
      
        tasksContainer.Children.Add(border);
    }
    private async void addedMoreTasks(object? sender, EventArgs e)
    {
        Button addGoalsBtn = (Button)sender;
        foreach (var item in labels)
        {
            if ( string.IsNullOrWhiteSpace(item.Text))
            {
                trash++;
                DisplayAlert("error", "please fill up all tasks", "OK");
            }
        }
        if (trash==0)
        {

            addGoalsBtn.Text = "+ Add Goals";
            addGoalsBtn.Clicked += addMoreTasks;
            addGoalsBtn.Clicked -= addedMoreTasks;
            goalsContainer.Remove(plusBtn);
            IsEditing = false;
            var postApi = new updateData();
            updatedTasks.Clear();
            totalTask = 0;
            foreach (var item in labels)
            {
                updatedTasks.Add(item.Text);
                totalTask++;
            }
            Preferences.Set("totalTask", totalTask);

            userDetails patchData = new userDetails
            {
                id= id,
                tasks = updatedTasks
            };
            await postApi.updateUserInfo(patchData);

        }

        trash = 0;
    }

    private void deleteTask(object? sender, EventArgs e)
    {
            var btn = (Button)sender;
            var grid = btn.Parent as Grid;
            var stackOfLabels = grid.Children;
            foreach (var item in stackOfLabels)
            {
                if (item is Label label)
                {
                    labels.Remove(label);
                    updatedTasks.Remove(label.Text);

                }
            }
        var border = grid?.Parent as Border;

            if (border != null && tasksContainer.Children.Contains(border))
                tasksContainer.Children.Remove(border);
    }
   
    public Border borderMaker(string labelText)
    {
        var arrowImg = new Image
        {
            VerticalOptions = LayoutOptions.Center,
            HeightRequest = 30,
            Source = "right.png",
        };
        Grid.SetColumn(arrowImg, 1);

        var entry = new Entry
        {
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            Text = labelText,

        };
        Binding binding = new Binding()
        {
            Source = this,
            Path = "IsEditing",
        };
        entry.SetBinding(Entry.IsVisibleProperty, binding);

        var label = new Label
        {
            Text = labelText,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
        };
        labels.Add(label);
        label.SetBinding(Label.IsVisibleProperty, new Binding
        {
            Path = "IsEditing",
            Source = this,
            Converter = new InverseBoolConverter()
        });
        label.SetBinding(Label.TextProperty, new Binding { Source = entry, Path = "Text" });
       
        Button delTask = new Button
        {
            ImageSource = "cross.png",
            BackgroundColor = Colors.Transparent,
            HeightRequest = 8,
            VerticalOptions = LayoutOptions.Start,
        };
        delTask.SetBinding(Button.IsVisibleProperty, new Binding { Source = this, Path = "IsEditing" });
        delTask.Clicked += deleteTask;
        Grid.SetColumn(delTask, 2);
        Border border = new Border
        {
            Stroke = Color.FromArgb("#E0E0E0"),
            StrokeThickness = 1,
            VerticalOptions = LayoutOptions.Start,
            StrokeShape = new RoundRectangle { CornerRadius = 30 },
            BackgroundColor = Color.FromArgb("#1A1A1A"),
            Margin = 16,
            Content = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    },
                Padding = 16,
                Children =
                    {
                        
                        entry,
                        label,
                        arrowImg,
                        delTask  
                    }
            }
        };
        var tap = new TapGestureRecognizer();
        tap.Tapped += async (s, e) => await Pet.addExp();
        border.GestureRecognizers.Add(tap);
        return border;
    }

    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;
    }

}
