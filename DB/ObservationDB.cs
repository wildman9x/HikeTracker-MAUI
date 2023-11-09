using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace HikeTracker.DB
{
    public class ObservationDB
    {
        SQLiteAsyncConnection _database;

        async Task Init() {
            if (_database is not null)
                return;
            
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await _database.CreateTableAsync<Observation>();

            // If the Hike ID 1 doesn't have any observation, generate 3 randomly
            if (await _database.Table<Observation>().CountAsync(o => o.HikeID == 1) == 0) {
                var mockObservations = new List<Observation> {
                    new Observation {
                        HikeID = 1,
                        Name = "Mock Observation 1",
                        Date = DateTime.Now,
                        Comment = "Mock comment 1"
                    },
                    new Observation {
                        HikeID = 1,
                        Name = "Mock Observation 2",
                        Date = DateTime.Now.AddMinutes(30),
                        Comment = "Mock comment 2"
                    },
                    new Observation {
                        HikeID = 1,
                        Name = "Mock Observation 3",
                        Date = DateTime.Now.AddMinutes(30),
                        Comment = "Mock comment 3"
                    }
                };
                await _database.InsertAllAsync(mockObservations);
            }
        }

        public async Task<List<Observation>> GetObservationsAsync(int hikeID) {
            await Init();
            return await _database.Table<Observation>().Where(o => o.HikeID == hikeID).ToListAsync();
        }

        public async Task<Observation> GetObservationAsync(int id) {
            await Init();
            return await _database.Table<Observation>().Where(o => o.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveObservationAsync(Observation observation) {
            await Init();
            if (observation.ID != 0) {
                return await _database.UpdateAsync(observation);
            } else {
                return await _database.InsertAsync(observation);
            }
        }

        public async Task<int> DeleteObservationAsync(Observation observation) {
            await Init();
            return await _database.DeleteAsync(observation);
        }


        public async Task<int> DeleteAllObservationsAsync() {
            await Init();
            return await _database.DeleteAllAsync<Observation>();
        }

        public async Task<int> DeleteAllObservationsOfAHikeAsync(int hikeID) {
            await Init();
            return await _database.Table<Observation>().DeleteAsync(o => o.HikeID == hikeID);
        }

        
    }
}