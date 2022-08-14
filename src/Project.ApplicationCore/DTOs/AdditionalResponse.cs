namespace Project.ApplicationCore.DTOs
{
    public class AdditionalResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<PopularDepartureStation>? Top5PopularDepartureStationn { get; set; }
        public List<PopularReturnStation>? Top5PopularReturnStationn { get; set; }
    }
    public class PopularReturnStation
    {  
        public string? popular_return_stations { get; set; }
        public double Average_distance_ending_at_station { get; set; }
        public int NumberOfEndingJourney { get; set; }
    }
    public class PopularDepartureStation
    {
        public string? popular_Departure_stations { get; set; }
        public double Average_distance_startingFrom_station { get; set; }
        public int NumberOfStartingJourney { get; set; }
    }


}