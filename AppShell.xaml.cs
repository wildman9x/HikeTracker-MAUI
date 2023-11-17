namespace HikeTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ObservationPage), typeof(ObservationPage));
            Routing.RegisterRoute(nameof(AddHike), typeof(AddHike));
        }
    }
}
