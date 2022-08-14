

namespace Project.ApplicationCore.DTOs
{
    public class FilterByMonth
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public Months Month { get; set; }
        public int Year { get; set; }
    }
    public enum Months
    {
        NotSet = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }
    public class FilterByMonthResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;
        public int TotalSearch { get; set; }
        public List<StationSearch>? Monthly { get; set; } 
    }

}
