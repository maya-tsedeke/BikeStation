using Project.ApplicationCore.Entities.Models;
using Project.ApplicationCore.Helper;
using Project.ApplicationCore.Interfaces;
using Project.Infrastructure.Persistence.Contexts;

namespace Project.Infrastructure.Persistence.Repositories
{
    public class ReaderWrapperRepository : IReaderWrapperRepository
    {
        private StationsContext _stationContext;
        private IGetStationRepository _bikestation;
        private ISortHelper<BikeStation> _stationSortHelper;

        public ReaderWrapperRepository(StationsContext stationContext, ISortHelper<BikeStation> stationSortHelper)
        {
            _stationContext = stationContext;
            _stationSortHelper=stationSortHelper;
        }

        public IGetStationRepository BikeStation 
        {
            get
            {
                if (_bikestation == null)
                {
                    _bikestation = new GetStationRepository(_stationContext,_stationSortHelper);
                }

                return _bikestation;
            }
        }
        public void Save()
        {
            _stationContext.SaveChanges();
        }
    }
}
