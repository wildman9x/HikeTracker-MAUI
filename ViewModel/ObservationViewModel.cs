using HikeTracker.Service;
using CommunityToolkit.Maui;

namespace HikeTracker.ViewModel
{
    [QueryProperty(nameof(Hike), "Hike")]
    public partial class ObservationViewModel : BaseViewModel
    {
        HikeService _hikeService;
        ObservationService _observationService;
        public ObservableCollection<Observation> Observations { get; } = new();

        public static int HikeID { get; set; }

        [ObservableProperty]
        Hike hike;

        [ObservableProperty]
        bool isRefreshing;

        public ObservationViewModel(ObservationService observationService)
        {
            _observationService = observationService;
            _hikeService = new HikeService();
            Title = "Details and Observations";
            Observations = new ObservableCollection<Observation>();
            Debug.WriteLine("Constructor HikeID: " + HikeID);
            // GetObservationsWithIDAsync(HikeID);
        }

        [RelayCommand]
        async Task Appearing()
        {
            try
            {

                Debug.WriteLine("Appearing HikeID: " + HikeID);
                await GetObservationsAsync();
                Hike = await _hikeService.GetHikeAsync(HikeID);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        async Task DeleteHikeAsync()
        {
            Debug.WriteLine("DeleteHikeAsync");
            if (IsBusy)
                return;
            try
            {
                // Prompt the user to confirm deletion
                var result = await Shell.Current.DisplayAlert("Delete Hike", $"Are you sure you want to delete {Hike.Name}?", "Yes", "No");
                if (!result)
                    return;
                Debug.WriteLine("Not busy");
                IsBusy = true;
                await _hikeService.DeleteHikeAsync(Hike);
                await Shell.Current.GoToAsync("..", true);
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
        async Task EditHikeAsync()
        {
            Debug.WriteLine("EditHikeAsync");
            if (IsBusy)
                return;
            try
            {
                Debug.WriteLine("Not busy");
                IsBusy = true;
                await Shell.Current.GoToAsync(nameof(AddHike), true, new Dictionary<string, object> {
                { "Hike", Hike }
            });
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
        public async Task AddObservation()
        {
            var name = await Shell.Current.DisplayPromptAsync("New Observation", "Enter name:");
            if (string.IsNullOrWhiteSpace(name))
                return;

            var comment = await Shell.Current.DisplayPromptAsync("New Observation", "Enter comment:");
            if (string.IsNullOrWhiteSpace(comment))
                return;

            var newObservation = new Observation
            {
                HikeID = HikeID,
                Name = name,
                Comment = comment,
                Date = DateTime.Now
            };
            await _observationService.AddObservationAsync(newObservation);
            Observations.Add(newObservation);
        }

        [RelayCommand]
        public async Task ShowObservationDetails(Observation observation)
        {
            Debug.WriteLine("ShowObservationDetails");
            var result = await Shell.Current.DisplayActionSheet(observation.Name,  "Edit", "Delete", $"Date: {observation.Date:d}\nNotes: {observation.Comment}");
            switch (result)
            {
                case "Delete":
                    await _observationService.DeleteObservationAsync(observation);
                    Observations.Remove(observation);
                    break;
                case "Edit":
                    // Pop up a prompt to edit the observation
                    var name = await Shell.Current.DisplayPromptAsync("Edit Observation", "Enter name:", initialValue: observation.Name);
                    if (string.IsNullOrWhiteSpace(name))
                        return;

                    var comment = await Shell.Current.DisplayPromptAsync("Edit Observation", "Enter comment:", initialValue: observation.Comment);
                    if (string.IsNullOrWhiteSpace(comment))
                        return;

                    observation.Name = name;
                    observation.Comment = comment;
                    await _observationService.AddObservationAsync(observation);
                    break;
            }
            await GetObservationsWithIDAsync(HikeID);
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
                IsRefreshing = false;
            }
        }
    }
}