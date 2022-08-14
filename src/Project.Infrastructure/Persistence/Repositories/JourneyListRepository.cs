

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;
using Project.ApplicationCore.Exceptions;
using Project.ApplicationCore.Helper;
using Project.ApplicationCore.Interfaces;
using Project.Infrastructure.Persistence.Contexts;
using Project.Infrastructure.Persistence.Query;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Project.Infrastructure.Persistence.Repositories
{
    public class JourneyListRepository : IJourneyListRepository
    {
        private readonly StationsContext _context;
        public static int PAGE_SIZE { get; set; } = 5;
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _SqlConnection;
        private readonly IMapper mapper;
    
        public JourneyListRepository(StationsContext context,IConfiguration configuration, IMapper _mapper)
        {
            _context = context;
            _configuration = configuration;
            _SqlConnection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            mapper = _mapper;   
        }
   
        public async Task<StationSearchResponse> FilterRecord(StationSearchRequest request)
        {
            StationSearchResponse response = new StationSearchResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            int e=0,s=0;
            try
            {
                if (_SqlConnection.State != ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.FilterRecord, _SqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@stationname", request.SearchKey);
                    using (SqlDataReader datafilter = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (datafilter.HasRows)
                        {
                            response.FilterRecord = new List<StationSearch>();
                            while (await datafilter.ReadAsync())
                            {
                                StationSearch selectdata = new StationSearch();
                          

                                selectdata.Departure_station_name = datafilter["Departure_station_name"] != DBNull.Value ? Convert.ToString(datafilter["Departure_station_name"]) : string.Empty;
                                selectdata.Return_station_name = datafilter["Return_station_name"] != DBNull.Value ? Convert.ToString(datafilter["Return_station_name"]) : string.Empty;
                                selectdata.Covered_distance = datafilter["Covered_distance"] != DBNull.Value ? Convert.ToDouble(datafilter["Covered_distance"]) : 0;
                                selectdata.Duration = datafilter["Duration"] != DBNull.Value ? Convert.ToDouble(datafilter["Duration"]) : 0;
                             
                                //Count Ending Station
                                var EndingStation = _context.BikeStations
                                    .Where(d => d.Return_station_name.Equals(request.SearchKey))
                                    .Select(p => this.mapper.Map<StationSearchResponse>(p));
                                response.Count_Ended_at_station = EndingStation.Count();
                                e = response.Count_Ended_at_station;
                                //Count Starting station
                                var StartingStation = _context.BikeStations
                                   .Where(d => d.Departure_station_name.Equals(request.SearchKey))
                                   .Select(p => this.mapper.Map<StationSearchResponse>(p)
                                   );
                                
                                response.Count_Started_from_station = StartingStation.Count();
                                 s = response.Count_Started_from_station;
                                response.TotalRecord = response.Count_Started_from_station+response.Count_Ended_at_station;
                                response.FilterRecord.Add(selectdata);
                            }
                        }
                    }


                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if(e >= 1 && s < 1)
                        {
                            response.Message = " Found " + response.Count_Ended_at_station + " And No journeys starting from the station";
                            response.IsSuccess = true;
                        }
                        else if (e < 1 && s >= 1)
                        {
                            response.Message = " Found " + response.Count_Started_from_station + " journeys starting from station and No  journeys ending at the station";
                            response.IsSuccess = true;
                        }
                        else if (e >= 1 && s >= 1)
                        {
                            response.Message = " Found "+response.Count_Started_from_station+" journeys starting from station and "+response.Count_Ended_at_station+" journeys ending at the station";
                            response.IsSuccess = true;
                        }
                        else if (e < 1 && s < 1)
                        {
                            response.Message = "Station not found in the database from "+response.TotalRecord+" records in the database";
                            response.IsSuccess = false;
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
        public async Task<GetStationResponse> GetAllStation(GetStationRequest request)
        {
            GetStationResponse response = new GetStationResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
     
            try
            {
                if (_SqlConnection.State != ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }
                var allResult = SqlQueries.GetAllStation;
                if (request.SearchKey==string.Empty)
                {
                    allResult = SqlQueries.AllResult;
                }
                var x = SqlQueries.GetAllStation;

                using (SqlCommand sqlCommand = new SqlCommand(allResult, _SqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@stationname", request.SearchKey);
                    sqlCommand.Parameters.AddWithValue("@PageSize", request.PageSize);
                    sqlCommand.Parameters.AddWithValue("@PageNumber", request.PageNumber);
                  
                    using (SqlDataReader stationRead = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (stationRead.HasRows)
                        {
                            response.Getstation = new List<GetStation>();
                            while (await stationRead.ReadAsync())
                            {
                                GetStation getStation = new GetStation();


                                getStation.Departure_station_name = stationRead["Departure_station_name"] != DBNull.Value ? Convert.ToString(stationRead["Departure_station_name"]) : string.Empty;
                                //Count Ending Station
                                var EndingStation = _context.BikeStations
                                    .Where(d => d.Departure_station_name.StartsWith(request.SearchKey))
                                    .Select(p => this.mapper.Map<StationSearchResponse>(p.Return_station_name));
                                     response.TotalSearch = EndingStation.Count();


                                response.CurrentPage = request.PageNumber;
                                response.TotalPage = (int)Math.Ceiling(response.TotalSearch / (double)request.PageSize);
                                response.Getstation.Add(getStation);
                            }
                        }
                    }
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (response.TotalSearch < 1)
                    {
                        response.Message = "Station not found in the database ";
                        response.IsSuccess = false;
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

        public async Task<AdditionalResponse> Additional()
        {
            AdditionalResponse response = new AdditionalResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection.State != ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }
            

                using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.Additional, _SqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    using (SqlDataReader stationRead = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (stationRead.HasRows)
                        {
                            response.Top5PopularDepartureStationn = new List<PopularDepartureStation>();
                            response.Top5PopularReturnStationn = new List<PopularReturnStation>();

                            while (await stationRead.ReadAsync())
                            {
                                PopularDepartureStation popularDepartureStation = new PopularDepartureStation();
                         

                                //Starting popular station 
                                popularDepartureStation.NumberOfStartingJourney = stationRead["DepartureStation"] != DBNull.Value ? Convert.ToInt16(stationRead["DepartureStation"]) : 0;
                                popularDepartureStation.popular_Departure_stations = stationRead["Departure_station_name"] != DBNull.Value ? Convert.ToString(stationRead["Departure_station_name"]) : string.Empty;
                                popularDepartureStation.Average_distance_startingFrom_station = stationRead["TotalDistance"] != DBNull.Value ? Convert.ToDouble(stationRead["TotalDistance"]) : 0;
                             
                                response.Top5PopularDepartureStationn.Add(popularDepartureStation);
                            }
                        }
                    }
                    int Status = await sqlCommand.ExecuteNonQueryAsync();

                }
                using (SqlCommand sqlCommand2 = new SqlCommand(SqlQueries.ReturnPopular, _SqlConnection))
                {
                    sqlCommand2.CommandType = CommandType.Text;
                    sqlCommand2.CommandTimeout = 180;
                    using (SqlDataReader stationRead = await sqlCommand2.ExecuteReaderAsync())
                    {
                        if (stationRead.HasRows)
                        {
                           
                            response.Top5PopularReturnStationn = new List<PopularReturnStation>();

                            while (await stationRead.ReadAsync())
                            {
                              
                                PopularReturnStation popularReturnStation = new PopularReturnStation();
                                //Ending Popular Station 
                                // popularReturnStation.popular_return_stations = stationRead["Return_station_name"] != DBNull.Value ? Convert.ToString(stationRead["Return_station_name"]) : string.Empty;
                                popularReturnStation.popular_return_stations = stationRead["Return_station_name"] != DBNull.Value ? Convert.ToString(stationRead["Return_station_name"]) : string.Empty;
                                popularReturnStation.NumberOfEndingJourney = stationRead["ReturnStation"] != DBNull.Value ? Convert.ToInt16(stationRead["ReturnStation"]) : 0;
                                popularReturnStation.Average_distance_ending_at_station = stationRead["TotalDistance"] != DBNull.Value ? Convert.ToDouble(stationRead["TotalDistance"]) : 0;
                                
                                response.Top5PopularReturnStationn.Add(popularReturnStation);
                            }
                        }
                    }
                    int Status = await sqlCommand2.ExecuteNonQueryAsync();

                }
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message=ex.Message.ToString();
                return response;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<FilterByMonthResponse> FilterByMonth(FilterByMonth request)
        {
            FilterByMonthResponse response = new FilterByMonthResponse();
            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                if (_SqlConnection.State != ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }
                var allResult = SqlQueries.FilterByMonth;
                if (request.Year == 0&& request.Month==0)
                {
                    allResult = SqlQueries.AllResult;
                }
                var x = SqlQueries.GetAllStation;

                using (SqlCommand sqlCommand = new SqlCommand(allResult, _SqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@month", request.Month);
                    sqlCommand.Parameters.AddWithValue("@year", request.Year);
                    sqlCommand.Parameters.AddWithValue("@PageSize", request.PageSize);
                    sqlCommand.Parameters.AddWithValue("@PageNumber", request.PageNumber);

                    using (SqlDataReader stationRead = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (stationRead.HasRows)
                        {
                            response.Monthly = new List<StationSearch>();
                            while (await stationRead.ReadAsync())
                            {
                                StationSearch selectdata = new StationSearch();

                                selectdata.Departure_station_name = stationRead["Departure_station_name"] != DBNull.Value ? Convert.ToString(stationRead["Departure_station_name"]) : string.Empty;
                                selectdata.Return_station_name = stationRead["Return_station_name"] != DBNull.Value ? Convert.ToString(stationRead["Return_station_name"]) : string.Empty;
                                selectdata.Covered_distance = stationRead["Covered_distance"] != DBNull.Value ? Convert.ToDouble(stationRead["Covered_distance"]) : 0;
                                selectdata.Duration = stationRead["Duration"] != DBNull.Value ? Convert.ToDouble(stationRead["Duration"]) : 0;

                                selectdata.Departure_station_name = stationRead["Departure_station_name"] != DBNull.Value ? Convert.ToString(stationRead["Departure_station_name"]) : string.Empty;
                                //Count Ending Station TotalCount
                                response.TotalSearch = stationRead["TotalCount"] != DBNull.Value ? Convert.ToInt16(stationRead["TotalCount"]) : 0;
                                response.CurrentPage = request.PageNumber;
                                response.TotalPage = (int)Math.Ceiling(response.TotalSearch / (double)request.PageSize);
                                response.Monthly.Add(selectdata);
                            }
                        }
                    }
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (response.TotalSearch == 0)
                    {
                        response.Message = "Station not found in the database from " + response.TotalSearch + " records in the database";
                        response.IsSuccess = false;
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
    }
}
