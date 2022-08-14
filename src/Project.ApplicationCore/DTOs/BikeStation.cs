using Project.ApplicationCore.Entities.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project.ApplicationCore.DTOs
{
    public class CreateBikeStationRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 6), DisplayName("Departure Station Name")]
        public string? Departure_station_name { get; set; }
       // [Required, DisplayName("Departure Date and Time")]
        //public string? Departure { get; set; }
        [Required,DisplayName("Deprture Station ID")]
        [StringLength(16, MinimumLength = 4)]
        public string? Departure_station_id { get; set; }

    }
    public class UpdateBikeStationRequest: CreateBikeStationRequest
    {
     
        [StringLength(16, MinimumLength = 4),DisplayName("Return Station ID")]
        public string? Return_station_id { get; set; }

        [StringLength(100, MinimumLength = 3), DisplayName("Return Station Name")]
        public string? Return_station_name { get; set; }
        [Required, DisplayName("Distance")]
        public double Covered_distance { get; set; }
        [Required, DisplayName("Duration in Second")]
        public double Duration { get; set; }
    }
    public class BikeStationResponse
    {
        [DisplayName("Departure Date and Time")]
        public DateTime? Departure { get; set; }
        [DisplayName("Return Date and Time")]
        public DateTime? Return { get; set; }
        [DisplayName("Departure Station Id")]
        public string? Departure_station_id { get; set; }
        [DisplayName("Departure Station Name")]
        public string? Departure_station_name { get; set; }
        [DisplayName("Return Station ID")]
        public string? Return_station_id { get; set; }
        [DisplayName("Return Station Name")]
        public string? Return_station_name { get; set; }
        [DisplayName("Distance")]
        public double Covered_distance { get; set; }
        [DisplayName("Duration")]
        public double Duration { get; set; }
       
    }

 
    public class FilterRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int CurrentPage { get; set; }
        public double TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public List<BikeStation>? FilterRecord { get; set; }
    }
}
