

using HikeTracker.DB;

namespace HikeTracker.View
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage(HikeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }

}
