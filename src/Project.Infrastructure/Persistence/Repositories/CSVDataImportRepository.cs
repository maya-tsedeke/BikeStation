using AutoMapper;
using LumenWorks.Framework.IO.Csv;
using Microsoft.Extensions.Configuration;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;
using Project.ApplicationCore.Interfaces;
using Project.Infrastructure.Persistence.Contexts;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Project.Infrastructure.Persistence.Repositories
{
    public class CSVDataImportRepository : ICSVDataImportRepository
    {
        private readonly StationsContext stationsContext;
        private readonly IMapper mapper;
        public readonly IConfiguration configuration; 
        public CSVDataImportRepository(StationsContext stationsContext, IMapper mapper, IConfiguration configuration)
        {
            this.stationsContext = stationsContext;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        public CSVFileResponse ImportBikeStationList(CSVFileRequest request,string Path)
        {
            var bikestaion = this.mapper.Map<CSVFileRequest>(request);
            CSVFileResponse response = new CSVFileResponse();
            List<BikeStation> Parameters = new List<BikeStation>();
            try
            {
                if (request.File.FileName.ToLower().Contains(".csv"))
                {
                    DataTable value = new DataTable();
                    //Install Library : LumenWorksCsvReader 
                    using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(Path)), true))
                    {
                        value.Load(csvReader);
                    };
                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                        BikeStation readData = new BikeStation();

                 


                        DateTimeFormatInfo date = new DateTimeFormatInfo();
                        date.ShortDatePattern = "MM/dd/yyyy HH:MM TT";
                        DateTime Departuretiem = Convert.ToDateTime(value.Rows[i][1], date).ToLocalTime();

                        readData.Departure = value.Rows[i][0] != null ? Convert.ToDateTime(value.Rows[i][0], date).ToLocalTime() : DateTime.UtcNow;
                        readData.Return = value.Rows[i][1] != null ? Convert.ToDateTime(value.Rows[i][1], date).ToLocalTime(): DateTime.UtcNow;
                        readData.Departure_station_id = value.Rows[i][2] != null ? Convert.ToString(value.Rows[i][2]) : "-1";
                        readData.Departure_station_name = value.Rows[i][3] != null ? Convert.ToString(value.Rows[i][3]) : "-1";
                        readData.Return_station_id = value.Rows[i][4] != null ? Convert.ToString(value.Rows[i][4]) : "-1";
                        readData.Return_station_name = value.Rows[i][5] != null ? Convert.ToString(value.Rows[i][5]) : "-1";
                        readData.Covered_distance = value.Rows[i][6] != null ? Convert.ToDouble(value.Rows[i][6]) : -1;
                        readData.Duration = value.Rows[i][7] != null ? Convert.ToDouble(value.Rows[i][7]) : -1;
                        Parameters.Add(readData);
                    }

                    if (Parameters.Count > 0)
                    {
                        foreach (BikeStation rows in Parameters)
                        {
                            BikeStation _data = new BikeStation();
                            DateTimeFormatInfo date = new DateTimeFormatInfo();
                            date.ShortDatePattern = "MM/dd/yyyy HH:MM TT";
                            DateTime Departuretiem = Convert.ToDateTime(rows.Departure, date).ToLocalTime();
                            DateTime ReturnTime = Convert.ToDateTime(rows.Return, date).ToLocalTime();
                            var timeDiff = new TimeSpan(ReturnTime.Ticks - Departuretiem.Ticks);
                            float totalduration = Convert.ToInt32(timeDiff.TotalSeconds);
                            if (totalduration > 10 && rows.Covered_distance > 10)
                            {
                                _data.Departure = rows.Departure;
                                _data.Return = rows.Return;
                                _data.Departure_station_id = rows.Departure_station_id;
                                _data.Departure_station_name = rows.Departure_station_name;
                                _data.Return_station_id = rows.Return_station_id;
                                _data.Return_station_name = rows.Return_station_name;
                                _data.Covered_distance = rows.Covered_distance;
                                _data.Duration = totalduration;
                                this.stationsContext.BikeStations.Add(_data);
                                this.stationsContext.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        response.Message = "Enter respective value";
                        response.IsSuccess = false;
                        return response;
                    }

                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "InValid File. Please upload anyfile with .csv extension name";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
            response = this.mapper.Map<CSVFileResponse>(bikestaion);
            response.IsSuccess = true;
            response.Message = "Data Imported Successfully";
            return response;
        }
    }
}
