using MauiApp1.apiCalls;
using Microsoft.Maui.Controls.Shapes;
using System.Drawing;
using System.Globalization;

namespace MauiApp1;

public partial class taskAddition : ContentPage
{
    userDetails Player;
    List<Editor> editors= new List<Editor>();
    List<string> tasks= new List<string>();
    public taskAddition(userDetails Player)
	{
        this.Player = Player;
		InitializeComponent();
        AnimateImage();
	}
    // In code-behind (NewPage2.xaml.cs)
    async void AnimateImage()
    {
        while (true)
        {
            // Rotate around Y-axis (pseudo-3D)
            await selectedEgg.RotateYTo(360, 2000);  // 360 degrees in 2 seconds
            selectedEgg.RotationY = 0;               // reset to start
        }
    }
    public void addGoal(object sender, EventArgs e)
    {
        var closeBtn = new Button
        {
            ImageSource = "cross.png",
            BackgroundColor = Colors.Transparent,
            HeightRequest = 8,
        };
        closeBtn.Clicked += removeCont;
        Grid.SetColumn(closeBtn, 1);

        var text = new Label
        {
            Text = "Custom Task",
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
        };
        Grid.SetRow(text, 1);
        Grid.SetColumnSpan(text, 2);
        var border = new Border
        {
            Stroke = Colors.Black,
            StrokeThickness = 2,
            BackgroundColor = Colors.LightGray,
            Padding = 10,
            StrokeShape= new RoundRectangle {CornerRadius= 12},
            // Only one Content assignment
            Content = new VerticalStackLayout
            {
                Children =
                    {
                        new Grid
                        {
                            ColumnDefinitions= new ColumnDefinitionCollection
                            {
                                new ColumnDefinition {Width= GridLength.Star},
                                new ColumnDefinition {Width= GridLength.Auto},

                            },
                            RowDefinitions= new RowDefinitionCollection
                            {
                                new RowDefinition {Height= GridLength.Auto},
                                new RowDefinition {Height= GridLength.Star},

                            },
                            Children=
                            {
                                    text,
                                    closeBtn,

                            }
                        },
                                    new Editor
                                    {
                                        ClassId= "customTask",
                                        Placeholder = "Describe your task...",
                                        AutoSize = EditorAutoSizeOption.TextChanges
                                    }
                    }
            }
        };
        tasksContainer.Children.Add(border);
    }
    public void removeCont(object sender, EventArgs e)
    {
        Button cross = (Button)sender;

        var tasks = cross.Parent.Parent.Parent;

        var taskBox = tasks.Parent as Layout;

        if (taskBox!= null && tasks is Border border)
        {
            taskBox.Children.Remove(border);
        }

    }
    public async void hatchEgg(object sender, EventArgs e)
    {
        editors= findAllEditor(tasksContainer, "customTask", editors);
        foreach (Editor item in editors)
        {
            tasks.Add(item.Text);
            await DisplayAlert("Success", $"{item.Text}", "OK");
        }
        Player.tasks = tasks;
        await Navigation.PushAsync(new petNameInput(Player));
    }

    public List<Editor> findAllEditor(Layout layout, string id,   List<Editor> result)
    {
        foreach (var item in layout.Children)
        {
            if(item is Editor e && e.ClassId== id)
            {
                result.Add(e);
            }
            
            if(item is Layout nested )
            {
                findAllEditor(nested,id, result);
            } 

            if(item is Border border)
            {
                    if (border.Content is Layout bunch)
                    {
                        findAllEditor(bunch, id, result);
                    }
            }
            
        }
        return result;
        
    }


}