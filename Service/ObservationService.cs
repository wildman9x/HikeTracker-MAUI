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
    }
}