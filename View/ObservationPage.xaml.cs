

namespace HikeTracker.View;

public partial class ObservationPage : ContentPage
{
	
	public ObservationPage(ObservationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	
}