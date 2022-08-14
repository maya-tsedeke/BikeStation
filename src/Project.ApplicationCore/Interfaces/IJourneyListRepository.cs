using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;

namespace Project.ApplicationCore.Interfaces
{
    public interface IJourneyListRepository
    {
     
        public Task<StationSearchResponse> FilterRecord(StationSearchRequest request);
        public Task<GetStationResponse> GetAllStation(GetStationRequest request);
        public Task<AdditionalResponse> Additional();
        public Task<FilterByMonthResponse> FilterByMonth(FilterByMonth request);


    }
}
