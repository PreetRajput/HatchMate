using MauiApp1.viewModel;

namespace MauiApp1
{
    public partial class enterPage : ContentPage
    {

        public enterPage()
        {
            InitializeComponent();

            BindingContext= new entryPageViewModel();

        }



    }

}
