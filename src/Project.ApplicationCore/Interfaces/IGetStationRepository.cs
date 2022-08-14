using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;
using Project.ApplicationCore.Helper;

namespace Project.ApplicationCore.Interfaces
{
    public interface IGetStationRepository:IReadBaseRepository<BikeStation>
    {
        PagedList<BikeStation> GetAllBikeStation(ConditionParameters parm);
       

    }
}

