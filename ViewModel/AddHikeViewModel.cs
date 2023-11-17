using HikeTracker.Service;

namespace HikeTracker.ViewModel
{
    [QueryProperty(nameof(Hike), "Hike")]
    public partial class AddHikeViewModel : BaseViewModel
    {
        HikeService hikeService;
        [ObservableProperty]
        Hike hike;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string location;
        [ObservableProperty]
        private DateTime date;
        [ObservableProperty]
        private string parkingAvailable;
        [ObservableProperty]
        private double length;
        [ObservableProperty]
        private string difficulty;
        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private string weather;
        [ObservableProperty]
        private bool hasWaterFountain;

        public AddHikeViewModel(HikeService hikeService)
        {
            this.hikeService = hikeService;
            Title = "Add Hike";
            Date = DateTime.Today;
        }

        [RelayCommand]
        async Task Appearing()
        {
            try
            {
                Debug.WriteLine("Appearing");
                Name = Hike.Name;
                Location = Hike.Location;
                Date = Hike.Date;
                ParkingAvailable = Hike.ParkingAvailable ? "Yes" : "No";
                Length = Hike.Length;
                Difficulty = Hike.Difficulty;
                Description = Hike.Description;
                Weather = Hike.Weather;
                HasWaterFountain = Hike.HasWaterFountain;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        

        [RelayCommand]
        async Task SaveHikeAsync()
        {
            Debug.WriteLine("AddHikeAsync");
            if (IsBusy)
                return;
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a name for the hike", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Location))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a location for the hike", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(ParkingAvailable))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter if parking is available for the hike", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Difficulty))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a difficulty for the hike", "OK");
                return;
            }
            if (double.IsNaN(Length))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a length for the hike", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Weather))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a weather for the hike", "OK");
                return;
            }

            try
            {
                Debug.WriteLine("Not busy, add hike");
                IsBusy = true;
                await hikeService.SaveHikeAsync(new Hike
                {
                    ID = Hike.ID,
                    Name = Name,
                    Location = Location,
                    Date = Date,
                    ParkingAvailable = ParkingAvailable.Equals("Yes") ? true : false,
                    Length = Length,
                    Difficulty = Difficulty,
                    Description = Description,
                    Weather = Weather,
                    HasWaterFountain = HasWaterFountain
                });
                // Clear all fields
                Name = "";
                Location = "";
                Date = DateTime.Today;
                ParkingAvailable = "";
                Length = 0;
                Difficulty = "";
                Description = "";
                Weather = "";
                HasWaterFountain = false;
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}