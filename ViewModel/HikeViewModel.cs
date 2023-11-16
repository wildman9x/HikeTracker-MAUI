using HikeTracker.Service;


namespace HikeTracker.ViewModel
{
    public partial class HikeViewModel : BaseViewModel
    {
        HikeService hikeService;
        public ObservableCollection<Hike> Hikes { get; } = new();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        

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
        // SearchCommand
        async Task SearchAsync(string SearchText)
        {
            Debug.WriteLine("SearchAsync");
            if (IsBusy)
                return;
            try
            {
                Debug.WriteLine("Not busy, search for: " + SearchText);
                IsBusy = true;
                var hikes = await hikeService.SearchHikesAsync(SearchText);
                Debug.WriteLine("Done with search" + hikes.Count);
                if (hikes != null && hikes.Count > 0)
                {
                    Hikes.Clear();
                    foreach (var hike in hikes)
                    {
                        Hikes.Add(hike);
                    }
                    Debug.WriteLine("Number of Hikes: " + Hikes.Count);
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