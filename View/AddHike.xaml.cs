namespace HikeTracker.View;

public partial class AddHike : ContentPage
{
	public AddHike(AddHikeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}