using AutoMapper;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;

namespace Project.ApplicationCore.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateBikeStationRequest, BikeStation>();
            CreateMap<BikeStation, BikeStationResponse>();
            CreateMap<CSVFileRequest, CSVFileResponse>();
            CreateMap<BikeStation, CSVFileResponse>();
            CreateMap<StationSearchRequest, BikeStation>();
            CreateMap<BikeStation, StationSearchResponse>().ReverseMap();

        }
   
    }

}
