namespace Project.ApplicationCore.DTOs
{
    public class StationSearchRequest
    {
       public string SearchKey { get; set; }
    }
    public class StationSearchResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int TotalRecord { get; set; }
        public int Count_Started_from_station { get; set; }
        public int Count_Ended_at_station { get; set; }
        public List<StationSearch>? FilterRecord { get; set; }


    }
    public class StationSearch
    {

        public string? Departure_station_name { get; set; }

        public string? Return_station_name { get; set; }

        public double Covered_distance { get; set; }

        public double Duration { get; set; }
    }
    public class GetStation
    {
        public string? Departure_station_name { get; set; }
    }
    public class GetStationResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
 
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;
        public int TotalSearch { get; set; }

        public List<GetStation>? Getstation { get; set; } 
    }
    public class GetStationRequest
    {
        public string SearchKey { get; set; }=String.Empty;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;

    }
}