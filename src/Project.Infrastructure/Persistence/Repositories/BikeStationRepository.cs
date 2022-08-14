using AutoMapper;
using Microsoft.Extensions.Configuration;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;
using Project.ApplicationCore.Exceptions;
using Project.ApplicationCore.Interfaces;
using Project.Infrastructure.Persistence.Contexts;
using Project.Infrastructure.Persistence.Query;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Project.Infrastructure.Persistence.Repositories
{
    public class BikeStationRepository : IBikeStationRepository
    {
        private readonly StationsContext stationsContext;
        private readonly IMapper mapper;
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _SqlConnection;
        //public string connection = @"Data Source=localhost;Initial Catalog=StationDB;User ID=sa;Password=1819@Parul;";
        public BikeStationRepository(StationsContext stationsContext, IMapper mapper, IConfiguration configuration)
        {
            this.stationsContext = stationsContext;
            this.mapper = mapper;
            this._configuration = configuration;
            _SqlConnection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        }
            public BikeStationResponse CreateBikeStation(CreateBikeStationRequest request)
        {
            var bikestaion = this.mapper.Map<BikeStation>(request);
           
            DateTimeFormatInfo date = new DateTimeFormatInfo();
            date.ShortDatePattern = "yyyy-MM-ddTHH:MM:TT";
          //  DateTime Departuretiem = Convert.ToDateTime(DateTime.UtcNow, date).ToLocalTime();
            bikestaion.Departure = DateTime.UtcNow;
            bikestaion.Covered_distance = 0;
            bikestaion.Duration = 0;
            bikestaion.Return_station_id= string.Empty;
            bikestaion.Return_station_name= string.Empty;
    
           

            this.stationsContext.BikeStations.Add(bikestaion);
            this.stationsContext.SaveChanges();

            return this.mapper.Map<BikeStationResponse>(bikestaion);
        }

        public void DeleteBikeStationById(int stationId)
        {
            var bikestaion = this.stationsContext.BikeStations.Find(stationId);
            if (bikestaion != null)
            {
                
               
                this.stationsContext.BikeStations.Remove(bikestaion);
                this.stationsContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public async Task<DeleteRecordResponse> DeleteRecord(DeleteRecordRequest request)
        {
           
                DeleteRecordResponse response = new DeleteRecordResponse();
                response.IsSuccess = true;
                response.Message = "Successful";
                try
                {
                    if (_SqlConnection.State != ConnectionState.Open)
                    {
                        await _SqlConnection.OpenAsync();
                    }

                    using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.DeleteRecord, _SqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@Id", request.Id);
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Delete Query Not Executed";
                            return response;
                        }
                    }

                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                }
                finally
                {
                    await _SqlConnection.CloseAsync();
                    await _SqlConnection.DisposeAsync();
                }

                return response;
            
        }

        public List<StationSearchResponse> Get(StationSearchRequest request)
        {
            StationSearchResponse response = new StationSearchResponse();
           
            if (string.IsNullOrWhiteSpace(request.SearchKey))
            {
                
                //  query = query.Where(x => x.Departure_station_name.StartsWith(request.SearchKey));
            }
            var data = stationsContext.BikeStations
                .Where(d => d.Departure_station_name.StartsWith(request.SearchKey))
                .Select(p => this.mapper.Map<StationSearchResponse>(p));

            response.Count_Started_from_station = data.Count();
            return data.ToList();


        }

        public BikeStationResponse UpdateBikeStationt(int stationId, UpdateBikeStationRequest request)
        {
            var bikestaion = this.stationsContext.BikeStations.Find(stationId);
            if (bikestaion != null)
            {
                DateTimeFormatInfo date = new DateTimeFormatInfo();
                date.ShortDatePattern = "yyyy-MM-ddTHH:MM:TT";
                DateTime Departuretiem = Convert.ToDateTime(bikestaion.Departure, date).ToLocalTime();
                bikestaion.Return = DateTime.UtcNow;
                DateTime ReturnTime = Convert.ToDateTime(bikestaion.Return, date).ToLocalTime();
                var timeDiff = new TimeSpan(ReturnTime.Ticks - Departuretiem.Ticks);
                float totalduration = Convert.ToInt32(timeDiff.TotalSeconds);
                bikestaion.Return_station_id = request.Return_station_id;
                bikestaion.Return_station_name = request.Return_station_name;
                bikestaion.Duration = totalduration;
                bikestaion.Covered_distance = request.Covered_distance;
                this.stationsContext.BikeStations.Update(bikestaion);
                this.stationsContext.SaveChanges();

                return this.mapper.Map<BikeStationResponse>(bikestaion);
            }

            throw new NotFoundException();
        }

    }
}
