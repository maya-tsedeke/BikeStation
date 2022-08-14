using AutoMapper;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;
using Project.ApplicationCore.Helper;
using Project.ApplicationCore.Interfaces;
using Project.Infrastructure.Persistence.Contexts;
using System.Data;


namespace Project.Infrastructure.Persistence.Repositories
{
    public class GetStationRepository : ReadBaseRepository<BikeStation>, IGetStationRepository
    {
        private readonly ISortHelper<BikeStation> _sortHelper;
        public GetStationRepository(StationsContext stationsContext, ISortHelper<BikeStation> sortHelper)
            : base(stationsContext)
        {
            _sortHelper = sortHelper;
        }
        public readonly Mapper mapper;

        public PagedList<BikeStation> GetAllBikeStation(ConditionParameters parm)
        {
            var data = FindByCondition(o => o.Covered_distance >= parm.MinDistance &&
                                             o.Covered_distance <= parm.MaxDistance);
               
            
            SearchByDepartureStationName(ref data, parm.Name);
            var sorteddata=_sortHelper.ApplySort(data, parm.OrderBy);
            var result = PagedList<BikeStation>.ToPagedList(sorteddata, parm.PageNumber, parm.PageSize);


            return result;
           
        }
        private void SearchByDepartureStationName(ref IQueryable<BikeStation> search, string StationName)
        {
     
            if (!search.Any() || string.IsNullOrWhiteSpace(StationName))
                return;

            if (string.IsNullOrEmpty(StationName)) { 
                search = search.OrderBy(x => x.Covered_distance);
                return; 
            }
            search = search.Where(o => o.Departure_station_name.Contains(StationName.Trim()));

        }

    }

}
