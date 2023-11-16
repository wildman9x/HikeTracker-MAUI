

using HikeTracker.DB;

namespace HikeTracker.Service
{
    public class HikeService
    {
        List<Hike> _hikes = new();
        HikeDB _hikeDB = new();
        public async Task<List<Hike>> GetHikesAsync()
        {
            _hikes = await _hikeDB.GetHikesAsync();
            return _hikes;
        }

        public async Task<Hike> GetHikeAsync(int id)
        {
            return await _hikeDB.GetHikeAsync(id);
        }

        public async Task<List<Hike>> SearchHikesAsync(string name)
        {
            return await _hikeDB.SearchHikesAsync(name);
        }

        public async Task<int> SaveHikeAsync(Hike hike)
        {
            return await _hikeDB.SaveHikeAsync(hike);
        }

        public async Task<int> DeleteHikeAsync(Hike hike)
        {
            return await _hikeDB.DeleteHikeAsync(hike);
        }

        // Search for hikes by name
        public async Task<List<Hike>> SearchHikesByNameAsync(string name)
        {
            return await _hikeDB.SearchHikesByNameAsync(name);
        }
    }
}