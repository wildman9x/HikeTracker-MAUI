using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HikeTracker.DB;

namespace HikeTracker.Service
{
    public class ObservationService
    {
        List<Observation> observations;
        ObservationDB observationDB;

        public async Task<List<Observation>> GetObservationsAsync(int hikeID)
        {
            observationDB = new ObservationDB();
            observations = await observationDB.GetObservationsAsync(hikeID);
            return observations;
        }

        public async Task DeleteObservationAsync(Observation observation)
        {
            observationDB = new ObservationDB();
            await observationDB.DeleteObservationAsync(observation);
        }

        internal Task AddObservationAsync(Observation newObservation)
        {
            observationDB = new ObservationDB();
            return observationDB.SaveObservationAsync(newObservation);
        }
    }
}