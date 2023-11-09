using HikeTracker.Service;


namespace HikeTracker.ViewModel
{
    public partial class HikeViewModel : BaseViewModel
    {
        HikeService hikeService;
        public ObservableCollection<Hike> Hikes { get; } = new();

        public HikeViewModel(HikeService hikeService)
        {
            this.hikeService = hikeService;

            Title = "Hikes";
            this.hikeService = hikeService;
            GetHikesAsync();
        }

        [RelayCommand]
        async Task RefreshAsync()
        {
            await GetHikesAsync();
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GoToDetails(Hike hike)
        {
            if (hike == null)
                return;
            Debug.WriteLine("GoToDetails of ID " + hike.ID);
            ObservationViewModel.HikeID = hike.ID;
            await Shell.Current.GoToAsync(nameof(ObservationPage), true, new Dictionary<string, object> {
                { "Hike", hike }
            });
            
        }


        [RelayCommand]
        async Task GetHikesAsync()
        {
            Debug.WriteLine("GetHikesAsync");
            if (IsBusy)
                return;
            try
            {
                Debug.WriteLine("Not busy");
                IsBusy = true;
                var hikes = await hikeService.GetHikesAsync();

                if (hikes != null && hikes.Any())
                {
                    Hikes.Clear();
                    foreach (var hike in hikes)
                    {
                        Hikes.Add(hike);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}