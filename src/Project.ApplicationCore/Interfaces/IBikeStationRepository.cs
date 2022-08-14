using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;
using Project.ApplicationCore.Helper;

namespace Project.ApplicationCore.Interfaces
{
    public interface IBikeStationRepository
    {

        void DeleteBikeStationById(int stationId);
        BikeStationResponse CreateBikeStation(CreateBikeStationRequest request);
        BikeStationResponse UpdateBikeStationt(int stationId, UpdateBikeStationRequest request);
        List<StationSearchResponse> Get(StationSearchRequest request);


      public Task<DeleteRecordResponse> DeleteRecord(DeleteRecordRequest request);
    }
}
