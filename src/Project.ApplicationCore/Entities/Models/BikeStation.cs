using Project.ApplicationCore.Entities.Extenssions;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.ApplicationCore.Entities.Models
{
    [Table("BikeStations")]
    public class BikeStation : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? Return { get; set; }
        public string? Departure_station_id { get; set; }
        public string? Departure_station_name { get; set; }
        public string? Return_station_id { get; set; }
        public string? Return_station_name { get; set; }
        public double Covered_distance { get; set; }
        public double Duration { get; set; }

    }
}
