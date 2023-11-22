using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace HikeTracker.DB
{
    public class HikeDB
    {
        SQLiteAsyncConnection _database;

        async Task Init()
        {
            if (_database != null)
                return;
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await _database.CreateTableAsync<Hike>();

            // only insert if the table is empty
            if (await _database.Table<Hike>().CountAsync() == 0)
            {
                // Insert mock data
                var mockHikes = new List<Hike>
    {
        new Hike {
            Name = "Mock Hike 1",
            Location = "Mock Location 1",
            Length = 5,
            Date = DateTime.Now.Date,
            ParkingAvailable = true,
            Difficulty = "Easy",
            Description = "Mock hike description 1",
            Weather = "Sunny",
            HasWaterFountain = true
        },
        new Hike {
            Name = "Mock Hike 2",
            Location = "Mock Location 2",
            Length = 10,
            Date = DateTime.Now.AddDays(1).Date,
            ParkingAvailable = false,
            Difficulty = "Moderate",
            Description = "Mock hike description 2",
            Weather = "Partly cloudy",
            HasWaterFountain = false
        },
        new Hike {
            Name = "Mock Hike 3",
            Location = "Mock Location 3",
            Length = 15,
            Date = DateTime.Now.AddDays(2).Date,
            ParkingAvailable = true,
            Difficulty = "Difficult",
            Description = "Mock hike description 3",
            Weather = "Rainy",
            HasWaterFountain = true
        }
    };
                await _database.InsertAllAsync(mockHikes);
                Debug.WriteLine("Mock hikes inserted");
            }

        }

        public async Task<List<Hike>> GetHikesAsync()
        {
            await Init();
            return await _database.Table<Hike>().ToListAsync();
        }

        public async Task<Hike> GetHikeAsync(int id)
        {
            await Init();
            return await _database.Table<Hike>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveHikeAsync(Hike hike)
        {
            await Init();
            if (hike.ID != 0)
            {
                return await _database.UpdateAsync(hike);
            }
            else
            {
                return await _database.InsertAsync(hike);
            }
        }

        public async Task<int> DeleteHikeAsync(Hike hike)
        {
            await Init();
            return await _database.DeleteAsync(hike);
        }

        public async Task<List<Hike>> SearchHikesAsync(string searchText)
        {
            await Init();
            return await _database.Table<Hike>()
                .Where(i => i.Name.ToLower().Contains(searchText.ToLower()) || i.Location.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();
        }

        internal Task<List<Hike>> SearchHikesByNameAsync(string name)
        {
            return _database.Table<Hike>()
        .Where(i => i.Name.ToLower().Contains(name.ToLower()))
        .ToListAsync();
        }
    }
}