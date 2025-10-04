using MauiApp1.Scripts;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace MauiApp1;

public partial class Goals : ContentPage
{
    Button okBtn;
    public Goals()
    {
        InitializeComponent();
        addingTasks();
    }

    public void addingTasks()
    {
        var user = detailsPage.currentUser.tasks;
        foreach (var item in user)
        {
            var arrowImg = new Image
            {
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 30,
                Source = "right.png",
            };
            Grid.SetColumn(arrowImg, 1);
            Button delTask = new Button
            {
                ImageSource = "cross.png",
                BackgroundColor = Colors.Transparent,
                HeightRequest = 8,
                VerticalOptions = LayoutOptions.Start,
            };
            Grid.SetColumn(delTask, 2);
            var border = new Border
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
                        new VerticalStackLayout
                        {
                            Spacing = 4,
                            Children =
                            {
                                new Label
                                {
                                    Text = item,
                                    FontSize = 18,
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Colors.White
                                }
                            }
                        },
                        arrowImg,
                        delTask
                    }
                }
            };
            tasksContainer.Add(border);
        }
    }

    private void addMoreTasks(object sender, EventArgs e)
    {
        Button addGoalsBtn = (Button)sender;
        okBtn = new Button
        {
            HorizontalOptions = LayoutOptions.Start,
            Text = "+",
            BackgroundColor = Color.FromRgba("#78C001"),
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            CornerRadius = 20,
            Margin = new Thickness(0, 0, 15, 15),
            AutomationId= "okBtn"
        };
        
        okBtn.Clicked += moreTasks;

        Grid.SetRow(okBtn, 2);
        goalsContainer.Add(okBtn);
        addGoalsBtn.Text = "Done";
        addGoalsBtn.Clicked += addedMoreTasks;
       
    }

    private void moreTasks(object? sender, EventArgs e)
    {
        var arrowImg = new Image
        {
            VerticalOptions = LayoutOptions.Center,
            HeightRequest = 30,
            Source = "right.png",
        };
        Grid.SetColumn(arrowImg, 1);
        Button delTask = new Button
        {
            ImageSource = "cross.png",
            BackgroundColor = Colors.Transparent,
            HeightRequest = 8,
            VerticalOptions = LayoutOptions.Start,
        };
        Grid.SetColumn(delTask, 2);
        var border = new Border
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
                        new VerticalStackLayout
                        {
                            Spacing = 4,
                            Children =
                            {
                                new Entry
                                {
                                    FontSize = 18,
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Colors.White
                                }
                            }
                        },
                        arrowImg,
                        delTask
                    }
            }
        };
        tasksContainer.Add(border);
    }
    private void addedMoreTasks(object? sender, EventArgs e)
    {
        Button addGoalsBtn = (Button)sender;
        addGoalsBtn.Text = "+ Add Goals";
        goalsContainer.Remove(okBtn);
    }
}
