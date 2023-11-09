using HikeTracker.Service;
using CommunityToolkit.Maui;

namespace HikeTracker.ViewModel
{
    [QueryProperty(nameof(Hike), "Hike")]
    public partial class ObservationViewModel : BaseViewModel
    {
        ObservationService _observationService;
        public ObservableCollection<Observation> Observations { get; } = new();

        public static int HikeID { get; set; }

        [ObservableProperty]
        Hike hike;


        public ObservationViewModel(ObservationService observationService)
        {
            _observationService = observationService;
            Title = "Details and Observations";
            Observations = new ObservableCollection<Observation>();
            Debug.WriteLine("Constructor HikeID: " + HikeID);
            // GetObservationsWithIDAsync(HikeID);
        }

        [RelayCommand]
        void Appearing()
        {
            try
            {

                Debug.WriteLine("Appearing HikeID: " + HikeID);
                GetObservationsAsync();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        async Task GetObservationsAsync()
        {
            Debug.WriteLine("GetObservationsAsync");
            if (IsBusy)
                return;
            try
            {
                Debug.WriteLine("Not busy");
                Observations.Clear();
                IsBusy = true;
                var observations = await _observationService.GetObservationsAsync(Hike.ID);

                if (observations != null && observations.Any())
                {
                    Observations.Clear();
                    foreach (var observation in observations)
                    {
                        Observations.Add(observation);
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
            }
        }

        [RelayCommand]
        async Task GetObservationsWithIDAsync(int hikeID)
        {
            Debug.WriteLine("GetObservationsAsync");
            if (IsBusy)
                return;
            try
            {
                Debug.WriteLine("Not busy");

                IsBusy = true;
                Observations.Clear();
                var observations = await _observationService.GetObservationsAsync(hikeID);

                if (observations != null && observations.Any())
                {

                    foreach (var observation in observations)
                    {
                        Observations.Add(observation);
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
            }
        }
    }
}